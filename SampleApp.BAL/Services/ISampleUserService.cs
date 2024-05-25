using SampleApplication.Models.Response;
using SampleApplication.Models;

namespace SampleApplication.BAL.Services
{
    public interface ISampleUserService
    {
        

        public ApiResponse CreateUsers(SampleUser user);
        public ApiResponse UpdateUsers(SampleUser user);
        public ApiResponse GetAllSampleUsers(string? SearchText);
        public ApiResponse GetUserById(int UserId);
        public ApiResponse DeleteUsers(int UserId);
        public LoginResponse LoginSampleUser(LoginPost login);
    }
}
