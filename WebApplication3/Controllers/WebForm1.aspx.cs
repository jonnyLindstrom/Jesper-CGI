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
        protected void btnReadFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader Reader = File.OpenText(Server.MapPath("Namnlista.txt")))
                {
                    string[] read;
                    string[,] matris;
                    matris = new string[100,3]; //tyvärr hårdkodad storlek på 100 rader o 3 kolumner
                    int i = 0;
                    int v;
                    char[] seperators = { ',' };
                    string data; 
                    while ((data = Reader.ReadLine()) != null)
                    {
                        read = data.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                        
                        bool isIntPos0 = int.TryParse(read[0], out v); //is integer?
                        if (isIntPos0) { //integer in first column
                            matris[i, 2] = read[0]; //age
                            matris[i, 0] = read[1]; //first name
                            if (read.Length > 2) { matris[i, 1] = read[2]; } //last name
                        }                        
                        
                        bool isIntPos1 = int.TryParse(read[1], out v); //is integer?
                         if (isIntPos1)  { //integer in second column
                            matris[i, 2] = read[1]; //age
                            matris[i, 0] = read[0]; //first name
                            if (read.Length > 2) { matris[i, 1] = read[2]; } //last name
                        }                       
                        
                        if (read.Length > 2) //does third column exist
                        {
                            bool isIntPos2 = int.TryParse(read[2], out v); //is integer?
                            if (isIntPos2)
                            { //integer in third column
                                if (read.Length > 2) { matris[i, 2] = read[2]; } //age
                                matris[i, 0] = read[0]; //first name
                                matris[i, 1] = read[1]; //last name
                            }
                        }
                        i++;
                    }
                    for (i=0; i<10; i++)
                    {
                        ListBox1.Items.Add(matris[i, 0] + "-" + matris[i, 1] + "-" + matris[i, 2]);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblReadText.Text = "Could not read from the file, the errormessage is: " + ex.ToString();
            }
        }
    }
}