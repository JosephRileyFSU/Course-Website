namespace Library.project.Tabs
{
    public class Course
    {
        public int Code { get; private set; }

        private static int lastCode;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Semester { get; set; }
        public string RoomLocation { get; set; }
        public List<Person> Roster { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        public IEnumerable<Assignment>Assignments
        {
            get
            {
                return AssignmentGroups.SelectMany(ag=>ag.Assignments);
            }
        }
        public List<Submission>Submissions { get; set; }
        public List<Module> Modules { get; set; }

        public List<Announcement> Announcements { get; set; }
        public int CreditHours { get; set; } 
        public Course() 
        { 
            Roster = new List<Person>();
            AssignmentGroups = new List<AssignmentGroup>();
            Modules = new List<Module>();
            Announcements = new List<Announcement>();
            Code = ++lastCode;
            Name =string.Empty;  
            Description=string.Empty;
            Submissions = new List<Submission>();
            CreditHours = 3;
            Semester = string.Empty;
            RoomLocation = string.Empty;
        }

        public override string ToString()
        {
            return $"Name: {Name} - Code: {Code} - Dates: {Semester} - Room Location: {RoomLocation}";
        }
    }
}