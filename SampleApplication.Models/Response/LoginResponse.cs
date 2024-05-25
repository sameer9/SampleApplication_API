using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.Models.Response
{
    public class LoginResponse
    {
        public int UserMasterId { get; set; }
        public string? UserName { get; set; }
        public string? EmailID { get; set; }
      //  public List<Role>? UserRoles { get; set; }

        public string? Token { get; set; }

        public DateTime TokenExpiration { get; set; }

        public bool? IsSuperUser { get; set; }
    }
}
