using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ResetData
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
