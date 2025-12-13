using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class SubmissionFileDto
    {
        public int Id { get; set; }
        public string File_Name { get; set; }
        public string File_Type { get; set; }
        public string Firebase_Url { get; set; }
        public DateTime Uploaded_At { get; set; }
    }
}
