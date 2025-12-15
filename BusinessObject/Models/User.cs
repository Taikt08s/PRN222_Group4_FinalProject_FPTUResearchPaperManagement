using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Role { get; set; }
        public string Major { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Suspended { get; set; }
        public DateTime? Suspended_Until { get; set; }
        public DateTime Created_At { get; set; }

        // Navigation
        public ICollection<Topic> Topics { get; set; }
        public ICollection<StudentGroup> CreatedGroups { get; set; }
    }
}
