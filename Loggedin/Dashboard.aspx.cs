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
    public partial class Dashboard : Page
    {        

        public void fetchAccounts()
        {
            try
            {
                string connections = ConfigurationManager.AppSettings["cons"].ToString();

                SqlConnection connect = new SqlConnection(connections);

                String uname = Session["user"].ToString();
               
                string Query = "select * from ACCOUNTS where USERNAME=@uname;";

                SqlCommand command = new SqlCommand(Query, connect);

                command.Parameters.Add("@uname", uname);

                connect.Open();

                
                SqlDataReader dr = command.ExecuteReader();

                if (dr.Read())
                {
                    lblUname.Text = dr[0].ToString();
                    lblAccno.Text = dr[1].ToString();
                    lblBalance.Text = dr[2].ToString();
                }

            }

            catch (Exception ex)
            {
                //
            }
        }

        public void DepositAmount()
        {
            Int64 depAmt = Int64.Parse(txtdeposit.Text);

            if (depAmt < 0)
            {
                lblInsFund.Text = "Enter a Positive amount!!";
            }
            else
            {
                try
                {
                    string connections = ConfigurationManager.AppSettings["cons"].ToString();

                    SqlConnection connect = new SqlConnection(connections);

                    String uname = Session["user"].ToString();

                    string Query1 = "select ACCBALANCE from ACCOUNTS where USERNAME=@uname1;";
                    SqlCommand command1 = new SqlCommand(Query1, connect);

                    command1.Parameters.Add("@uname1", uname);

                    connect.Open();

                    String res = command1.ExecuteScalar().ToString();
                    Int64 DbAmt = Int64.Parse(res);
                    Int64 UpdAmt = depAmt + DbAmt;

                    string Query2 = "UPDATE ACCOUNTS set ACCBALANCE=@bal " + "WHERE USERNAME=@uname2;";
                    SqlCommand command2 = new SqlCommand(Query2, connect);

                    command2.Parameters.Add("@bal", UpdAmt);
                    command2.Parameters.Add("@uname2", uname);

                    command2.ExecuteNonQuery();


                    Response.Redirect("~/Loggedin/Dashboard.aspx");

                    connect.Close();

                }

                catch (Exception ex)
                {
                    //
                }
            }

        }

        public void WithdrawAmount()
        {
            Int64 drawAmt = Int64.Parse(txtWithdraw.Text);

            if (drawAmt < 0)
            {
                lblInsFund.Text = "Enter a positive amount!!";
            }

            else
            {
                try
                {
                    string connections = ConfigurationManager.AppSettings["cons"].ToString();

                    SqlConnection connect = new SqlConnection(connections);

                    String uname = Session["user"].ToString();

                    string Query1 = "select ACCBALANCE from ACCOUNTS where USERNAME=@uname1;";
                    SqlCommand command1 = new SqlCommand(Query1, connect);

                    command1.Parameters.Add("@uname1", uname);

                    connect.Open();

                    String res = command1.ExecuteScalar().ToString();
                    Int64 DbAmt = Int64.Parse(res);
                    Int64 UpdAmt = DbAmt - drawAmt;
                    if (UpdAmt < 0)
                    {
                        lblInsFund.Text = "No sufficient balance in Account!!";

                    }

                    else
                    {
                        string Query2 = "UPDATE ACCOUNTS set ACCBALANCE=@bal1 " + "WHERE USERNAME=@uname3;";
                        SqlCommand command2 = new SqlCommand(Query2, connect);

                        command2.Parameters.Add("@bal1", UpdAmt);
                        command2.Parameters.Add("@uname3", uname);

                        command2.ExecuteNonQuery();


                        Response.Redirect("~/Loggedin/Dashboard.aspx");
                    }

                    connect.Close();

                }

                catch (Exception ex)
                {
                    //
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Session["user"].ToString();
            fetchAccounts();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            divBalance.Attributes["style"] = "visibility: visible;";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DepositAmount();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            WithdrawAmount();
        }
    }
}