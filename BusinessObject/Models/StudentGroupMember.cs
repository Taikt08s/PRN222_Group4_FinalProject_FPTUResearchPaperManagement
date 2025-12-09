using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class StudentGroupMember
    {
        public int Id { get; set; }
        public int Group_Id { get; set; }
        public Guid Student_Id { get; set; }
        public bool Is_Leader { get; set; }

        public StudentGroup Group { get; set; }
        public User Student { get; set; }
    }
}
