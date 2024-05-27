using Dapper;
using SampleApplication.Models;
using SampleApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using SampleApplication.DAL.Extentions;

namespace SampleApplication.DAL.Repositories
{
    public class SampleUserRepo : ISampleUserRepo
    {
        private readonly string _connectionString;
        private ApiResponse _apiResponse;

        public SampleUserRepo(string connectionString)
        {
            _connectionString = connectionString;
            _apiResponse = new();
        }





        public ApiResponse CreateUsers(SampleUser user)
        {
            var procedure = "sp_CreateSampleUser";
            var parameters = new DynamicParameters();


            parameters.Add("@UserName", user.UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@EmailID", user.EmailID, DbType.String, ParameterDirection.Input);
            parameters.Add("@Password", user.Password, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsSuperUser", user.IsSuperUser, DbType.Boolean, ParameterDirection.Input);
            //parameters.Add("@EmpAddress", user.Address, DbType.String, ParameterDirection.Input);

            SampleUser users;
   
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (DbConnection conn = con)
                    {
                        conn.Open();
                        users = conn.Query(procedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    }

                    ApiResponse apiResponse = new();

                  _apiResponse.Data = users;
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;

           
                }


        }

        public ApiResponse DeleteUsers(int UserId)
        {
            var procedure = "sp_DeleteSampleUser";
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    var result = con.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    ApiResponse apiResponse = new();
                    if (result > 0)
                    {
                        apiResponse.Status = true;
                        apiResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        apiResponse.Status = false;
                        apiResponse.StatusCode = HttpStatusCode.NotFound;
                        apiResponse.Errors.Add("User not found or could not be updated.");
                    }
                    return apiResponse;
                }
            

        }


        public ApiResponse GetAllSampleUsers(string? SearchText)
        {
            var procedure = "sp_FilterGetAllSampleUserDetails";
            var parameters = new DynamicParameters();

            // Task<IEnumerable<Employee>> result;
            IList<SampleUser> result = [];

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (DbConnection conn = con)
                    {

                        result = conn.Query<SampleUser>(procedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                    }


                    _apiResponse.Data = result;
                    _apiResponse.Status = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                    return _apiResponse;
                }


        }

        public ApiResponse GetUserById(int UserId)
        {
            var procedure = "sp_GetSampleUserByUserId";
            var parameters = new DynamicParameters();
            

            parameters.Add("@UserId", UserId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    var user = con.Query<SampleUser>(procedure, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    ApiResponse apiResponse = new();
                    if (user != null)
                    {
                        apiResponse.Data = user;
                        apiResponse.Status = true;
                        apiResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        apiResponse.Status = false;
                        apiResponse.StatusCode = HttpStatusCode.NotFound;
                        apiResponse.Errors.Add("User not found.");
                    }
                    return apiResponse;
                }

        }

        public LoginResponse LoginSampleUser(LoginPost login)
        {
            var response = new LoginResponse();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", login.EmailId);
                parameters.Add("@Password", login.Password);

                con.Open();

                var result = con.QueryFirstOrDefault<LoginResponse>("sp_ValidateLogin", parameters, commandType: CommandType.StoredProcedure);

                response = result ?? new LoginResponse(); // Handle null response from stored procedure
            }

            
            return response;
        }

       

        public ApiResponse UpdateUsers(SampleUser user)
        {
            var procedure = "sp_UpdateSampleUser";
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", user.UserMasterId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", user.UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@EmailID", user.EmailID, DbType.String, ParameterDirection.Input);
            parameters.Add("@Password", user.Password, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsSuperUser", user.IsSuperUser, DbType.Boolean, ParameterDirection.Input);
            // Add other parameters as needed
          
            //try
            //{
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    var result = con.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    ApiResponse apiResponse = new();
                    if (result > 0)
                    {
                        apiResponse.Status = true;
                        apiResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        apiResponse.Status = false;
                        apiResponse.StatusCode = HttpStatusCode.NotFound;
                        apiResponse.Errors.Add("User not found or could not be updated.");
                    }
                    return apiResponse;
                }
            //}
            //catch (Exception ex)
            //{
            //    ApiResponse apiResponse = new();
            //    apiResponse.Errors.Add(ex.Message);
            //    apiResponse.Status = false;
            //    apiResponse.StatusCode = HttpStatusCode.InternalServerError;
            //    return apiResponse;
            //}
        }
    }
}
