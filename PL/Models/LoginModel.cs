using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    internal class LoginModel
    {
        public LoginModel(bool admin = false)
        {
            AdminState = admin;
        }

        public bool AdminState { get; set; }
    }
}
