using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthExperiment.Models
{
    public class UserModel
    {
        public string _id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}