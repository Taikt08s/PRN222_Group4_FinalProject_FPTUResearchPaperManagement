using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Term { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
