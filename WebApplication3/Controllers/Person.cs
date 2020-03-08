using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Controllers
{
    public class Person //: IEquatable<Person>
    {
        public String firstName;
        public String lastName;
        public int age;
        
        public Person(string firstname, string lastname, int age) //constructor
        {
            this.firstName = firstname;
            this.lastName = lastname;
            this.age = age;
        }
    }
}