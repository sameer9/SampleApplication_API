using SampleApplication.Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplication.DAL.Extentions
{
    public static class UserExtensions
    {
        public static LoginResponse ToUserLogin(this DataSet dataSet)
        {
            var response = new LoginResponse();
            if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
            {
                response.UserMasterId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["UserMasterId"]);
                response.UserName = Convert.ToString(dataSet.Tables[0].Rows[0]["UserName"]);
                response.EmailID = Convert.ToString(dataSet.Tables[0].Rows[0]["EmailID"]);
                response.IsSuperUser = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["IsSuperUser"]);
                response.TokenExpiration = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["TokenExpiration"]);

                // response.Token = Convert.ToString(dataSet.Tables[0].Rows[0]["Token"]);

            }
            return response;
        }
    }
}
