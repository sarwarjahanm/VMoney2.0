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
    public partial class Profile : Page
    {
        String uname = "";
        string connections = ConfigurationManager.AppSettings["cons"].ToString();
  
        public void fetchDetails()
        {
            try
            {
                SqlConnection connect = new SqlConnection(connections);

                string Query = "select * from DETAILS where USERNAME=@name1;";
                string Query2 = "select USRROLES from USERS where USERNAME=@name2;";
                string Query3 = "select FULLNAME from USERS where USERNAME=@name3;";

                SqlCommand command = new SqlCommand(Query, connect);
                SqlCommand command2 = new SqlCommand(Query2, connect);
                SqlCommand command3 = new SqlCommand(Query3, connect);

                command.Parameters.Add("@name1", uname);
                command2.Parameters.Add("@name2", uname);
                command3.Parameters.Add("@name3", uname);

                connect.Open();

                txtRole.Text = command2.ExecuteScalar().ToString();
                txtFullname.Text = command3.ExecuteScalar().ToString();

                SqlDataReader dr = command.ExecuteReader();


                if (dr.Read())
                {
                    txtUsername.Text = dr[0].ToString();
                    txtMobile.Text = dr[1].ToString();
                    txtCity.Text = dr[2].ToString();
                    txtState.Text = dr[3].ToString();
                    txtCountry.Text = dr[4].ToString();

                }
                connect.Close();
                dr.Close();

            }

            catch (Exception ex)
            {
                //
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            uname = Session["user"].ToString();
            
            if (!IsPostBack)
                fetchDetails();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            String mob = txtMobile.Text;
            String city = txtCity.Text;
            String state = txtState.Text;
            String country = txtCountry.Text;

            txtRole.Attributes["style"] = "enabled: true;";
            txtFullname.Attributes["style"] = "enabled: true;";

            String role = txtRole.Text;
            String fname = txtFullname.Text;

            txtRole.Attributes["style"] = "enabled: false;";
            txtFullname.Attributes["style"] = "enabled: false;";

            try
            {
                SqlConnection connect2 = new SqlConnection(connections);

                string Query3 = "UPDATE DETAILS SET MOBILE=@mob, CITY=@city, STATENAME=@state, COUNTRY=@country " + "WHERE USERNAME=@uname;";
                
                SqlCommand command3 = new SqlCommand(Query3, connect2);

                command3.Parameters.Add("@mob", mob);
                command3.Parameters.Add("@city", city);
                command3.Parameters.Add("@state", state);
                command3.Parameters.Add("@country", country);
                command3.Parameters.Add("@uname", uname);

                
                connect2.Open();

                command3.ExecuteNonQuery();
                
                connect2.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('Profile Updated Successfully!!!');</script>");

            }

            catch (Exception ex)
            {
                lblMsg.Text = "Sorry, Some runtime error has occured!!";
            }
            finally
            {
               // Response.Redirect("~/Loggedin/Profile.aspx");
            }

        }
    }
}