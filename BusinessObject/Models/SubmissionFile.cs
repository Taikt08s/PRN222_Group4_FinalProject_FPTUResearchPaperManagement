using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class SubmissionFile
    {
        public int Id { get; set; }
        public int Submission_Id { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public string Firebase_Url { get; set; }
        public DateTime Uploaded_At { get; set; }

        public Submission Submission { get; set; }
    }
}
