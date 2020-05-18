using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Coursework___40091970.Models
{
    public class Member : User
    {
       public virtual List<Cause> Causes { get; set; }

        public virtual List<Signature> Signatures{ get; set; }

        public Member() {
            this.Causes = new List<Cause>(); //set to empty
            this.Signatures = new List<Signature>();

        }
        public Member(String email, String password, String firstName, String lastName)
        {
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Causes = new List<Cause>(); //set to empty
        }//for use with base account generation

        public void updateCauses(Cause newCause)
        {
            this.Causes.Add(newCause);
        }
    }
}