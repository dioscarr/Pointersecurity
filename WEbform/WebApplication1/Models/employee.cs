using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.DB;


namespace WebApplication1
{
    public class employee
    {
        webformEntities1 db = new webformEntities1();

       [DisplayName("First Name")]
        public string FirstName { set; get; }
       [DisplayName("Last Name")]
        public string LastName { set; get; }


       public List<employee> loadperson()
       {

           List<employee> empdb = db.People.Select(c => new employee { FirstName = c.Firstname, LastName = c.Lastname }).ToList();

           return empdb;

       }
    }
}