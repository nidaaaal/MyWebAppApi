using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models
{
    public class Users
    {

        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName { get; set; } = null;

        public DateTime DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        public byte? Age { get; set; }

        public string Address { get; set; }

        public string City { get; set; } = null;
        public string State { get; set; } = null;

        public int ZipCode { get; set; }

        public string Phone { get; set; }
        public string Mobile { get; set; } = null;
    }

}