using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {

        public Names nameData { get; set; }
        public UserPersonalData UserPersonalData { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public User(Names n, UserPersonalData pd, string username, string pass)
        {
            this.nameData = n;
            this.UserPersonalData = pd;
            this.username = username;
            this.password = pass;
        }
    }
}
