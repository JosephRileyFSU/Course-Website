using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Tabs
{
    public class Student : Person
    {
        public StudentYear Classification { get; set; }
        public Dictionary<int, double> Grades { get; set; }

        public Student()
        {
            Grades = new Dictionary<int, double>();
        }
        public override string ToString()
        {
            return $"{Name} - {Classification} - {id}";
        }
        public enum StudentYear
        {
            Freshman, Sophmore, Junior, Senior
        }
    }
}
