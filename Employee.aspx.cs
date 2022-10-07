using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyFirstAzureProject
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string folderPath = Server.MapPath("~/Images/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

                Image1.ImageUrl = "~/Images/" + Path.GetFileName(FileUpload1.FileName);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
                SqlCommand cmd = new SqlCommand("insert into tblImages(ImagePath) values(@ImagePath)", con);
                cmd.Parameters.AddWithValue("@ImagePath", Image1.ImageUrl);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Label1.Text = "Image Uploaded";
                Label1.ForeColor = System.Drawing.Color.ForestGreen;

            }

            else
            {
                Label1.Text = "Please Upload your Image";
                Label1.ForeColor = System.Drawing.Color.Red;
            }

        }
    }
}