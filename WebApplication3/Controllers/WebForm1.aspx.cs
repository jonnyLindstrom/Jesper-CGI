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
using System.Web.Services;
using System.Web.Script.Services;

namespace WebApplication3.Controllers {
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        [WebMethod]
        public static string[] GetNames(string prefix)
        {
            List<string> names = new List<string>();
            //logic
            return names.ToArray();
        }

        public void ListPersons ()
        {
            List<Person> people = new List<Person>(); //create people object
            people = (List<Person>)Session["sPeople"]; //fetch from session
            int i; //counter
            int startValue = Convert.ToInt32(Session["DisplayStart"]);
            if (startValue > Convert.ToInt32(Session["PersonsInList"]))
            { //stopValue not allowed to be higher that no of persons in list
                return; // no persons to show
            }

            int stopValue = Convert.ToInt32(Session["DisplayStop"]);
            if (stopValue > Convert.ToInt32(Session["PersonsInList"])) { //stopValue not allowed to be higher that no of persons in list
                stopValue = Convert.ToInt32(Session["PersonsInList"]);
            }
            ListBox1.Items.Clear(); //empty listbox
            for (i = startValue; i < stopValue; i++)
            {
                ListBox1.Items.Add(people[i].firstName.ToString() + " - " + people[i].lastName + " - " + people[i].age);
            }

            //enable or disable Prev o Next buttons
            if(startValue <= 0) { 
                btnPrev.Enabled = false;
            } else
            {
                btnPrev.Enabled = true;
            }

            if (stopValue >= Convert.ToInt32(Session["PersonsInList"]))
            {
                btnNext.Enabled = false;
            } else
            {
                btnNext.Enabled = true;
            }
        }

        public void LoadFile() //Read file, populate session object, list first 10 persons
        {
            try
            {
                List<Person> people = new List<Person>();
                using (StreamReader Reader = File.OpenText(Server.MapPath("Namnlista.txt")))
                {
                    string[] readLine;
                    int v;
                    char[] seperators = { ',' }; //CSV file
                    string sFirstName = "";
                    string sLastName = "";
                    int iAge = 0;
                    int iPos = 4;
                    string data;
                    bool isIntPos0, isIntPos1, isIntPos2 = false;

                    while ((data = Reader.ReadLine()) != null) //read file line by line until EOF
                    {
                        readLine = data.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                        //find age column position in readLine
                        isIntPos0 = int.TryParse(readLine[0], out v);
                        isIntPos1 = int.TryParse(readLine[1], out v);
                        if (readLine.Length > 2) { // to avoid exception if only 2 colummns
                            isIntPos2 = int.TryParse(readLine[2], out v);
                        };
                        if (isIntPos0) iPos = 0;
                        if (isIntPos1) iPos = 1;
                        if (isIntPos2) iPos = 2;                        
                        
                        switch (iPos) //age column position in readLine
                        {
                            case 0: //age in column 0
                                {
                                    iAge = v;
                                    sFirstName = readLine[1]; 
                                    if (readLine.Length > 2) sLastName = readLine[2]; 
                                    break;
                                }
                            case 1: //age in column 1
                                { 
                                    iAge = v; 
                                    sFirstName = readLine[0]; 
                                    if (readLine.Length > 2) sLastName = readLine[2]; 
                                    break;
                                }
                            case 2: //age in column 2
                                { 
                                    if (readLine.Length > 2) iAge = v; 
                                    sFirstName = readLine[0]; 
                                    sLastName = readLine[1]; 
                                }
                                break;
                            default:
                                // Should not come here, error
                                throw new System.InvalidProgramException("Should not be here!!");
                        }                       
                        /*if (isIntPos0)
                        { //age in first column
                            iAge = v; 
                            sFirstName = readLine[1]; //first name
                            if (readLine.Length > 2) //verify we have 3 values in row
                            {
                                sLastName = readLine[2]; //last name
                            }
                        }*/

                        //bool isIntPos1 = int.TryParse(readLine[1], out v); //is integer?
                        /*if (isIntPos1)
                        { //integer in second column
                            iAge = v; //age
                            sFirstName = readLine[0]; //first name
                            if (readLine.Length > 2)
                            {
                                sLastName = readLine[2]; //last name
                            }
                        }*/

                        /*if (readLine.Length > 2) //does third column exist
                        {
                            //bool isIntPos2 = int.TryParse(readLine[2], out v); //is integer?
                            if (isIntPos2)
                            { //integer in third column
                                if (readLine.Length > 2)
                                {
                                    iAge = v; //age
                                } //age
                                sFirstName = readLine[0]; //first name
                                sLastName = readLine[1]; //last name
                            }
                        }*/
                        people.Add(new Person(sFirstName, sLastName, iAge)); // add person to people object
                    }
                    //now file is loaded into people object
                    //store people object into session object for use going forward
                    Session["sPeople"] = people; //Populate sessionobject to be used going forward
                    Session["PersonsInList"] = people.Count;
                    //List 10 first person in list
                    Session["DisplayStart"] = 0; //position of first person to list 
                    Session["DisplayStop"] = 9; 
                    Label1.Text = "DisplayStart= " + Session["DisplayStart"]; //debug
                    //int startValue = Convert.ToInt32(Session["DisplayStart"]); //start list from this position
                    //int stopValue = startValue + 9;
                    ListPersons();
                    /*int i;
                    for (i = startValue; i < endValue; i++)
                    {
                        ListBox1.Items.Add(people[i].firstName.ToString() + " - " + people[i].lastName + " - " + people[i].age);
                    }*/
                    
                    //lblText.Text = "<table><tr><td>FirstName</td><td>LastName</td><td>Age</td></tr>";
                    //contentDiv.InnerHtml = "<table><tr><td>FirstName</td><td>LastName</td><td>Age</td></tr>"; //starta tabell m rubrik
                    /*foreach (Person per in people)
                    {
                        if (i < 10) //list 10 persons
                        {
                            ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                            //ListItem1.Text = per.firstName;
                            //ListItem2.Text = per.lastName;
                            //ListItem3.Text = per.age.ToString();
                            //contentDiv.InnerHtml += "<tr><td class=\"klassnamn\">" + per.firstName + "</td><td>" + per.lastName + "</td><td>" + per.age + "</td></tr>";
                            //lblText.Text += "<tr><td class=\"klassnamn\">" + per.firstName + "</td><td>" + per.lastName + "</td><td>" + per.age + "</td></tr>";
                        }
                        i++;
                    }*/
                    //contentDiv.InnerHtml += "</ table >"; //stäng tabell
                    //lblText.Text += "</ table >"; //stäng tabell
                    //Session["sPeople"] = people; //Populate sessionobject to be used going forward
                }
            }
            catch (Exception ex)
            {
                lblReadText.Text = "Could not read from the file, the errormessage is: " + ex.ToString();
            }
        }

