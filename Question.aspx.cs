﻿using System;
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
    public partial class Question : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.AppSettings["cons"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string ques = txtQuestion.Text;
            ques=HttpUtility.HtmlEncode(ques);

            try
            {
                SqlConnection connect = new SqlConnection(connectionString);

                string Query = "insert into QUESTION values(@name,@ques);";
                SqlCommand command = new SqlCommand(Query, connect);

                command.Parameters.Add("@name", txtName.Text);
                command.Parameters.Add("@ques", ques);

                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();

                lblQuestionsuccess.Text = "Your question was submitted. After moderation, we will publish the answer.";

            }
            catch (Exception ex)
            {
                lblQuestionsuccess.Text = "Sorry, Some runtime error has occured!!";
            }
            finally
            {
                txtName.Text = "";
                txtQuestion.Text = "";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string ques = txtQuestion.Text;
            string mal = "<script>";
            string mal1 = "onload";
            string mal2 = "onmouseover";
            string mal3 = "onerror";
            string mal4 = "<h1>";
            string mal5 = "<p>";
            string mal6 = "<img>";
            string mal7 = "<a>";

            ques = HttpUtility.HtmlEncode(ques);

            if (ques.Contains(mal) || ques.Contains(mal1) || ques.Contains(mal2) || ques.Contains(mal3))
            {
                lblQuestionsuccess.Text = "Your question: "+ ques +" contains XSS payload!!";
            }
            else if (ques.Contains(mal4) || ques.Contains(mal5) || ques.Contains(mal6) || ques.Contains(mal7))
            {
                lblQuestionsuccess.Text = "Your question: "+ ques +" contains HTML tags which are not allowed!!";
            }
            else
            {
            lblQuestionsuccess.Text = "Your question: "+ ques +" is good to post...!";
            }
        }
    }
}