using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class ReviewLog
    {
        public int Id { get; set; }
        public int Submission_Id { get; set; }
        public Guid Reviewer_Id { get; set; }

        public string Original_Status { get; set; }
        public string New_Status { get; set; }
        public string Comment { get; set; }
        public DateTime Created_At { get; set; }

        public Submission Submission { get; set; }
        public User Reviewer { get; set; }
    }
}
