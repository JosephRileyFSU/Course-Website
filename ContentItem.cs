using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Tabs
{
    public class ContentItem
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        private static int lastId = 0;

        public int Id
        {
            get; private set;
        }
        public ContentItem()
        {
            Id = ++lastId;
        }
        public override string ToString()
        {
            return $"{Name}: {Description}";
        }
    }
}
