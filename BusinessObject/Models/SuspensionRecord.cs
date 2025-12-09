using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class SuspensionRecord
    {
        public int Id { get; set; }
        public Guid Student_Id { get; set; }
        public int Submission_Id { get; set; }

        public bool Detected_By_Ai { get; set; }
        public bool Approved_By_Gpec { get; set; }
        public bool Approved_By_Sdc { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        public User Student { get; set; }
        public Submission Submission { get; set; }
    }
}
