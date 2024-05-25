using SampleApplication.BAL.Services;
using SampleApplication.DAL.Repositories;

namespace SampleApplication.API
{
    public static class ServiceExtensions
    {

        public static void RegisterRepos(this IServiceCollection collection, ConfigurationManager configuration)
        {
            //Register Repos
            var connectionString = configuration["ConnectionStrings:sampleAppConnectionString"];
            collection.AddTransient<ISampleUserService, SampleUserService>();
            collection.AddTransient<ISampleUserRepo>(s => new SampleUserRepo(connectionString));







        }
        public static void RegisterLogging(this IServiceCollection collection)
        {
            //Register logging

        }

        public static void RegisterAuth(this IServiceCollection collection)
        {
            //Register authentication services.

        }

    }
}