        protected void btnSaveListToExcel_Click(object sender, EventArgs e)
        {
            //need to have excel/office installed and use API's
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
            //List<Person> people = new List<Person>(); //create people object
            //people = (List<Person>)Session["sPeople"]; //fetch from session

            //ListBox1.Items.Clear();
            //int start = (int)Session["DisplayStart"] + 10;
            //int i = 0;
            //int stop = start + 9;

            //List next 10 persons in list
            //Session["DisplayStart"] = 0; //debug varför har jag denna? 
            //Label1.Text = "DisplayStart= " + Session["DisplayStart"]; //debug
            //int startValue = Convert.ToInt32(Session["DisplayStart"]); //start list from this position
            //int endValue = startValue + 9;

            /*for (i = startValue; i < endValue; i++)
            {
                ListBox1.Items.Add(people[i].firstName.ToString() + " - " + people[i].lastName + " - " + people[i].age);
            }*/

            /*foreach (Person per in people)
            {
                if (i >= start && i <= stop)
                {
                    ListBox1.Items.Add(per.firstName + " - " + per.lastName + " - " + per.age);
                }
                i++;
            }*/
            //Session["sPeople"] = people; //update session w sorted list
            if ((int)Session["DisplayStart"] <= (int)Session["PersonsInList"])
            {
                Session["DisplayStart"] = (int)Session["DisplayStart"] + 10; //update with current display
                Session["DisplayStop"] = (int)Session["DisplayStop"] + 10; //update with current display
                //Label1.Text = "DisplayStart= " + Session["DisplayStart"];
                ListPersons();
            }    
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            /*List<Person> people = new List<Person>(); //create people object
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
            Label1.Text = "DisplayStart= " + Session["DisplayStart"];*/

            if ((int)Session["DisplayStart"] >= 10) {  // make sure we are within list
                Session["DisplayStart"] = (int)Session["DisplayStart"] - 10; //update with current display
                Session["DisplayStop"] = (int)Session["DisplayStop"] - 10; //update with current display
                ListPersons();
            }
        }
    }
}