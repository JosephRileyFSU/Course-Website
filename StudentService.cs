using Library.project.Database;
using Library.project.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.project.Tabs.Student;

namespace Library.project.Services
{
    public class StudentService
    {
        private static StudentService? instance;
        public IEnumerable<Student?> Students
        {
            get
            {
                return FakeDatabase.People.Where(p => p is  Student).Select(p => p as Student);
            }
        }
        public IEnumerable<Instructor?> Instructors
        {
            get
            {
                return FakeDatabase.People.Where(i => i is Instructor).Select(i => i as Instructor);
            }
        }
        public IEnumerable<TA?> TAs
        {
            get
            {
                return FakeDatabase.People.Where(t => t is TA).Select(t => t as TA);
            }
        }
        private StudentService()
        {

        }
        public static StudentService Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new StudentService();
                }
                return instance;
            }
        }
        public CourseService? ScourseService { get; set; }
        public void AddStudent(Student student)
        {
            FakeDatabase.People.Add(student);
        }
        public void AddTA(TA ta)
        {
            FakeDatabase.People.Add(ta);
        }
        public void AddInstructor(Instructor instructor)
        {
            FakeDatabase.People.Add(instructor);
        }
        public bool containsStudents()
        {
            bool x;
            if (Students.ToList().Any())
            {
                x=true;
            }
            else
            {
                x=false;
            }

            return x;
        }
        public decimal GetGPA(int studentId)
        {
            if (ScourseService != null)
            {
                var courses = ScourseService.Courses.Where(c => c.Roster.Select(a => a.id).Contains(studentId));
                var totalGradePoints = courses.Select(c => ScourseService.GetGradePoints(c.Code,studentId) * c.CreditHours).Sum();
                var totalCreditHours = courses.Select(c =>  c.CreditHours).Sum();

                return totalGradePoints / (totalCreditHours > 0 ? totalCreditHours : -1);
            }
            else { return -1; }
        }
        public bool IdIsUnique(int x)
        {
            bool check = true;
            if(x==0)
            {
                check = false;
            }
            if (FakeDatabase.People.Any())
            {
                foreach (var person in FakeDatabase.People)
                {
                    if (person.id == x)
                    {
                        check = false;
                    }
                }
            }
            
            return check;
        }
        public void RemoveStudent(Person person1)
        {
            for(int i=0;i<FakeDatabase.People.Count;i++)
            {
                if (FakeDatabase.People[i]==person1)
                {
                    FakeDatabase.People.RemoveAt(i);
                }
            }
        }
        public Person? GetById (int id)
        {
            foreach(Person person in FakeDatabase.People)
            {
                if(person.id == id)
                {
                    return person;
                }
            }
            return null;
        }
    }
}
