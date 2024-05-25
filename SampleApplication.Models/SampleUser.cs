using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.Models
{
    public class SampleUser
    {
        public int UserMasterId { get; set; }
        public string? UserName { get; set; }
        public string? EmailID { get; set; }
        public string? Password { get; set; }

        public bool? IsSuperUser { get; set; }
        public bool IsActive { get; set; }
    }
}
