using SampleApplication.Models;
using SampleApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.DAL.Repositories
{
    public interface ISampleUserRepo
    {
       

        public ApiResponse CreateUsers(SampleUser user);
        public ApiResponse UpdateUsers(SampleUser user);
        public ApiResponse GetAllSampleUsers(string? SearchText);
        public ApiResponse GetUserById(int UserId);
        public ApiResponse DeleteUsers(int UserId);
        public LoginResponse LoginSampleUser(LoginPost login);
    }
}
