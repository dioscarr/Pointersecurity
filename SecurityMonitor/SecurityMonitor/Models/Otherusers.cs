using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecurityMonitor.Models
{
    public class Otherusers
    {
        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]        
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public DateTime created { get; set; }
        public int BuildingID { get; set; }
        [Required]
        public int aptID { get; set; }
        public int UserID { get; set; }
    }
}