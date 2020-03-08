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
        public void LoadFile()
        {
            try
            {
                List<Person> people = new List<Person>();
                using (StreamReader Reader = File.OpenText(Server.MapPath("Namnlista.txt")))
                {
                    string[] read;
                    //int i = 0;
                    int v;
                    char[] seperators = { ',' };
                    string sFirstName = "";
                    string sLastName = "";
                    int iAge = 0;
                    string data;

                    while ((data = Reader.ReadLine()) != null)
                    {
                        read = data.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        bool isIntPos0 = int.TryParse(read[0], out v); //is integer?
                        if (isIntPos0)
                        { //integer in first column
                            iAge = v; //age
                            sFirstName = read[1]; //first name
                            if (read.Length > 2)
                            {
                                sLastName = read[2]; //last name
                            }
                        }

                        bool isIntPos1 = int.TryParse(read[1], out v); //is integer?
                        if (isIntPos1)
                        { //integer in second column
                            iAge = v; //age
                            sFirstName = read[0]; //first name
                            if (read.Length > 2)
                            {
                                sLastName = read[2]; //last name
                            }
                        }

                        if (read.Length > 2) //does third column exist
                        {
                            bool isIntPos2 = int.TryParse(read[2], out v); //is integer?
                            if (isIntPos2)
                            { //integer in third column
                                if (read.Length > 2)
                                {
                                    iAge = v; //age
                                } //age
                                sFirstName = read[0]; //first name
                                sLastName = read[1]; //last name
                            }
                        }
                        people.Add(new Person(sFirstName, sLastName, iAge));
                    }

                    Session["DisplayStart"] = 0; //set start value
                    Label1.Text = "DisplayStart= " + Session["DisplayStart"];
                    int i = 0;
                    foreach (Person per in people)
                    {
                        if (i < 10) //list 10 persons
                        {
                            ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                        }
                        i++;
                    }
                    Session["sPeople"] = people;
                }
            }
            catch (Exception ex)
            {
                lblReadText.Text = "Could not read from the file, the errormessage is: " + ex.ToString();
            }
        }

        protected void btnSavePersonToFile_Click(object sender, EventArgs e)
        {
            int age;
            //validate input
            if (TextBoxFirstName.Text.Trim().Length == 0 || TextBoxLastName.Text.Trim().Length == 0) { 
                lblReadText.Text = "Förnamn samt efternamn måste innehålla bokstäver";
                return;
            }
            bool isNumber = int.TryParse(TextBoxAge.Text, out age);
            if (!isNumber || age<1 || age>150) { 
                lblReadText.Text = "Ålder får endast ligga mellan 1-150"; 
                return; 
            }
            string line = TextBoxFirstName.Text + "," + TextBoxLastName.Text + "," + TextBoxAge.Text;
            using (StreamWriter w = File.AppendText(Server.MapPath("Namnlista.txt")))
            {
                w.WriteLine(line);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int valueSearch;
            
            if (TextBoxSearch.Text.Trim().Length == 0) { return; } //dont accept blank input

            List<Person> people = new List<Person>(); //create people object
            people = (List<Person>)Session["sPeople"]; //fetch people list from session

            bool isNumber = int.TryParse(TextBoxSearch.Text, out valueSearch); //number search?

            ListBox1.Items.Clear();
            if (isNumber) {
                foreach (Person per in people)
                {
                    if (valueSearch == per.age)
                    {
                        ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                    }
                }
            }
            else {
                foreach (Person per in people)
                {
                    if (per.firstName.ToLower().Contains(TextBoxSearch.Text.ToLower()) || per.lastName.ToLower().Contains(TextBoxSearch.Text.ToLower()))
                    {
                        ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["Page"] != "FileLoaded") {  //only load file once
                Session["Page"] = "FileLoaded";
                LoadFile();
            }
        }
        protected void btnSortFirstName_Click(object sender, EventArgs e)
        {
            List<Person> people = new List<Person>(); //create people object
            people = (List<Person>)Session["sPeople"]; //fetch people list from session

            //Sorted list, by name

            string sort = (String)Session["fNamnSort"];
            //Sorted list, by age 
            if (sort == "ascending")
            {
                people.Sort(delegate (Person p1, Person p2) { return p1.firstName.CompareTo(p2.firstName); }); //descending
                Session["fNamnSort"] = "descending";
            }
            else
            {
                people.Sort(delegate (Person p1, Person p2) { return p2.firstName.CompareTo(p1.firstName); }); //ascending
                Session["fNamnSort"] = "ascending";
            }

            ListBox1.Items.Clear();
            int i = 0;
            foreach (Person per in people)
            {
                if (i < 10)
                {
                    ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                }
                i++;
            }
            Session["sPeople"] = people; //update session w sorted list
        }
        protected void btnSortAge_Click(object sender, EventArgs e)
        {
            List<Person> people = new List<Person>(); //create people object
            people = (List<Person>)Session["sPeople"]; //fetch from session

            string sort = (String) Session["ageSort"];
            //Sorted list, by age 
            if (sort == "ascending")
            {
                people.Sort(delegate (Person p1, Person p2) { return p1.age.CompareTo(p2.age); }); //descending
                Session["ageSort"] = "descending";
            } else { 
                people.Sort(delegate (Person p1, Person p2) { return p2.age.CompareTo(p1.age); }); //ascending
                Session["ageSort"] = "ascending";
            }

            ListBox1.Items.Clear();
            //int i = 0;

            for (int i = 0; i < 10; i++) { 
                ListBox1.Items.Add(people[i].firstName + " - " + people[i].lastName + " - " + people[i].age);
            }

            /*foreach (Person per in people)
            {
                if (i < 10)
                {
                    ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age); 
                }
                i++;
            }*/
            Session["sPeople"] = people; //update session w sorted list
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            List<Person> people = new List<Person>(); //create people object
            people = (List<Person>)Session["sPeople"]; //fetch from session
            
            ListBox1.Items.Clear();
            int start = (int)Session["DisplayStart"] + 10;
            int i = 0;
            int stop = start + 9;
            foreach (Person per in people)
            {
                if (i >= start && i <= stop)
                {
                    ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                }
                i++;
            }
            Session["sPeople"] = people; //update session w sorted list
            Session["DisplayStart"] = (int)Session["DisplayStart"] + 10; //update with current display
            Label1.Text = "DisplayStart= " + Session["DisplayStart"];
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            List<Person> people = new List<Person>(); //create people object
            people = (List<Person>)Session["sPeople"]; //fetch from session

            ListBox1.Items.Clear();
            int start = (int)Session["DisplayStart"] - 10;
            int i = 0;
            int stop = start + 9;
            foreach (Person per in people)
            {
                if (i >= start && i <= stop)
                {
                    ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                }
                i++;
            }
            Session["sPeople"] = people; //update session w sorted list
            Session["DisplayStart"] = (int)Session["DisplayStart"] - 10; //update with current display
            Label1.Text = "DisplayStart= " + Session["DisplayStart"];
        }
    }
}