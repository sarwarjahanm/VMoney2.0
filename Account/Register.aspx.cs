using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace VMoney.Account
{
    public partial class Register : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.AppSettings["cons"].ToString();
        int result;

        public int register()
        {
            try
            {
                SqlConnection connect = new SqlConnection(connectionString);

                string Query1 = "insert into USERS values(@uname,@fname,@email,@pass,'R');";
                string Query2 = "insert into DETAILS(USERNAME) values(@uname1);";
                string Query3 = "insert into ACCOUNTS(USERNAME,ACCBALANCE) values(@uname2,0);";

                SqlCommand command1 = new SqlCommand(Query1, connect);
                SqlCommand command2 = new SqlCommand(Query2, connect);
                SqlCommand command3 = new SqlCommand(Query3, connect);

                String fname = HttpUtility.HtmlEncode(txtFullname.Text);

                command1.Parameters.Add("@uname", txtUname.Text);
                command1.Parameters.Add("@fname", fname);
                command1.Parameters.Add("@email", txtEmail.Text);
                command1.Parameters.Add("@pass", txtPass.Text);
                command2.Parameters.Add("@uname1", txtUname.Text);
                command3.Parameters.Add("@uname2", txtUname.Text);

                connect.Open();                               

                result = command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                command3.ExecuteNonQuery();

                connect.Close();

            }
            catch (Exception f)
            {
                lblDbEerror.Text = f.Message.ToString();
            }
            return result;

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            int res = register();
            if (res > 0)
            {
                Response.Redirect("~/Messages/Success.aspx");
            }
            else
            {
                Response.Redirect("~/Messages/Regerror.aspx");
            }

        }
    }
}