using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Coursework___40091970.Models
{
    public class Data
    {
        public CourseworkDB db = new CourseworkDB();

        private static Data uniqueInstance; //singleton class
        private Data()
        {
            //class for initialising and populating database on initial database entries
            Member testMember = new Member { FirstName = "Andy", Email = "andy@gmail.com", LastName = "Macdrew", Password = "andypass" };
            Member testMember2 = new Member { FirstName = "Jimmy", Email = "jimmy@gmail.com", LastName = "Neutron", Password = "jimmypass" };
            Member testMember3 = new Member { FirstName = "Sarah", Email = "sarah@gmail.com", LastName = "Conner", Password = "sarahpass" };
            Member testMember4 = new Member { FirstName = "Tom", Email = "tom@gmail.com", LastName = "Smee", Password = "tompass" };

            Admin testAdmin = new Admin { FirstName = "Billy", Email = "billy@gmail.com", LastName = "Bob", Password = "billypass" };
            Admin testAdmin2 = new Admin { FirstName = "Lisa", Email = "lisa@gmail.com", LastName = "Lavish", Password = "lisapass" };
            db.ClientUserbase.Add(testAdmin);
            db.ClientUserbase.Add(testAdmin2);

            Cause cause1 = new Cause { Title = "Save the Whales", Description = "Whales are under treat from over fishing and we need to stop this. Sign this cause and boycott big fishing industries.", Action = "Boycott Business", CauseOwner = testMember, Category = "Environment" };
            Cause cause2 = new Cause { Title = "Make Google Pay Tax", Description = "Google isn't paying their taxes! Sign this cause and take political action against this!", Action = "Politically Campaign", CauseOwner = testMember, Category = "Tax Avoidance" };
            Cause sampleCause = new Cause
            {
                Title = "End E Corp's Washington Township Plant Emissions",
                Description = "In 1993, an alleged gas leak took place in the factories of E Corp in Washington Township, Bergen County, NJ., resulting in the leakage of chemicals with dangerous levels of toxicity. In the ensuing 24 months, 26 employees died from similar types of leukemia including Edward Alderson, Elliot and Darlene Alderson's father, as well as Emily Moss, the mother of Angela Moss. A class action lawsuit filed against E Corp by the surviving victims was dismissed; E Corp claimed there was no direct evidence linking their factories to the diagnoses. According to the release of E Corp corporate data from fsociety, the dangerous toxicity levels of the E Corp facilities was covered up by three high-level executives, Jim Chutney, Saul Weinberg and Terry Colby, as it \"would not be cost-effective to retool the current systems in place even if there are potential lawsuits.\" However, E Corp and legal analysts support the claim that there is not enough evidence to suggest a cover up.",
                Action = "Boycott Business",
                CauseOwner = testMember2,
                Category = "Environmental Impact"
            };

            Cause sampleCause2 = new Cause
            {
                Title = "Ban Cars in City Centre",
                Description = "A ban of all personal motor vehicles in the city centre. This will improve safety conditions after many fatal accidents, which has left pedestrians afraid of the city centre and reduced tourist traffic to Edinburgh.",
                Action = "Share Message",
                CauseOwner = testMember4,
                Category = "Environmental Impact"
            };

            Cause sampleCause3 = new Cause
            {
                Title = "Install Lighting in Edinburgh Parks",
                Description = "We need to install more lights in Edinburgh parks. This is especially important to reduce anti-social behaviour, and to improve conditions for dog-walkers who need to use areas late at night.",
                Action = "Share Message",
                CauseOwner = testMember3,
                Category = "Environmental Impact"
            };

            Signature sig1 = new Signature { Signer = testMember2 };
            Signature sig2 = new Signature { Signer = testMember3 };
            Signature sig3 = new Signature { Signer = testMember2 };
            Signature sig4 = new Signature { Signer = testMember3 };
            Signature sig5 = new Signature { Signer = testMember2 };
            Signature sig6 = new Signature { Signer = testMember3 };
            Signature sig7 = new Signature { Signer = testMember };

            List<Signature> signers = new List<Signature>();
            signers.Add(sig1);
            signers.Add(sig2);

            cause1.AddSignature(sig1);
            cause1.AddSignature(sig2);

            cause2.AddSignature(sig3);
            cause2.AddSignature(sig4);

            db.Causes.Add(cause1);
            db.Causes.Add(cause2);
            db.Causes.Add(sampleCause);
            db.Causes.Add(sampleCause2);
            db.Causes.Add(sampleCause3);
            db.Userbase.Add(testMember2);
            db.Userbase.Add(testMember3);
            db.Userbase.Add(testMember4);

            db.SaveChanges();
            sampleCause.AddSignature(sig5);
            sampleCause.AddSignature(sig6);
            sampleCause.AddSignature(sig7);

            
            db.SaveChanges();
        }

        public static Data getInstance()
        {
            if(uniqueInstance == null)
            {
                uniqueInstance = new Data();
            } else
            {
                //do nothing
            }

            return uniqueInstance;
        }
    }
}