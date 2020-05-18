using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Coursework___40091970.Models
{
    public class Signature
    {
        public int Id { get; set; }
        public virtual Member Signer { get; set; }
        public virtual Cause TheCause { get; set; }

    }
}