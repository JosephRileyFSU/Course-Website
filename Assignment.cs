using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Tabs
{
    public class Assignment
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        private static int lastId = 0;
        public int Id 
        {
            get;private set;
        }
        public Decimal TotalAvaliablePoints { get; set; }
        public DateTime DueDate { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Name} - {DueDate}";
        }
        public Assignment() 
        {
            Id = ++lastId;
        }
    }
}
