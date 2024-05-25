using SampleApplication.DAL.Repositories;
using SampleApplication.Models;
using SampleApplication.Models.Response;

namespace SampleApplication.BAL.Services
{
    public class SampleUserService : ISampleUserService
    {
        private readonly ISampleUserRepo _sampleUserRepo;

        public SampleUserService(ISampleUserRepo sampleUserRepo)
        {
            _sampleUserRepo = sampleUserRepo;
        }
        public ApiResponse CreateUsers(SampleUser user)
        {
           return _sampleUserRepo.CreateUsers(user);
        }

        public ApiResponse DeleteUsers(int UserId)
        {
            return _sampleUserRepo.DeleteUsers(UserId); 
        }

        public ApiResponse GetAllSampleUsers(string? SearchText)
        {
            return _sampleUserRepo.GetAllSampleUsers(SearchText);
        }


        public ApiResponse GetUserById(int UserId)
        {
            return _sampleUserRepo.GetUserById(UserId);
        }

        public LoginResponse LoginSampleUser(LoginPost login)
        {
            return _sampleUserRepo.LoginSampleUser(login);
        }


        public ApiResponse UpdateUsers(SampleUser user)
        {
            return _sampleUserRepo.UpdateUsers(user);
        }
    }
}
