using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Exceptions
{
    public class UserNotFoundException : Exception
    {

        public string Username { get; set; }

        public UserNotFoundException(string username) 
        {
            Username = username;
        }
        public UserNotFoundException(string message, string username) : base(message) 
        {
            Username = username;
        }
        public UserNotFoundException(string message, Exception inner, string username) : base(message, inner) 
        {
            Username = username;
        }
        
    }
    
    
}
