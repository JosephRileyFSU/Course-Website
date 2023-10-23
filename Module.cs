using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Tabs
{
    public class Module
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ContentItem> Content { get; set; }
        private static int lastId = 0;

        public int Id
        {
            get; private set;
        }
        public Module() 
        {
            Content = new List<ContentItem>();
            Id = ++lastId;
        }
    }
}
