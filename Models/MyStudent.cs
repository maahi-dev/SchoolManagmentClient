using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementClient.Models
{
    public class MyStudent
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int? Age { get; set; }
        public int Total
        {
            get
            {
                return (int)(StudentId + Age);
            }
        }
        public string Gender { get; set; }
        public string Contact { get; set; }
    }
}
