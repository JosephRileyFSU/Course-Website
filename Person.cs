using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Tabs
{
    public class Person
    {
        public int id { get; private set; }
        private static int lastId;
        public string Name { get; set; }
        public Person() 
        {
            Name= string.Empty;
            id = ++lastId;
        }
        public override string ToString()
        {
            return $"{Name} - {id}";
        }
    }
}
