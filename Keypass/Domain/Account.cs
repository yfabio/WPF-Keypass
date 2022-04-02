using MvvmFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Domain
{
    public class Account 
    {
        private int id;

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        public override string ToString()
        {
            return Username;
        }
    }
}
