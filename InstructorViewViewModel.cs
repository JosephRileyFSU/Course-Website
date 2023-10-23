
//using CoreImage;
using Library.project.Services;
using Library.project.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.project.ViewModels
{
    public class InstructorViewViewModel : INotifyPropertyChanged
    {
        public InstructorViewViewModel()
        {
            IsPeopleVisible = true;
            IsCoursesVisible = false;
        }
        public bool IsPeopleVisible { get; set; }
        public bool IsCoursesVisible { get; set; }
        public ObservableCollection<Person> People
        {
            get
            {
                var filteredList = StudentService.Current.Students.Where(s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);   
            }
        }
        public ObservableCollection<Course> Courses
        {
            get
            {
                var filteredList = CourseService.Current.Courses.Where(c => c.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Course>(filteredList);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void ShowPeople()
        {
            IsPeopleVisible = true;
            IsCoursesVisible = false;
            NotifyPropertyChanged("IsPeopleVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }
        public void ShowCourses()
        {
            IsPeopleVisible = false;
            IsCoursesVisible = true;
            NotifyPropertyChanged("IsPeopleVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }
        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
                NotifyPropertyChanged(nameof(Courses));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddPersonClick(Shell s)
        {
            var idParam = SelectedPerson?.id ?? 0;
            s.GoToAsync($"//CreateNewPerson?personId={idParam}");
        }
        public void AddCourseClick(Shell s)
        {
            var codeParam = SelectedCourse?.Code ?? 0;
           s.GoToAsync($"//CourseDetailView?courseId={codeParam}");
        }
        public void RemoveCourseClick(Shell s)
        {
            if (SelectedCourse == null)
            {
                return;
            }
            CourseService.Current.RemoveCourse(SelectedCourse);
            RefreshView();
        }
        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }
        public void RemovePersonClick(Shell s)
        {
            if(SelectedPerson == null)
            {
                return;
            }
            StudentService.Current.RemoveStudent(SelectedPerson);
            RefreshView();
        }
    }
}
