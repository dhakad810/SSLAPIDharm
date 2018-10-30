using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.IO;
 

namespace MobileAppAPI.Models.Admin
{
    public class HeaderKey : DBConnection
    {   
            public string HeaderKeyID { get; set; }
            public string AppID { get; set; }
            public string AppKey { get; set; }

        commonEncryption commonEncrpt = new commonEncryption();

        public void GetHeaderDetail()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetMobileAppAPIDetail", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "HeaderKey"));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                //HeaderKeyID = commonEncrpt.Encrypt(dt.Rows[0]["HeaderKey"].ToString());
                //AppID = commonEncrpt.Encrypt(dt.Rows[0]["AppID"].ToString());
                //AppKey = commonEncrpt.Encrypt(dt.Rows[0]["AppKey"].ToString());
                HeaderKeyID = dt.Rows[0]["HeaderKey"].ToString();
                AppID = dt.Rows[0]["AppID"].ToString();
                AppKey = dt.Rows[0]["AppKey"].ToString();

            }
        }
    }
}