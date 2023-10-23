using Library.project.Services;
using Library.project.Tabs;
using MAUI.project.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Library.project.Tabs.Student;

namespace MAUI.project.ViewModels
{
    class CreateNewPersonModel
    {
        
        public int PersonType { get; set; }
        public string Name { get; set; }
        public string Classification { get; set; }
        public int id { get; set; }
        public CreateNewPersonModel(int ID)
        {
            if(ID>0)
            {
                LoadById(ID);
            }
        }
        public void LoadById(int ID)
        {
            if(ID == 0)
            {
                return;
            }
            var person = StudentService.Current.GetById(ID) as Student;
            if(person != null)
            {
                Name = person.Name;
                id = person.id;
                switch (person.Classification)
                {
                    case StudentYear.Sophmore:
                        Classification = "O";
                        break;
                    case StudentYear.Junior:
                        Classification = "J";
                        break;
                    case StudentYear.Senior:
                        Classification = "S";
                        break;
                    case StudentYear.Freshman:
                    default:
                        Classification = "F";
                        break;
                }
            }
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Classification));
        }

        public void AddPerson()
        {
            if (id <= 0)
            {
                if (PersonType == 2)
                {
                    TA ta = new TA();
                    ta.Name = Name;
                    StudentService.Current.AddTA(ta);
                }
                else if (PersonType == 3)
                {
                    Instructor instructor = new Instructor();
                    instructor.Name = Name;
                    StudentService.Current.AddInstructor(instructor);
                }
                else
                {
                    Student student1 = new Student();
                    Student.StudentYear studentyear;
                    switch (Classification)
                    {
                        case "O":
                            studentyear = Student.StudentYear.Sophmore;
                            break;
                        case "J":
                            studentyear = Student.StudentYear.Junior;
                            break;
                        case "S":
                            studentyear = Student.StudentYear.Senior;
                            break;
                        case "F":
                        default:
                            studentyear = Student.StudentYear.Freshman;
                            break;
                    }
                    student1.Name = Name;
                    student1.Classification = studentyear;
                    StudentService.Current.AddStudent(student1);
                }
            }
            else
            {
                if (PersonType == 2)
                {
                    var TaToUpdate = StudentService.Current.GetById(id) as TA;
                    TaToUpdate.Name = Name;
                }
                else if (PersonType == 3)
                {
                    var InstructorToUpdate = StudentService.Current.GetById(id) as Instructor;
                    InstructorToUpdate.Name = Name;
                }
                else
                {
                    var studentToUpdate = StudentService.Current.GetById(id) as Student;
                    studentToUpdate.Name = Name;
                    studentToUpdate.Classification = StringToStudentYear(Classification);
                }
            }
            Shell.Current.GoToAsync("//InstructorView");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string StudentYearToString(StudentYear studentYear)
        {
            string year = string.Empty;
            switch (studentYear)
            {
                case StudentYear.Sophmore:
                    year = "O";
                    break;
                case StudentYear.Junior:
                    year = "J";
                    break;
                case StudentYear.Senior:
                    year = "S";
                    break;
                case StudentYear.Freshman:
                default:
                    year = "F";
                    break;
            }
            return year;
        }
        public StudentYear StringToStudentYear(string str)
        {
            StudentYear studentYear = StudentYear.Freshman;
            switch (str)
            {
                case "O":
                    studentYear = StudentYear.Sophmore;
                    break;
                case "J":
                    studentYear = StudentYear.Junior;
                    break;
                case "S":
                    studentYear = StudentYear.Senior;
                    break;
                case "F":
                default:
                    studentYear = StudentYear.Freshman;
                    break;
            }
            return studentYear;
        }
    }
}