using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Tabs
{
    public class Submission
    {
        private static int lastId = 0;
        public int Id
        {
            get; private set;
        }
        public Student? Student { get; set; }
        public Assignment? Assignment { get; set; }
        public string Content { get; set; }
        public decimal Grade { get; set; }
        public Submission()
        {
            Id = ++lastId;
            Content = string.Empty;
        }
        public override string ToString ()
        {
            return $"Id:{Id} - Grade: {Grade} - {Student.Name} - {Assignment.Name}";
        }
    }
}
