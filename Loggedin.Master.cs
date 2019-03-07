using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace VMoney
{
    public partial class Loggedin : System.Web.UI.MasterPage
    {
        public String fetchFullname(String user)
        {
            string fname = "";
            try
            {
                string connections = ConfigurationManager.AppSettings["cons"].ToString();

                SqlConnection connect = new SqlConnection(connections);

                string Query = "select FULLNAME from USERS where USERNAME=@name";

                SqlCommand command = new SqlCommand(Query, connect);

                command.Parameters.Add("@name", user);

                connect.Open();
                fname = command.ExecuteScalar().ToString();

                fname = HttpUtility.HtmlEncode(fname);      //HTML encoding to avoid HTMLi

            }

            catch (Exception ex)
            {
                //
            }

            return fname;
        }

        public string SetlblCurrentUser
        {            
            set { lblCurrentUser.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetlblCurrentUser = fetchFullname(Session["user"].ToString());
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();        //To disable caching of LoggedIn pages
        }

        protected void LogOut(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Home.aspx");       //Proper LogOut
        }
    }
}