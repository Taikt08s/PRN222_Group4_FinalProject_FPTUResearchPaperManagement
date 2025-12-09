using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Major { get; set; }

        public int Semester_Id { get; set; }
        public Guid Created_By { get; set; }

        public bool Is_Group_Required { get; set; }
        public DateTime Deadline_Date { get; set; }

        // Navigation
        public Semester Semester { get; set; }
        public User Creator { get; set; }
        public ICollection<StudentGroup> Groups { get; set; }
    }
}
