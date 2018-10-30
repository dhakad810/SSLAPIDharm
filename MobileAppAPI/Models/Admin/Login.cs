using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Net;

namespace MobileAppAPI.Models.Admin
{
    public class Login : DBConnection
    {
    }
    public class UserRegistration
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserMobileNo { get; set; }
        public string EmailId { get; set; }
        public string UserAddress { get; set; }
        public string UserPassWord { get; set; }
        public string DeviceID { get; set; }
        public string ModelNo { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
    public class LoginUser
    {
        public string UserMobileNo { get; set; }
        public string Password { get; set; }
    }

    public class UserLogin : DBConnection
    {      
        public int SaveUserDetail(UserRegistration UserRD)
        {
            int RowAffected = 0;
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetMobileAppAPIDetail", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "NewUser"));
                cmd.Parameters.Add(new SqlParameter("@UserMobileNo", UserRD.UserMobileNo));
                cmd.Parameters.Add(new SqlParameter("@UserName", UserRD.UserName));
                cmd.Parameters.Add(new SqlParameter("@UserPassWord", UserRD.UserPassWord));
                cmd.Parameters.Add(new SqlParameter("@UserAddress", UserRD.UserAddress));
                cmd.Parameters.Add(new SqlParameter("@EmailId", UserRD.EmailId));
                cmd.Parameters.Add(new SqlParameter("@DeviceID", UserRD.DeviceID));
                cmd.Parameters.Add(new SqlParameter("@ModelNo", UserRD.ModelNo));               
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                RowAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            return RowAffected;
        }

        public UserRegistration CheckLoginDetail(LoginUser UserRD)
        {
            UserRegistration userRegistration = new UserRegistration();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetMobileAppAPIDetail", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "UserLogin"));
                cmd.Parameters.Add(new SqlParameter("@UserMobileNo", UserRD.UserMobileNo));
                cmd.Parameters.Add(new SqlParameter("@UserPassWord", UserRD.Password));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                userRegistration.UserId = dt.Rows[0]["UserId"].ToString();
                userRegistration.UserName = dt.Rows[0]["UserName"].ToString();
                userRegistration.UserMobileNo = dt.Rows[0]["MobileNo"].ToString();
                userRegistration.EmailId = dt.Rows[0]["EmailId"].ToString();
                userRegistration.UserAddress = dt.Rows[0]["UserAddress"].ToString();
            }
            return userRegistration;
        }

        public UserRegistration GetUserDetail(string MobileNo)
         {
            UserRegistration userregister = new Admin.UserRegistration();

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetMobileAppAPIDetail", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "GetUserDetail"));
                cmd.Parameters.Add(new SqlParameter("@UserMobileNo", MobileNo));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                userregister.UserName = dt.Rows[0]["UserName"].ToString();
                userregister.UserMobileNo = dt.Rows[0]["MobileNo"].ToString();
                userregister.EmailId = dt.Rows[0]["EmailId"].ToString();
                userregister.UserAddress = dt.Rows[0]["UserAddress"].ToString();
                userregister.DeviceID = dt.Rows[0]["DeviceID"].ToString();
                userregister.ModelNo = dt.Rows[0]["ModelNo"].ToString();
                userregister.RegistrationDate = Convert.ToDateTime(dt.Rows[0]["RegistrationDate"]);
            }
            return userregister;

        }
    }
}