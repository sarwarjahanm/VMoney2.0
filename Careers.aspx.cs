using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;

namespace VMoney
{
    public partial class Careers : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.AppSettings["cons"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String fileName = FUResume.FileName;
            String FileExt = System.IO.Path.GetExtension(fileName); 
            String name = txtName.Text;
            String contentType = FUResume.PostedFile.ContentType;

            try
            {
                SqlConnection connect = new SqlConnection(connectionString);                

                if (FUResume.HasFile)
                {
                    String Jpg = ".jpg", Jpeg = ".jpeg", Png = ".png", Pdf = ".pdf";

                    if (string.Equals(FileExt, Jpg) || string.Equals(FileExt, Jpeg) || string.Equals(FileExt, Png) || string.Equals(FileExt, Pdf))
                    {
                    String path = "~/Files/" + name + "/";
                    DirectoryInfo di = Directory.CreateDirectory(Server.MapPath(path));
                    FUResume.SaveAs(Server.MapPath(path + fileName));

                    string Query = "insert into RESUME values(@name1,@emailID,@mobile,@filename);";
                    SqlCommand command = new SqlCommand(Query, connect);

                    String name1 = HttpUtility.HtmlEncode(txtName.Text);
                    String emailID = HttpUtility.HtmlEncode(txtEmail.Text);
                    String mobile = HttpUtility.HtmlEncode(txtMobile.Text);
                    fileName = HttpUtility.HtmlEncode(fileName);

                    command.Parameters.Add("@name1", name1);
                    command.Parameters.Add("@emailID", emailID);
                    command.Parameters.Add("@mobile", mobile);
                    command.Parameters.Add("@filename", fileName);

                    connect.Open();
                    command.ExecuteNonQuery();
                    connect.Close();

                    lblMsg.Text = "Thank you. We will contact you if your profile matches our requirement.";

                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtMobile.Text = "";
                    }
                    else
                    {
                        lblMsg0.Text = "STOP! ";
                        lblMsg.Text = "Allowed file types: .pdf, .png, .jpeg, .jpg";
                    }
                }
                

            }
            catch (Exception ex)
            {
               lblMsg.Text = "Sorry, Some runtime error occured!!";
            }
            

        }
    }
}