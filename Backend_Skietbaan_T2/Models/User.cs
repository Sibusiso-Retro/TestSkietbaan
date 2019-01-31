using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Skietbaan_T2.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }
}
