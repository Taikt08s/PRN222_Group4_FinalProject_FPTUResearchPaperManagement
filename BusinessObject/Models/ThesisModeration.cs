using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class ThesisModeration
    {
        public int Id { get; set; }
        public int Submission_Id { get; set; }

        public bool Plagiarism_Pass { get; set; }
        public float Plagiarism_Score { get; set; }
        public bool Originality_Pass { get; set; }
        public bool Citation_Quality_Pass { get; set; }
        public bool Academic_Rigor_Pass { get; set; }
        public bool Research_Relevance_Pass { get; set; }
        public bool Writing_Quality_Pass { get; set; }
        public bool Ethical_Compliance_Pass { get; set; }
        public bool Completeness_Pass { get; set; }

        public bool Is_Approved { get; set; }
        public string Reasoning { get; set; }
        public string Violations_Json { get; set; }

        public DateTime Created_At { get; set; }

        public Submission Submission { get; set; }
    }
}
