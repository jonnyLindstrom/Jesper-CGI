using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Controllers
{
    public class People
    {
        protected List<Person> ThePeople { get; set; }
        protected String fileName { get; set; }
        protected int listLength { get; set; }

        public People(String fileName) //constructor
        {
            ThePeople = new List<Person>();
            if (fileName == null) throw new ArgumentNullException(" Filename is missing ");
            this.fileName = fileName;
            this.listLength = 0;
        }

        public void AddPerson(Person newPerson)
        {
            ThePeople.Add(newPerson);
        }

        /*public People GetByLastName(string LastName)
        {
            LastName = LastName ?? string.Empty;
            People TheSmits = new People(Repository);
            TheSmits.AddRange(ThePeople.FindAll(x => x.LastName.ToLower() == LastName.ToLower()));
            return TheSmits;
        }*/
    }
}