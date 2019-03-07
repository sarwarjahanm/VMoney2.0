using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace VMoney
{
    public partial class Settings : Page
    {
        string connectionString = ConfigurationManager.AppSettings["cons"].ToString();
        String result;
               

        public String getPassword(String username)
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionString);

                string Query = "SELECT PASSWORD FROM USERS WHERE USERNAME=@name2";

                SqlCommand command = new SqlCommand(Query, connect);

                command.Parameters.Add("@name2", username);

                connect.Open();

                result = command.ExecuteScalar().ToString();

                connect.Close();

            }
            catch (Exception f)
            {
                lblError.Text = f.Message.ToString();
            }
            return result;

        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Session["user"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String uname = Session["user"].ToString();
            String DbPassword = getPassword(uname);
            String NewPassword = txtNewPwd.Text;
            String CurPassword = txtCurrentPwd.Text;

            if (!NewPassword.Equals(DbPassword))
            {
                if (DbPassword.Equals(CurPassword))
                {
                    try
                    {
                        lblError.Text = "";

                        SqlConnection connect1 = new SqlConnection(connectionString);

                        string Query1 = "UPDATE USERS set PASSWORD=@pass " +  "WHERE USERNAME=@name1;";
                        SqlCommand command1 = new SqlCommand(Query1, connect1);

                        command1.Parameters.Add("@pass", NewPassword);
                        command1.Parameters.Add("@name1", uname);

                        connect1.Open();
                        command1.ExecuteNonQuery();

                        connect1.Close();

                        lblError.Text = "Password Change Successful :)";
                    }
                    catch (Exception ex)
                    {
                        //
                    }
                }

                else
                {
                    lblError.Text = "Invalid Current Password!!!";
                }
            }
            else
            {
                lblError.Text = "Password Change not successful!!!";
            }

        }
    }
}