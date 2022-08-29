using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Names
    {
        public Names() { }
        public Names(string firstN, string fatherN, string FamilyN)
        {
            this.FirstName = firstN;
            this.FatherName = fatherN;
            this.FamilyName = FamilyN;

        }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string FamilyName { get; set; }

    }
}
