using Assignment2_Solution;
using Library.project.Services;
using Library.project.Tabs;
using LMS.project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LMS.project.Helpers
{
    public class CourseHelper
    {
        //private CourseService courseService = new CourseService();
        private CourseService courseService;
        private StudentService studentService;
        private ListNavigator<Student> StudentNavigatorC;
        private ListNavigator<Course> courseNavigator;

        public CourseHelper(StudentService studentServiceReference, CourseService courseServiceReference)
        {
            studentService = studentServiceReference;
            courseService = courseServiceReference;
            StudentNavigatorC = new ListNavigator<Student>(studentService.Students.ToList());
            courseNavigator = new ListNavigator<Course>(courseService.Courses.ToList());
        }
        public void CreateCourse()
        {
            Course course = new Course();
            string code;
            string name;
            string desc;
            Console.WriteLine("Create a course and add it to the list of courses...");
            Console.WriteLine("Enter a unique code for the course");
            code = Console.ReadLine() ?? string.Empty;
            while(!CodeIsUnique(code))
            {
                Console.WriteLine("Code is not unique, enter another one");
                code = Console.ReadLine() ?? string.Empty;
            }
            course.Code = code;
            Console.WriteLine("Enter a name for the course");
            name = Console.ReadLine() ?? string.Empty;
            course.Name = name;
            Console.WriteLine("Enter a description for the course");
            desc = Console.ReadLine() ?? string.Empty;
            course.Description = desc;
            courseService.Add(course);
        }
        public Person? NavigatePeopleC()
        {
            bool check = true;
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no people in the people list\n");
                check = false;
            }
            while (check)
            {
                foreach (var pair in StudentNavigatorC.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }
                if (StudentNavigatorC.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous Page");
                }
                if (StudentNavigatorC.HasNextPage)
                {
                    Console.WriteLine("N. Next Page");
                }
                Console.WriteLine("Q. Quit");
                Console.WriteLine("Make a selection");
                var choice = Console.ReadLine() ?? "Q";
                if (choice.Equals("P", StringComparison.InvariantCultureIgnoreCase) || choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (choice.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                    {
                        StudentNavigatorC.GoBackward();
                    }
                    else if (choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        StudentNavigatorC.GoForward();
                    }
                }
                else if (!choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var student in studentService.Students)
                    {
                        if (student.id == int.Parse(choice))
                        {
                            return student;
                        }
                    }
                }
                else if (choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    check = false;
                }
            }
            return null;
        }
        public Course? CourseNavigator()
        {
            bool check = true;
            if (!courseService.containsCourses())
            {
                Console.WriteLine("There are currently no courses in the course list\n");
                check = false;
            }
            while (check)
            {
                foreach (var pair in courseNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }
                if (courseNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous Page");
                }
                if (courseNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next Page");
                }
                Console.WriteLine("Q. Quit");
                Console.WriteLine("Make a selection");
                var choice = Console.ReadLine() ?? "Q";
                if (choice.Equals("P", StringComparison.InvariantCultureIgnoreCase) || choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (choice.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                    {
                        courseNavigator.GoBackward();
                    }
                    else if (choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        courseNavigator.GoForward();
                    }
                }
                else if (!choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var course in courseService.Courses)
                    {
                        if (course.Code.Equals(choice))
                        {
                            return course;
                        }
                    }
                }
                else if (choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    check = false;
                }
            }
            return null;
        }
        public void listCourses()
        {
            CourseNavigator();
        }
        public void UpdateCourse()
        {
            string str9;
            Console.WriteLine("Update a course's information...");
            Console.WriteLine("Select a course code to update that course's information.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                str9 = thisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str9))
                    {
                        string input;
                        int choice;
                        Console.WriteLine("Do you want to update the code of the course? (1-Yes, 2-No)");
                        input = Console.ReadLine() ?? "1";
                        choice = int.Parse(input);
                        if (choice == 1)
                        {
                            Console.WriteLine("Enter an updated code for the course.");
                            str9 = Console.ReadLine() ?? "None";
                            while (!CodeIsUnique(str9))
                            {
                                Console.WriteLine("Code is not unique, enter another one");
                                str9 = Console.ReadLine() ?? string.Empty;
                            }
                            course1.Code = str9;
                        }
                        Console.WriteLine("Do you want to update the name of the course? (1-Yes, 2-No)");
                        input = Console.ReadLine() ?? "1";
                        choice = int.Parse(input);
                        if (choice == 1)
                        {
                            Console.WriteLine("Enter an updated name for the course");
                            str9 = Console.ReadLine() ?? "None";
                            course1.Name = str9;
                        }
                        Console.WriteLine("Do you want to update the description of the course? (1-Yes, 2-No)");
                        input = Console.ReadLine() ?? "1";
                        choice = int.Parse(input);
                        if (choice == 1)
                        {
                            Console.WriteLine("Enter an updated description for the course");
                            str9 = Console.ReadLine() ?? "None";
                            course1.Description = str9;
                        }
                    }
                }
            }
        }
        public void SearchCourse()
        {
            Console.WriteLine("Search for courses by name or description...");
            if (!courseService.containsCourses())
            {
                Console.WriteLine("There are currently no courses in the course list\n");
            }
            else
            {
                string str1;
                Console.WriteLine("Enter the name or description of the course you are searching for.");
                str1 = Console.ReadLine() ?? "None";
                foreach (var course in courseService.Courses)
                {
                    if (course.Name.Equals(str1) || course.Description.Equals(str1))
                    {
                        Console.WriteLine($"Course Name: {course.Name}, Code:{course.Code} Description: {course.Description}.");
                        if (course.Roster.Count > 0)
                        {
                            Console.WriteLine("Roster: ");
                            foreach (var student in course.Roster)
                            {
                                Console.WriteLine(student.Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No students are currently enrolled in this course.\n");
                        }
                        if (course.AssignmentGroups.Count > 0)
                        {
                            Console.WriteLine("Assignments: ");
                            Console.WriteLine("Assignment Name, Description, Due date, and Total avaliable points. ");
                            foreach (AssignmentGroup assignmentGroup in course.AssignmentGroups)
                            {
                                foreach (Assignment assignment in assignmentGroup.Assignments)
                                {
                                    Console.WriteLine(assignment.Name);
                                    Console.WriteLine(assignment.Description);
                                    Console.WriteLine(assignment.DueDate);
                                    Console.WriteLine(assignment.TotalAvaliablePoints);
                                    Console.WriteLine("");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No assignments are currently assigned for this course.\n");
                        }
                        if (course.Modules.Count > 0)
                        {
                            Console.WriteLine("Modules: ");
                            foreach (var module in course.Modules)
                            {
                                Console.WriteLine("Modules Name, Description, and Content. ");
                                Console.WriteLine(module.Name);
                                Console.WriteLine(module.Description);
                                module.Content.ForEach(x => Console.WriteLine(x));
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are currently no modules for this course.\n");
                        }
                        if (course.Announcements.Count > 0)
                        {
                            Console.WriteLine("Announcements: ");
                            foreach (var announcement in course.Announcements)
                            {
                                Console.WriteLine(announcement);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no current announcements for this course.\n");
                        }
                    }
                }
            }
        }
        public void AddToRoster()
        {
            string str3;
            Console.WriteLine("Add a student from the list of students to a specific course...");
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no students in the student list.\n");
            }
            else if (!courseService.containsCourses())
            {
                Console.WriteLine("There are currently no courses in the course list.\n");
            }
            else
            {
                Console.WriteLine("Select a course code to add a student to (enter in the course code)");
                Course? ThisCourse = CourseNavigator();
                if (ThisCourse != null)
                {
                    str3 = ThisCourse.Code;
                    foreach (var course in courseService.Courses)
                    {
                        if (course.Code.Equals(str3))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Select a student to add to this course(Enter student id).");
                            Person? person = NavigatePeopleC();
                            if (person != null)
                            {
                                int id = person.id;
                                foreach (var name2 in studentService.Students)
                                {
                                    if (name2.id == id)
                                    {
                                        course.Roster.Add(name2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void RemoveStudent()
        {
            Console.WriteLine("Remove a student from a course's roster...");
            string str6;
            string str7;
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no students in the student list.\n");
            }
            else if (!courseService.containsCourses())
            {
                Console.WriteLine("There are currently no courses in the course list.\n");
            }
            else
            {
                Console.WriteLine("Select a course code to remove a student from(enter course code).");
                Course? ThisCourse = CourseNavigator();
                if (ThisCourse != null)
                {
                    str6 = ThisCourse.Code;
                    foreach (var course in courseService.Courses)
                    {
                        if (course.Code.Equals(str6))
                        {
                            if (course.Roster.Count == 0)
                            {
                                Console.WriteLine("No students are currently on this course's roster.\n");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Select a student to remove from this course(enter in their id).");
                                foreach (var student in course.Roster)
                                {
                                    Console.WriteLine(student);
                                }
                                str7 = Console.ReadLine() ?? "None";
                                foreach (var students in studentService.Students)
                                {
                                    if (students.id == (int.Parse(str7)))
                                    {
                                        course.Roster.Remove(students);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void ListCoursesForStudent()
        {
            Console.WriteLine("List all courses a student is taking...");
            if (!studentService.containsStudents())
            {
                Console.WriteLine("There are currently no students in the student list\n");
            }
            else
            {
                Console.WriteLine("Enter the Id of the student.\n");
                Person? person = NavigatePeopleC();
                if (person != null)
                {
                    int id = person.id;
                    foreach (var name2 in studentService.Students)
                    {
                        if (name2.id == id)
                        {
                            foreach (var courses in courseService.Courses)
                            {
                                foreach (var roster in courses.Roster)
                                {
                                    if (name2 == roster)
                                    {
                                        Console.WriteLine(courses);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public Assignment CreateAssignment()
        {
            string str8;
            string str9;
            Assignment assignment = new Assignment();
            string input;
            Console.WriteLine("Enter a name for the assignment.");
            str8 = Console.ReadLine() ?? "None";
            assignment.Name = str8;
            Console.WriteLine("Enter a description for the assignment.");
            str8 = Console.ReadLine() ?? "None";
            assignment.Description = str8;
            Console.WriteLine("Enter the total number of points avaliable for the assignment.");
            input = Console.ReadLine() ?? "";
            assignment.TotalAvaliablePoints = decimal.Parse(input);
            Console.WriteLine("Enter a due date for the assignment.");
            str9 = Console.ReadLine() ?? "None";
            assignment.DueDate = DateTime.Parse(str9);
            return assignment;
        }
        public void UpdateAssignment()
        {
            string str8;
            string str9;
            string str2;
            string str3;
            Console.WriteLine("Update an assignment for a course...");
            Console.WriteLine("Select a course code to Update an assignment for the course.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code; 
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        string input;
                        Console.WriteLine("Enter an assignment Groups Id to update");
                        foreach (AssignmentGroup assignmentGroup in course1.AssignmentGroups)
                        {
                            Console.WriteLine(assignmentGroup);
                        }
                        str9 = Console.ReadLine() ?? "None";
                        foreach (AssignmentGroup assignmentGroup in course1.AssignmentGroups)
                        {
                            if (assignmentGroup.Id == int.Parse(str9))
                            {
                                Console.WriteLine("Enter an assignment id to update");
                                foreach (var assignment in assignmentGroup.Assignments)
                                {
                                    Console.WriteLine(assignment);
                                }
                                str2=Console.ReadLine() ?? "None";
                                foreach(var assignment in assignmentGroup.Assignments)
                                { 
                                    if (assignment.Id == int.Parse(str2))
                                    {
                                        Console.WriteLine("Do you want to update the Name of the assignment?(1-Yes, 2-No)");
                                        int choice = int.Parse(Console.ReadLine() ?? "2");
                                        if (choice == 1)
                                        {
                                            Console.WriteLine("Enter the updated name for the assignment.");
                                            str3 = Console.ReadLine() ?? "none";
                                            assignment.Name = str3;
                                        }
                                        Console.WriteLine("Do you want to update the description of the assignment?(1-Yes, 2-No)");
                                        choice = int.Parse(Console.ReadLine() ?? "2");
                                        if (choice == 1)
                                        {
                                            Console.WriteLine("Enter an updated description for the assignment.");
                                            str3 = Console.ReadLine() ?? "None";
                                            assignment.Description = str3;
                                        }
                                        Console.WriteLine("Do you want to update the total number of points for the assignment?(1-Yes, 2-No)");
                                        choice = int.Parse(Console.ReadLine() ?? "2");
                                        if (choice == 1)
                                        {
                                            Console.WriteLine("Enter the updated total number of points avaliable for the assignment.");
                                            input = Console.ReadLine() ?? "";
                                            assignment.TotalAvaliablePoints = decimal.Parse(input);
                                        }
                                        Console.WriteLine("Do you want to update the due-date for the assignment?(1-Yes, 2-No)");
                                        choice = int.Parse(Console.ReadLine() ?? "2");
                                        if (choice == 1)
                                        {
                                            Console.WriteLine("Enter the updated due-date for the assignment.");
                                            str3= Console.ReadLine() ?? "None";
                                            assignment.DueDate = DateTime.Parse(str9);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void RemoveAssignment()
        {
            string str8;
            string str9;
            string str2;
            Console.WriteLine("Remove an assignment from a course...");
            Console.WriteLine("Select a course code to Remove an assignment for the course.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    { 
                        Console.WriteLine("Enter an assignment group Id to remove an assignment from");
                        foreach (AssignmentGroup assignmentGroup in course1.AssignmentGroups)
                        {
                            Console.WriteLine(assignmentGroup.Id);
                        }
                        str9 = Console.ReadLine() ?? "None";
                        foreach(AssignmentGroup assignmentGroup in course1.AssignmentGroups)
                        {
                            if(assignmentGroup.Id==int.Parse(str9))
                            {
                                Console.WriteLine("Select an assignment id to remove that assignment");
                                foreach(Assignment assignment in assignmentGroup.Assignments)
                                {
                                    Console.WriteLine(assignment);
                                }
                                str2 = Console.ReadLine() ?? "0";
                                for (int i = 0; i < course1.AssignmentGroups.Count; i++)
                                {
                                    if (course1.AssignmentGroups[i].Id == int.Parse(str9))
                                    {
                                        for (int j = 0; j < course1.AssignmentGroups[i].Assignments.Count; j++)
                                        {
                                            if (course1.AssignmentGroups[i].Assignments[j].Id == int.Parse(str2))
                                            {
                                                course1.AssignmentGroups[i].Assignments.RemoveAt(j);
                                            }
                                        }
                                               
                                    }
                                }
                  
                            }
                        }
                    }
                }
            }
        }
        public void CreateModule()
        {
            Console.WriteLine("Create a module for a course...");
            string str8;
            Console.WriteLine("Select a course code to create a module for.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        Console.WriteLine("What is the name of this module?");
                        var name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("What is the description of this module?");
                        var desc = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("What type of content would you like to add");
                        Console.WriteLine("1.Assignment, 2.File, 3. Page");
                        var input = int.Parse(Console.ReadLine() ?? "0");
                        if (input == 1)
                        {
                            var newContent = CreateAssignmentItem(course1);
                            if (newContent != null)
                            {
                                Module module = new Module() { Name = name, Description = desc };
                                module.Content.Add(newContent);
                                course1.Modules.Add(module);
                            }
                        }
                        if (input == 2)
                        {
                            var newFileItem = CreateFileItem(course1);
                            if (newFileItem != null)
                            {
                                Module module = new Module() { Name = name, Description = desc };
                                module.Content.Add(newFileItem);
                                course1.Modules.Add(module);
                            }
                        }
                        if (input == 3)
                        {
                            var newPageItem = CreatePageItem(course1);
                            if (newPageItem != null)
                            {
                                Module module = new Module() { Name = name, Description = desc };
                                module.Content.Add(newPageItem);
                                course1.Modules.Add(module);
                            }
                        }
                    }
                }
            }
        }
        private PageItem? CreatePageItem(Course course)
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Descrpition:");
            var description = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter page Content:");
            var body = Console.ReadLine() ?? string.Empty;

            return new PageItem
            {
                Name = name,
                Description = description,
                HTMLBody = body
            };
        }
        private FileItem? CreateFileItem(Course course)
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Descrpition:");
            var description = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter a path to the file");
            var filepath = Console.ReadLine() ?? string.Empty;

            return new FileItem
            {
                Name = name,
                Description = description,
                Path = filepath
            };
        }
        private AssignmentItem? CreateAssignmentItem(Course course)
        {
            string str8;
            string str9 ="-2";
            Console.WriteLine("Select an assignment group to choose an assignment");
            foreach(AssignmentGroup assignmentGroup in course.AssignmentGroups)
            {
                Console.WriteLine(assignmentGroup);
            }
            str8 = Console.ReadLine() ?? "0";
            Console.WriteLine("Which assignment should be added?");
            foreach(AssignmentGroup assignmentGroup in course.AssignmentGroups)
            {
                if (assignmentGroup.Id == int.Parse(str8))
                {
                    foreach (Assignment assignment in assignmentGroup.Assignments)
                    {
                        Console.WriteLine(assignment);
                    }
                    str9=Console.ReadLine() ?? "0";
                }
            }
            foreach (AssignmentGroup assignmentGroup in course.AssignmentGroups)
            {
                if (assignmentGroup.Id == int.Parse(str8))
                {
                    foreach (Assignment assignment in assignmentGroup.Assignments)
                    {
                        if(assignment.Id == int.Parse(str9))
                        {
                            Assignment newAssignment = assignment;
                            return new AssignmentItem { Assignment = assignment };
                        }
                    }
                }
            }
            return null;
        }
    public void DeleteModule()
        {
            string str8;
            Console.WriteLine("Remove a module from a course...");
            Console.WriteLine("Select a course code to remove a module from.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        Console.WriteLine("Modules");
                        Console.WriteLine("Module id - Module name");
                        foreach (var module in course1.Modules)
                        {
                            Console.WriteLine($"{module.Id} - {module.Name}");
                        }
                        Console.WriteLine("Enter the id of a module to delete");
                        int x = int.Parse(Console.ReadLine() ?? "0");
                        for (int i = 0; i < course1.Modules.Count; i++)
                        {
                            if (course1.Modules[i].Id == x)
                            {
                                course1.Modules.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }
        public bool CodeIsUnique(string x)
        {
            bool check = true;
                foreach (var course in courseService.Courses)
                {
                    if(course.Code.Equals(x))
                    {
                        check = false;
                    }
                }
                return check;
        }
        public void CreateAnnouncement()
        {
            string str8;
            Console.WriteLine("Create an announcement for a course...");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                Console.WriteLine("Select a course code to create an announcement for.\n");
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        Console.WriteLine("Enter the name of the announcement:");
                        var name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Enter the description of the announcement:");
                        var desc = Console.ReadLine() ?? string.Empty;
                        course1.Announcements.Add(new Announcement { Description = desc, Name = name });
                    }
                }
            }
        }
        public void DeleteAnnouncement()
        {
            string str8;
            Console.WriteLine("Remove an announcement from a course...");
            Console.WriteLine("Select a course code to delete an announcement for.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        Console.WriteLine("Announcements");
                        Console.WriteLine("Announcement id - Announcement name");
                        foreach (var announcement in course1.Announcements)
                        {
                            Console.WriteLine($"{announcement.Id} - {announcement.Name}");
                        }
                        Console.WriteLine("Enter the id of an announcement to delete");
                        int x = int.Parse(Console.ReadLine() ?? "0");
                        for (int i = 0; i < course1.Announcements.Count; i++)
                        {
                            if (course1.Announcements[i].Id == x)
                            {
                                course1.Announcements.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }
        public void UpdateAnnouncement()
        {
            string str8;
            Console.WriteLine("update an announcement for a course...");
            Console.WriteLine("Select a course code to update an announcement for.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        Console.WriteLine("Announcements");
                        Console.WriteLine("Announcement id - Announcement name");
                        foreach (var announcement in course1.Announcements)
                        {
                            Console.WriteLine($"{announcement.Id} - {announcement.Name}");
                        }
                        Console.WriteLine("Enter the id of an announcement to update");
                        int x = int.Parse(Console.ReadLine() ?? "0");
                        for (int i = 0; i < course1.Announcements.Count; i++)
                        {
                            if (course1.Announcements[i].Id == x)
                            {
                                int choice = 0;
                                Console.WriteLine("Update the name of the announcement? (1-yes, 2-no)");
                                choice = int.Parse(Console.ReadLine() ?? "1");
                                if (choice == 1)
                                {
                                    Console.WriteLine("Enter the updated name for the announcement");
                                    string name = Console.ReadLine() ?? "";
                                    course1.Announcements[i].Name = name;
                                }
                                choice = 0;
                                Console.WriteLine("Update the description of the announcement? (1-yes, 2-no)");
                                choice = int.Parse(Console.ReadLine() ?? "1");
                                if (choice == 1)
                                {
                                    Console.WriteLine("Enter the updated description for the announcement");
                                    string desc = Console.ReadLine() ?? "";
                                    course1.Announcements[i].Description = desc;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void UpdateModule()
        {
            string str8;
            Console.WriteLine("update a module for a course...");
            Console.WriteLine("Select a course code to update a module for.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        Console.WriteLine("Modules");
                        Console.WriteLine("Module id - Module name");
                        foreach (var module in course1.Modules)
                        {
                            Console.WriteLine($"{module.Id} - {module.Name}");
                        }
                        Console.WriteLine("Enter the id of a module to update");
                        int x = int.Parse(Console.ReadLine() ?? "0");
                        for (int i = 0; i < course1.Modules.Count; i++)
                        {
                            if (course1.Modules[i].Id == x)
                            {
                                int choice = 0;
                                Console.WriteLine("Update the name of the module? (1-yes, 2-no)");
                                choice = int.Parse(Console.ReadLine() ?? "1");
                                if (choice == 1)
                                {
                                    Console.WriteLine("Enter the updated name for the module");
                                    string name = Console.ReadLine() ?? "";
                                    course1.Modules[i].Name = name;
                                }
                                choice = 0;
                                Console.WriteLine("Update the description of the module? (1-yes, 2-no)");
                                choice = int.Parse(Console.ReadLine() ?? "1");
                                if (choice == 1)
                                {
                                    Console.WriteLine("Enter the updated description for the module");
                                    string desc = Console.ReadLine() ?? "";
                                    course1.Modules[i].Description = desc;
                                }
                                choice = 0;
                                Console.WriteLine("Update the content of the module? (1-yes, 2-no)");
                                choice = int.Parse(Console.ReadLine() ?? "1");
                                if (choice == 1)
                                {
                                    int ans = 0;
                                    Console.WriteLine("Add more content to the module? (1-yes, 2-no)");
                                    ans = int.Parse(Console.ReadLine() ?? "2");
                                    if (ans == 1)
                                    {
                                        AddContentToModule(course1, course1.Modules[i]);
                                    }
                                    ans = 0;
                                    Console.WriteLine("Update current content in the module? (1-yes, 2-no)");
                                    ans = int.Parse(Console.ReadLine() ?? "2");
                                    if (ans == 1)
                                    {
                                        UpdateModuleContent(course1, course1.Modules[i]);
                                    }
                                    ans = 0;
                                    Console.WriteLine("remove current content in the module? (1-yes, 2-no)");
                                    ans = int.Parse(Console.ReadLine() ?? "2");
                                    if (ans == 1)
                                    {
                                        RemoveModuleContent(course1.Modules[i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void AddContentToModule(Course course,Module module)
        {
            Console.WriteLine("What type of content would you like to add");
            Console.WriteLine("1.Assignment, 2.File, 3. Page");
            var input = int.Parse(Console.ReadLine() ?? "0");
            if (input == 1)
            {
                var newContent = CreateAssignmentItem(course);
                if (newContent != null)
                {
                    module.Content.Add(newContent);
                }
            }
            if (input == 2)
            {
                var newFileItem = CreateFileItem(course);
                if (newFileItem != null)
                {
                    module.Content.Add(newFileItem);
                }
            }
            if (input == 3)
            {
                var newPageItem = CreatePageItem(course);
                if (newPageItem != null)
                {
                    module.Content.Add(newPageItem);
                }
            }
        }
        public void UpdateModuleContent(Course course, Module module)
        {
            Console.WriteLine("Module Content");
            Console.WriteLine("Content id - Content name");
            foreach (ContentItem content in module.Content)
            {
                Console.WriteLine($"{content.Id} - {content.Name}");
            }
            Console.WriteLine("Enter the id of the content to update");
            int x = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < module.Content.Count; i++)
            {
                if (module.Content[i].Id == x)
                {
                    int choice = 0;
                    Console.WriteLine("Update the name of the content? (1-yes, 2-no)");
                    choice = int.Parse(Console.ReadLine() ?? "1");
                    if (choice == 1)
                    {
                        Console.WriteLine("Enter the updated name for the content");
                        string name = Console.ReadLine() ?? "";
                        module.Content[i].Name = name;
                    }
                    choice = 0;
                    Console.WriteLine("Update the description of the content? (1-yes, 2-no)");
                    choice = int.Parse(Console.ReadLine() ?? "1");
                    if (choice == 1)
                    {
                        Console.WriteLine("Enter the updated description for the content");
                        string desc = Console.ReadLine() ?? "";
                        module.Content[i].Description = desc;
                    }
                    choice = 0;
                    Console.WriteLine("Update the type of content? (1-yes, 2-no)");
                    choice = int.Parse(Console.ReadLine() ?? "1");
                    if (choice == 1)
                    {
                        Console.WriteLine("Which type of content would you like to update the content to?");
                        Console.WriteLine("1.Assignment, 2.File, 3. Page");
                        var input = int.Parse(Console.ReadLine() ?? "0");
                        if (input == 1)
                        {
                            var newContent = CreateAssignmentItem(course);
                            if (newContent != null)
                            {
                                module.Content[i] = newContent;
                            }
                        }
                        if (input == 2)
                        {
                            var newFileItem = CreateFileItem(course);
                            if (newFileItem != null)
                            {
                                module.Content[i]= newFileItem;
                            }
                        }
                        if (input == 3)
                        {
                            var newPageItem = CreatePageItem(course);
                            if (newPageItem != null)
                            {
                                module.Content[i] = newPageItem;
                            }
                        }
                    }
                }
            }
        }
        public void RemoveModuleContent(Module module)
        {
            Console.WriteLine("Module Content");
            Console.WriteLine("Content id - Content name");
            foreach (ContentItem content in module.Content)
            {
                Console.WriteLine($"{content.Id} - {content.Name}");
            }
            Console.WriteLine("Enter the id of the content to remove");
            int x = int.Parse(Console.ReadLine() ?? "0");
            for (int i = 0; i < module.Content.Count; i++)
            {
                if (module.Content[i].Id == x)
                {
                    module.Content.RemoveAt(i);
                }
            }
        }
        public void CreateAssignmentWithGroup()
        {
            string str8;
            int choice;
            Console.WriteLine("Create Assignment group for a course...");
            Console.WriteLine("Select a course code to modify an assignment group for.\n");
            Course? ThisCourse = CourseNavigator();
            if (ThisCourse != null)
            {
                str8 = ThisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str8))
                    {
                        if(course1.AssignmentGroups.Any())
                        {
                            Console.WriteLine("0 - Add a new group");
                            course1.AssignmentGroups.ForEach(Console.WriteLine);
                            choice = int.Parse(Console.ReadLine() ?? "0");
                            if(choice==0)
                            {
                                var newGroup = new AssignmentGroup();
                                Console.WriteLine("Group Name:");
                                newGroup.Name = Console.ReadLine() ?? string.Empty;
                                Console.WriteLine("Group Weight:");
                                newGroup.Weight = decimal.Parse(Console.ReadLine() ?? "1");
                                Assignment assignment = new Assignment();
                                Console.WriteLine("Enter a name for the assignment.");
                                string name = Console.ReadLine() ?? "None";
                                assignment.Name = name;
                                Console.WriteLine("Enter a description for the assignment.");
                                string desc = Console.ReadLine() ?? "None";
                                assignment.Description = desc;
                                Console.WriteLine("Enter the total number of points avaliable for the assignment.");
                                string input = Console.ReadLine() ?? "";
                                assignment.TotalAvaliablePoints = decimal.Parse(input);
                                Console.WriteLine("Enter a due date for the assignment.");
                                string date = Console.ReadLine() ?? "None";
                                assignment.DueDate = DateTime.Parse(date);
                                newGroup.Assignments.Add(assignment);
                                course1.AssignmentGroups.Add(newGroup);
                            }
                            else if(choice!=0)
                            {
                                foreach(AssignmentGroup assignmentGroup in course1.AssignmentGroups)
                                {
                                    if(assignmentGroup.Id==choice)
                                    {
                                        Assignment assignment1 = new Assignment();
                                        Console.WriteLine("Enter a name for the assignment.");
                                        string name1 = Console.ReadLine() ?? "None";
                                        assignment1.Name = name1;
                                        Console.WriteLine("Enter a description for the assignment.");
                                        string desc1 = Console.ReadLine() ?? "None";
                                        assignment1.Description = desc1;
                                        Console.WriteLine("Enter the total number of points avaliable for the assignment.");
                                        string input1 = Console.ReadLine() ?? "";
                                        assignment1.TotalAvaliablePoints = decimal.Parse(input1);
                                        Console.WriteLine("Enter a due date for the assignment.");
                                        string date1 = Console.ReadLine() ?? "None";
                                        assignment1.DueDate = DateTime.Parse(date1);
                                        assignmentGroup.Assignments.Add(assignment1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var newGroup = new AssignmentGroup();
                            Console.WriteLine("Group Name:");
                            newGroup.Name = Console.ReadLine() ?? string.Empty;
                            Console.WriteLine("Group Weight:");
                            newGroup.Weight = decimal.Parse(Console.ReadLine() ?? "1");
                            Assignment assignment = new Assignment();
                            Console.WriteLine("Enter a name for the assignment.");
                            string name = Console.ReadLine() ?? "None";
                            assignment.Name = name;
                            Console.WriteLine("Enter a description for the assignment.");
                            string desc = Console.ReadLine() ?? "None";
                            assignment.Description = desc;
                            Console.WriteLine("Enter the total number of points avaliable for the assignment.");
                            string input = Console.ReadLine() ?? "";
                            assignment.TotalAvaliablePoints = decimal.Parse(input);
                            Console.WriteLine("Enter a due date for the assignment.");
                            string date = Console.ReadLine() ?? "None";
                            assignment.DueDate = DateTime.Parse(date);
                            newGroup.Assignments.Add(assignment);
                            course1.AssignmentGroups.Add(newGroup);
                        }
                    }
                }
            }
        }
        public void AddSubmission()
        {
            Course course1 = new Course();
            Student student1 = new Student();
            Assignment assignment1 = new Assignment();
            bool check = false;
            string str9;
            Console.WriteLine("Select a course code to add a submission to.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                check = true;
                str9 = thisCourse.Code;
                Console.WriteLine("Enter the id for the student");
                foreach (Course course in courseService.Courses)
                {
                    if (course.Code.Equals(str9))
                    {
                        course1 = course;
                        foreach (Student student in course.Roster)
                        {
                            Console.WriteLine(student);
                        }
                    }
                }
                string studId = Console.ReadLine() ?? string.Empty;
                foreach (Course course in courseService.Courses)
                {
                    if (course.Code.Equals(str9))
                    {
                        foreach (Student student in course.Roster)
                        {
                            if (student.id == int.Parse(studId))
                            {
                                student1 = student;
                            }
                        }
                    }
                }
                Console.WriteLine("Enter the id for the assignment");
                foreach (Course course in courseService.Courses)
                {
                    if (course.Code.Equals(str9))
                    {
                        foreach (AssignmentGroup assignmentGroup in course.AssignmentGroups)
                        {
                            foreach (Assignment assignment in assignmentGroup.Assignments)
                            {
                                Console.WriteLine(assignment);
                            }
                        }
                    }
                }
                int assignId = int.Parse(Console.ReadLine() ?? "0");
                foreach (Course course in courseService.Courses)
                {
                    if (course.Code.Equals(str9))
                    {
                        foreach (AssignmentGroup assignmentGroup in course.AssignmentGroups)
                        {
                            foreach (Assignment assignment in assignmentGroup.Assignments)
                            {
                                if (assignment.Id == assignId)
                                {
                                    assignment1 = assignment;
                                }
                            }
                        }
                    }
                }
            }
            if (check)
            {
                CreateSubmission(course1, student1, assignment1);
            }
        }
        public void CreateSubmission(Course course, Student student, Assignment assignment)
        {
            Console.WriteLine("What is the content of the submission?");
            var content = Console.ReadLine() ?? string.Empty;
            course.Submissions.Add(new Submission { Assignment = assignment, Student=student, Content=content });
        }
        public void ViewSubmissions()
        {
            string str9;
            Console.WriteLine("View all submissions for a course");
            Console.WriteLine("Select a course code to view all submissions for.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                str9 = thisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str9))
                    {
                        foreach (Submission submission in course1.Submissions)
                        {
                            Console.WriteLine(submission);
                        }
                    }
                }
            }
        }
        public void RemoveSubmission()
        {
            string str9;
            string subId;
            Console.WriteLine("Remove a submission for a course");
            Console.WriteLine("Select a course code to remove a submission for.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                str9 = thisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str9))
                    {
                        Console.WriteLine("Select a submission id to remove from this course");
                        foreach(Submission submission in course1.Submissions)
                        {
                            Console.WriteLine(submission);
                        }
                        subId = Console.ReadLine() ?? "0";
                        for(int i=0;i<course1.Submissions.Count;i++)
                        {
                            if (course1.Submissions[i].Id==int.Parse(subId))
                            {
                                course1.Submissions.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }
        public void UpdateSubmission()
        {
            string str9;
            string subId;
            string content;
            Console.WriteLine("Update a submission for a course");
            Console.WriteLine("Select a course code to update a submission for.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                str9 = thisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str9))
                    {
                        Console.WriteLine("Select a submission id to update from this course");
                        foreach (Submission submission in course1.Submissions)
                        {
                            Console.WriteLine(submission);
                        }
                        subId = Console.ReadLine() ?? "0";
                        foreach (Submission submission in course1.Submissions)
                        {
                            if(submission.Id==int.Parse(subId))
                            {
                                Console.WriteLine("Enter the updated content for the submission");
                                content = Console.ReadLine()?? String.Empty;
                                submission.Content = content;
                            }
                        }
                    }
                }
            }
        }
        public void GradeSubmission()
        {
            string str9;
            string subId;
            string grade;
            Console.WriteLine("Grade a submission for a course");
            Console.WriteLine("Select a course code to grade a submission for.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                str9 = thisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str9))
                    {
                        Console.WriteLine("Select a submission id to grade from this course");
                        foreach (Submission submission in course1.Submissions)
                        {
                            Console.WriteLine(submission);
                        }
                        subId = Console.ReadLine() ?? "0";
                        foreach (Submission submission in course1.Submissions)
                        {
                            if (submission.Id == int.Parse(subId))
                            {
                                Console.WriteLine("Enter the grade for the submission");
                                grade = Console.ReadLine() ?? "0";
                                submission.Grade = decimal.Parse(grade);
                            }
                        }
                    }
                }
            }
        }
        public void GetStudentGrade()
        {
            string str9;
            string studId = "-1";
            decimal grade;
            Console.WriteLine("Get a students grade for a course");
            Console.WriteLine("Select a course code to get a students grade for.\n");
            Course? thisCourse = CourseNavigator();
            if (thisCourse != null)
            {
                str9 = thisCourse.Code;
                foreach (var course1 in courseService.Courses)
                {
                    if (course1.Code.Equals(str9))
                    {
                        Console.WriteLine("Select a student id to get a grade for");
                        foreach(Person student in course1.Roster)
                        {
                            if (student is Student)
                            {
                                Console.WriteLine(student);
                            }
                        }
                        studId = Console.ReadLine() ?? "-1";
                    }
                }
                if (studId != "-1")
                {
                    grade = courseService.GetWeightedGrade(str9, int.Parse(studId));
                    str9 = courseService.GetLetterGrade(grade);
                    Console.WriteLine($"Student Grade: ({str9}) - {grade}");
                }
            }
        }
    }
}
