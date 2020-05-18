using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Coursework___40091970.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}