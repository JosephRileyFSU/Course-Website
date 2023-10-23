using Library.project.Services;
using Library.project.Tabs;
//using SoundAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Library.project.Tabs.Student;
using System.Xml.Linq;

namespace MAUI.project.ViewModels
{
    public class CourseDetailViewModel
    {
        public int Code1;
        public string? Name1 { get; set; }
        public string? Description1 { get; set; }
        public string? Semester1 { get; set; }
        public string? RoomLocation1 { get; set; }
        public IEnumerable<Person?>? roster { get; set; }
        public ObservableCollection<Person> Roster1
        {
            get
            {
                if (roster != null)
                {
                    return new ObservableCollection<Person>(roster);
                }
                else
                {
                    return null;
                }
            }
        }
        public ObservableCollection<AssignmentGroup>AssignmentGroups1
        {
            get
            {
                var roster = CourseService.Current.getCourseByCode(Code1).AssignmentGroups;
                return new ObservableCollection<AssignmentGroup>(roster);
            }
        }
        public ObservableCollection<Submission> Submissions1
        {
            get
            {
                var roster = CourseService.Current.getCourseByCode(Code1).Submissions;
                return new ObservableCollection<Submission>(roster);
            }
        }
        public ObservableCollection<Announcement> Announcements1
        {
            get
            {
                var roster = CourseService.Current.getCourseByCode(Code1).Announcements;
                return new ObservableCollection<Announcement>(roster);
            }
        }
        public ObservableCollection<Module> Modules1
        {
            get
            {
                var roster = CourseService.Current.getCourseByCode(Code1).Modules;
                return new ObservableCollection<Module>(Modules1);
            }
        }
        public ObservableCollection<Person> People
        {
            get
            {
                var filteredList = StudentService.Current.Students.Where(s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);
            }
        }
        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
            }
        }
        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
        }
        public CourseDetailViewModel(int CODE)
        {
            if (CODE > 0)
            {
                LoadByCode(CODE);
            }
        }
        public void LoadByCode(int CODE)
        {
            if (CODE == 0)
            {
                return;
            }
            var course = CourseService.Current.getCourseByCode(CODE);
            if (course != null)
            {
                Code1= course.Code;
                Name1 = course.Name;
                Description1 = course.Description;
                Semester1 = course.Semester;
                RoomLocation1 = course.RoomLocation;
                roster = course.Roster;
            }
            NotifyPropertyChanged(nameof(Name1));
            NotifyPropertyChanged(nameof(Description1));
            NotifyPropertyChanged(nameof(RoomLocation1));
            NotifyPropertyChanged(nameof(Semester1));
            NotifyPropertyChanged(nameof(Roster1));
        }
        public void AddCourse()
        {
            if (Code1 <= 0)
            {
                Course course1 = new Course();
                course1.Name = Name1;
                course1.Description = Description1;
                course1.Semester = Semester1;
                course1.RoomLocation = RoomLocation1;
                CourseService.Current.AddCourse(course1);
            }
            else
            {
                var CourseToUpdate = CourseService.Current.getCourseByCode(Code1);
                CourseToUpdate.Name = Name1;
                CourseToUpdate.Description = Description1;
                CourseToUpdate.Semester = Semester1;
                CourseToUpdate.RoomLocation = RoomLocation1;
            }
            Shell.Current.GoToAsync("//InstructorView");
        }
        public void AddToRosterClicked(int courseId)
        {
            foreach(Course course in CourseService.Current.Courses)
            {
                if(course!=null)
                {
                    if(course.Code==courseId)
                    {
                        course.Roster.Add(SelectedPerson);
                    }
                }
            }
        }
    }
}
