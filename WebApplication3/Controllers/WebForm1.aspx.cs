using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace WebApplication3.Controllers
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void btnReadFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a StreamReader. The using block is used to call dispose (close) automatically even 
                // if there are an exception.
                using (StreamReader Reader = File.OpenText(Server.MapPath("Namnlista.txt"))) //C:\Kodning
                {
                    string Content = Reader.ReadToEnd();
                    lblReadText.Text = Content.Replace("\r\n", "<br>");
                }
            }
            catch (Exception ex)
            {
                lblReadText.Text = "Could not read from the file, the errormessage is: " + ex.ToString();
            }
        }
    }
}