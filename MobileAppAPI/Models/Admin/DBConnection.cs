using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MobileAppAPI.Models.Admin
{
    public class DBConnection
    {
        protected string strConString = ConfigurationManager.ConnectionStrings["conStr"].ToString();
    }
}
