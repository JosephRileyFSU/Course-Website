using System;
using System.Collections.Generic;
using Library.project.Services;
using Library.project.Tabs;
using LMS.project.Helpers;
using LMS.project.Services;

namespace LMS.project
{
    public class Program
    {
        static void Main(string[] args)
        {
            var studentSrvc = new StudentService();
            var courseSrvc = new CourseService();
            var studentHelper = new StudentHelper(studentSrvc, courseSrvc);
            var courseHelper = new CourseHelper(studentSrvc, courseSrvc);
            studentSrvc.ScourseService = courseSrvc;
            courseSrvc.CstudentService = studentSrvc;
            string choice;
            int option = 0;
            do
            {
                do
                {
                    Console.WriteLine("Select one of the following tabs:");
                    Console.WriteLine("1.Courses");
                    Console.WriteLine("2.Students");
                    Console.WriteLine("3.Exit");
                    choice = Console.ReadLine() ?? "3";
                    option = int.Parse(choice);
                    if(option < 1 || option > 3)
                    {
                        Console.WriteLine("Please select one of the given choices.");
                    }
                    if (option == 1)
                    {
                        Console.WriteLine("Courses...");
                        int x;
                        string str;
                        do
                        {
                            do
                            {
                                Console.WriteLine("Select one of the following options");
                                Console.WriteLine("1.Create a course");
                                Console.WriteLine("2.Add a student from the list of students to a specific course");
                                Console.WriteLine("3.Remove a student from a course's roster");
                                Console.WriteLine("4.List all courses");
                                Console.WriteLine("5.Search for courses by name or description");
                                Console.WriteLine("6.Update a course's information");
                                Console.WriteLine("7.Create an assignment group for a course");
                                Console.WriteLine("8.Update an assignment for a course");
                                Console.WriteLine("9.Remove an assignment from a course");
                                Console.WriteLine("10.Create a module for a course");
                                Console.WriteLine("11.Update a module for a course");
                                Console.WriteLine("12.Remove a module from a course");
                                Console.WriteLine("13.Create an announcement for a course");
                                Console.WriteLine("14.Update an announcement for a course");
                                Console.WriteLine("15.Remove an announcement from a course");
                                Console.WriteLine("16.Create a student submission");
                                Console.WriteLine("17.List all submissions for a course");
                                Console.WriteLine("18.Update a submission for a course");
                                Console.WriteLine("19.Remove a submission for a course");
                                Console.WriteLine("20.Grade a submission");
                                Console.WriteLine("21.Get a student grade");
                                Console.WriteLine("22.Back");
                                str = Console.ReadLine() ?? "22";
                                x = int.Parse(str);
                                if (x < 1 || x > 22)
                                {
                                    Console.WriteLine("Please select one of the given choices.");
                                }
                                if (x == 1)
                                {
                                    courseHelper.CreateCourse();
                                }
                                if (x == 2)
                                {
                                    courseHelper.AddToRoster();
                                }
                                if (x == 3)
                                {
                                    courseHelper.RemoveStudent();
                                }
                                if (x == 4)
                                {
                                    courseHelper.listCourses();
                                }
                                if(x==5)
                                {
                                    courseHelper.SearchCourse();
                                }
                                if(x==6)
                                {
                                    courseHelper.UpdateCourse();
                                }
                                if(x==7)
                                {
                                    courseHelper.CreateAssignmentWithGroup();
                                }
                                if (x == 8)
                                {
                                    courseHelper.UpdateAssignment();
                                }
                                if (x == 9)
                                {
                                    courseHelper.RemoveAssignment();
                                }
                                if ((x == 10))
                                {
                                    courseHelper.CreateModule();
                                }
                                if (x==11)
                                {
                                    courseHelper.UpdateModule();
                                }
                                if (x == 12)
                                {
                                    courseHelper.DeleteModule();
                                } 
                                if (x==13)
                                {
                                    courseHelper.CreateAnnouncement();
                                }
                                if(x == 14)
                                {
                                    courseHelper.UpdateAnnouncement();
                                }
                                if(x==15)
                                {
                                    courseHelper.DeleteAnnouncement();
                                }
                                if(x==16)
                                {
                                    courseHelper.AddSubmission();
                                }
                                if(x==17)
                                {
                                    courseHelper.ViewSubmissions();
                                }
                                if(x==18)
                                {
                                    courseHelper.UpdateSubmission();
                                }
                                if(x==19)
                                {
                                    courseHelper.RemoveSubmission();
                                }
                                if(x==20)
                                {
                                    courseHelper.GradeSubmission();
                                }
                                if(x==21)
                                {
                                    courseHelper.GetStudentGrade();
                                }
                            } while (x < 1 || x > 22) ;
                            } while (x != 22);
                        }
                    if(option==2)
                    {
                        Console.WriteLine("Students...");
                        int x;
                        string str;
                        do
                        {
                            do
                            {
                                Console.WriteLine("Select one of the following options");
                                Console.WriteLine("1.Create a person and add them to the list of people");
                                Console.WriteLine("2.List all students");
                                Console.WriteLine("3.Search for a student by Id");
                                Console.WriteLine("4.List all courses a student is taking");
                                Console.WriteLine("5.Update a person's information");
                                Console.WriteLine("6.Get a student's GPA");
                                Console.WriteLine("7.Back");
                                str = Console.ReadLine() ?? "7";
                                x = int.Parse(str);
                                if (x < 1 || x > 7)
                                {
                                    Console.WriteLine("Please select one of the given choices.");
                                }
                                if (x == 1)
                                {
                                    studentHelper.CreateStudent();
                                }
                                if(x==2)
                                {
                                    studentHelper.ListStudents();
                                }
                                if(x==3)
                                {
                                    studentHelper.SeachStudent();
                                }
                                if (x==4)
                                {
                                    courseHelper.ListCoursesForStudent();
                                }
                                if(x==5)
                                {
                                    studentHelper.UpdateStudent();
                                }
                                if(x==6)
                                {
                                    studentHelper.GetStudentGPA();
                                }
                            } while (x < 1 || x > 7);
                        } while (x != 7);
                    }
                } while (option < 1 || option > 3);
            } while (option != 3);
        }
    }
}