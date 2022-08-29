using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserPersonalData
    {
        public UserPersonalData() { }
        public UserPersonalData(string bd, string occ, string add)
        {
            this.BirthDate = bd;
            this.Occupation = occ;
            this.Address = add;
        }
        public string BirthDate { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }

    }
}
