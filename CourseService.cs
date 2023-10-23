using Library.project.Database;
using Library.project.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Services
{
    public class CourseService
    {
        private static CourseService? instance;
        public static CourseService Current
        {
            get
            {
                if(instance == null)
                {
                    instance= new CourseService();
                }
                return instance;
            }
        }
        private CourseService()
        {

        }
        public void Add(Course course)
        {
            FakeDatabase.Course.Add(course);
        }
        public void RemoveCourse(Course course1)
        {
            for (int i = 0; i < FakeDatabase.Course.Count; i++)
            {
                if (FakeDatabase.Course[i] == course1)
                {
                    FakeDatabase.Course.RemoveAt(i);
                }
            }
        }

        public IEnumerable<Course?> Courses
        {
            get
            {
                return FakeDatabase.Course;
            }
        }


        public bool containsCourses()
        {
            bool x;
            if (FakeDatabase.Course.Count > 0)
            {
                x = true;
            }
            else
            {
                x = false;
            }

            return x;
        }
        public string GetLetterGrade(decimal grade)
        {
            if (grade >= 93)
            {
                return "A";
            }
            if (grade < 93 && grade >= 90)
            {
                return "A-";
            }
            if (grade < 90 && grade >= 87)
            {
                return "B+";
            }
            if (grade < 87 && grade >= 83)
            {
                return "B";
            }
            if (grade < 83 && grade >= 80)
            {
                return "B-";
            }
            if (grade < 80 && grade >= 77)
            {
                return "C+";
            }
            if (grade < 77 && grade >= 73)
            {
                return "C";
            }
            if (grade < 73 && grade >= 70)
            {
                return "C-";
            }
            if (grade < 70 && grade >= 60)
            {
                return "D";
            }
            if (grade < 60)
            {
                return "F";
            }
            return "I";
        }
        public decimal GetWeightedGrade(int courseId, int studentId)
        {
            var weightedAverage = -1M;
            foreach (Course course in Courses)
            {
                if (course != null)
                {
                    if (course.Code == courseId)
                    {
                        weightedAverage = 0M;
                        foreach (var group in course.AssignmentGroups)
                        {
                            var submissions = course.Submissions.Where(s => s.Student.id == studentId && group.Assignments.Select(a => a.Id).Contains(s.Assignment.Id));
                            if (submissions.Any())
                            {
                                weightedAverage += submissions.Select(s => s.Grade).Average() * group.Weight;
                            }
                        }
                    }
                }
            }
            return weightedAverage;
        }
        public decimal GetGradePoints (int courseId, int studentId)
        {
            return GetGradePoints(GetWeightedGrade(courseId, studentId));
        }
        public decimal GetGradePoints(decimal grade)
        {
            if (grade >= 93)
            {
                return 4m;
            }
            if (grade < 93 && grade >= 90)
            {
                return 3.7m;
            }
            if (grade < 90 && grade >= 87)
            {
                return 3.3m;
            }
            if (grade < 87 && grade >= 83)
            {
                return 3m;
            }
            if (grade < 83 && grade >= 80)
            {
                return 2.7m;
            }
            if (grade < 80 && grade >= 77)
            {
                return 2.3m;
            }
            if (grade < 77 && grade >= 73)
            {
                return 2m;
            }
            if (grade < 73 && grade >= 70)
            {
                return 1.7m;
            }
            if (grade < 70 && grade >= 60)
            {
                return 1m;
            }
            if (grade < 60)
            {
                return 0m;
            }
            return 0m;
        }
        public Course? getCourseByCode(int code)
        {
            foreach(Course? course in FakeDatabase.Course)
            {
                 if (course.Code==code)
                 {
                    return course;
                 }
            }
            return null;
        }
        public void AddCourse(Course? course)
        {
            if (course != null)
            {
                FakeDatabase.Course.Add(course);
            }
        }
        public bool CodeIsUnique(int x)
        {
            bool check = true;
            foreach (var course in CourseService.Current.Courses)
            {
                if (course != null)
                {
                    if (course.Code== x)
                    {
                        check = false;
                    }
                }
            }
            return check;
        }
    }
}
