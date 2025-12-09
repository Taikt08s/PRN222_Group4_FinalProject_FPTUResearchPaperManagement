using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class DashboardCache
    {
        public int Id { get; set; }
        public int Semester_Id { get; set; }
        public int Total_Students { get; set; }
        public int Submitted_Count { get; set; }
        public int Approved_Count { get; set; }
        public int Plagiarism_Count { get; set; }
        public int Reject_Count { get; set; }
        public DateTime Generated_At { get; set; }

        public Semester Semester { get; set; }
    }
}
