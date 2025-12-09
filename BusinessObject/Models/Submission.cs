using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public int Group_Id { get; set; }
        public int Topic_Id { get; set; }
        public int Semester_Id { get; set; }

        public string Status { get; set; }
        public float Plagiarism_Score { get; set; }
        public bool Plagiarism_Flag { get; set; }
        public string Reject_Reason { get; set; }

        public DateTime? Submitted_At { get; set; }
        public DateTime? Reviewed_At { get; set; }

        public StudentGroup Group { get; set; }
        public Topic Topic { get; set; }
        public Semester Semester { get; set; }

        public ThesisModeration Moderation { get; set; }
        public ICollection<SubmissionFile> Files { get; set; }
    }
}
