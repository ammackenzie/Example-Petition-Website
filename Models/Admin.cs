using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Coursework___40091970.Models
{
    public class Admin : User
    {
        public string Permissions { get; set; }

        public Admin()
        {
            this.Permissions = "Admin";
        }
    }
}