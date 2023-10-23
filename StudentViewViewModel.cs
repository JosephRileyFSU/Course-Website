using Library.project.Services;
using Library.project.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.project.ViewModels
{
    public class StudentViewViewModel : INotifyPropertyChanged
    {
        
        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }
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
                List<Course> CurrentCourseList = new List<Course>();
                if (SelectedPerson != null)
                {
                    foreach (Course course in CourseService.Current.Courses)
                    {
                        if (course != null)
                        {
                            foreach (Person person in course.Roster)
                            {
                                if (person != null)
                                {
                                    {
                                        if (person.id == SelectedPerson.id)
                                        {
                                            CurrentCourseList.Add(course);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (CurrentCourseList != null)
                    {
                        return new ObservableCollection<Course>(CurrentCourseList);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                { 
                    return null; 
                }
            }
        }
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
        public event PropertyChangedEventHandler PropertyChanged;
        public Person SelectedPerson { get; set; }
    }
}
