using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.Models.Response
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public dynamic Data { get; set; }
        //public List<String> Errors { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
