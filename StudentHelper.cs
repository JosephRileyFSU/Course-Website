using Assignment2_Solution;
using Library.project.Services;
using Library.project.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.project.Services
{
    public class StudentHelper
    {
        private StudentService studentService;
        private CourseService courseService;
        private ListNavigator<Student> StudentNavigator;
        public StudentHelper(StudentService studentServiceReference, CourseService CourseServiceReference)
        {
            studentService= studentServiceReference;
            courseService = CourseServiceReference;
            StudentNavigator = new ListNavigator<Student>(studentService.Students);
        }
        public void CreateStudent()
        {
            Student.StudentYear studentyear;
            string studentname;
            string input;
            int studentid;
            Console.WriteLine("Create a person and add them to the list of people...");
            Console.WriteLine("Choose a person to create: 1.Student, 2.TA, 3.Instructor");
            int choice = int.Parse(Console.ReadLine()??"1");
            Console.WriteLine("Enter a name for the person");
            studentname = Console.ReadLine() ?? "None";
            Console.WriteLine("Enter the id for the person");
            studentid = int.Parse(Console.ReadLine() ?? "0");
            while(!studentService.IdIsUnique(studentid))
            {
                Console.WriteLine("Id is not unique, enter another one");
                studentid = int.Parse(Console.ReadLine()??"0");
            }
            if (choice == 1)
            {
                Console.WriteLine("Enter a classification for the student");
                Console.WriteLine("1-Freshman, 2-Sophmore, 3-Junior, 4-Senior.");
                input = Console.ReadLine() ?? "";
                studentyear = new Student.StudentYear();
                if (input == "1")
                {
                    studentyear = Student.StudentYear.Freshman;
                }
                if (input == "2")
                {
                    studentyear = Student.StudentYear.Sophmore;
                }
                if (input == "3")
                {
                    studentyear = Student.StudentYear.Junior;
                }
                if (input == "4")
                {
                    studentyear = Student.StudentYear.Senior;
                }
                Student student = new Student();
                student.Name = studentname;
                student.id = studentid;
                student.Classification = studentyear;
                studentService.AddStudent(student);
            }
            if(choice == 2)
            {
                TA ta = new TA();
                ta.Name = studentname;
                ta.id = studentid;
                studentService.AddTA(ta);
            }
            if (choice == 3)
            {
                Instructor instructor = new Instructor();
                instructor.Name = studentname;
                instructor.id = studentid;
                studentService.AddInstructor(instructor);
            }
            Console.WriteLine("Person successfully added.\n");
        }

        public Person? NavigatePeople()
        {
            bool check = true;
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no people in the people list\n");
                check = false;
            }
            while (check)
            {
                foreach (var pair in StudentNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }
                if (StudentNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous Page");
                }
                if (StudentNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next Page");
                }
                Console.WriteLine("Q. Quit");
                Console.WriteLine("Make a selection");
                var choice = Console.ReadLine() ?? "Q";
                if(choice.Equals("P", StringComparison.InvariantCultureIgnoreCase) || choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    if(choice.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                    {
                        StudentNavigator.GoBackward();
                    }
                    else if (choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        StudentNavigator.GoForward();
                    }
                }
                else if(!choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var student in studentService.Students)
                    {
                        if (student.id == int.Parse(choice))
                        {
                            return student;
                        }
                    }
                }
                else if(choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    check = false;
                }
            }
            return null;
        }
        public void ListStudents()
        {
            NavigatePeople();
        }
        public void SeachStudent()
        {
            Console.WriteLine("Search for a person by id...");
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no people in the persons list\n");
            }
            else
            {
                string str1;
                Console.WriteLine("Enter the id of the person you are searching for.");
                str1 = Console.ReadLine() ?? "None";
                foreach (var student in studentService.Students)
                {
                    if (student.id== int.Parse(str1))
                    {
                        Console.WriteLine(student);
                    }
                }
            }
        }
        public void UpdateStudent()
        {
            Console.WriteLine("Update a persons information...");
            int id;
            string str8;
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no people in the student list\n");
            }
            else
            {
                Console.WriteLine("Select a person to update their information (Enter their id number).\n");
                Person? person = NavigatePeople();
                if (person != null)
                {
                    id = person.id;
                    foreach (var student1 in studentService.Students)
                    {
                        if (student1.id == id)
                        {
                            Console.WriteLine("Enter an updated name for the person.");
                            str8 = Console.ReadLine() ?? "None";
                            student1.Name = str8;
                            if (student1 is Student)
                            {
                                UpdateStudentClass(student1 as Student);
                            }

                            Console.WriteLine("Enter an updated id for the person.");
                            str8 = Console.ReadLine() ?? "None";
                            while (!studentService.IdIsUnique(int.Parse(str8)))
                            {
                                Console.WriteLine("Id is not unique, enter another one");
                                str8 = Console.ReadLine() ?? "0";
                            }

                            student1.id = int.Parse(str8);
                            Console.WriteLine("Successfully updated student information.\n");
                        }
                    }
                }
            }
        }
        public void UpdateStudentClass(Student stud)
        {
            string str8;
            Console.WriteLine("Enter an updated classification for the student.");
            Console.WriteLine("1-Freshman, 2-sophmore, 3-junior, 4-senior.");
            str8 = Console.ReadLine() ?? "";
            Student.StudentYear studentyear;
            studentyear = new Student.StudentYear();
            if (str8 == "1")
            {
                studentyear = Student.StudentYear.Freshman;
            }
            if (str8 == "2")
            {
                studentyear = Student.StudentYear.Sophmore;
            }
            if (str8 == "3")
            {
                studentyear = Student.StudentYear.Junior;
            }
            if (str8 == "4")
            {
                studentyear = Student.StudentYear.Senior;
            }
            stud.Classification = studentyear;
        }

        
        public void GetStudentGPA()
        {
            Console.WriteLine("Get student GPA...");
            Console.WriteLine("Enter a student id to get the GPA for");
            int id;
            Person? person = NavigatePeople();
            if (person != null)
            {
                id = person.id;
                foreach (var student1 in studentService.Students)
                {
                    if (student1.id == id)
                    {
                        Console.WriteLine($"GPA: {studentService.GetGPA(id)}");
                    }
                }
            }
        }
    }
}
