using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dtos
{
    public class StudentBasicInfo
    {
        public Guid Id { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public bool IsLeader { get; set; }
    }
}
