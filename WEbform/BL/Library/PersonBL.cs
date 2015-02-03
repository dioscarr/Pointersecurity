using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using BL.Library;
using BL.DB;

namespace BL.Library
{
    
    public class PersonBL
    {

        WFEntities1 db = new WFEntities1();

        [DisplayName("First Name")]
        public string FirstName { set; get; }
        [DisplayName("Last Name")]
        public string LastName { set; get; }

       

        public List<PersonBL> loadperson() 
        { 
        
            List<PersonBL> empdb = db.People.Select(c => new PersonBL { FirstName = c.Firstname, LastName = c.Lastname }).ToList();

            return empdb;
        
        }
    }
}