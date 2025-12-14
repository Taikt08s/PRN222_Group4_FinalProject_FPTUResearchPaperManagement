using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class SubmissionDto
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        public int TopicId { get; set; }
        public int SemesterId { get; set; }

        public string Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }

        public float PlagiarismScore { get; set; }
        public bool PlagiarismFlag { get; set; }
        public string RejectReason { get; set; }

        public bool IsSubmitted => Status == "Submitted";
        public bool IsRejected => Status == "Rejected";
    }
}
