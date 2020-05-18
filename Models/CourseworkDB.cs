using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Final_Coursework___40091970.Models
{
    public class CourseworkDB : DbContext
    {
        public CourseworkDB() : base("courseworkdb")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CourseworkDB>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<CourseworkDB>());
        }

        public DbSet<Member> Userbase { get; set; }

        public DbSet<Admin> ClientUserbase { get; set; }
        public DbSet<Cause> Causes { get; set; }

        public DbSet<Signature> Signatures { get; set; }

        public System.Data.Entity.DbSet<Final_Coursework___40091970.Models.User> Users { get; set; }
    }
}