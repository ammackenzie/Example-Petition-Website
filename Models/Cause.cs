using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Coursework___40091970.Models
{
    public class Cause
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public virtual List<Signature> Signatures { get; set; }
        public virtual Member CauseOwner { get; set; }

        public Cause() {
            this.Signatures = new List<Signature>(); //set to empty
        }

        public void AddSignature(Signature sig)
        {
            this.Signatures.Add(sig);
        }
    }

}