using Keypass.Domain;
using System;
using System.Collections.Generic;
using Keypass.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.Services.AuthenticationService
{

    public enum RegistrationResult 
    {
        Success,
        PasswordDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }

    public interface IAuthenticationService
    {


        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="username">The user's name</param>
        /// <param name="password">The user's password</param>
        /// <param name="confirmPassword">The user's confirmed password</param>
        /// <returns cref="Exception">Thrown if the registration fails</returns>        
        RegistrationResult Register(string email, string username, string password, string confirmPassword);


        /// <summary>
        /// Get an account for a user's credentials.
        /// </summary>
        /// <param name="username">The user's name</param>
        /// <param name="password">The user's password</param>
        /// <returns cref="UserNotFoundException">Thrown if the user does not exist</returns>
        /// <returns cref="InvalidPasswordException">Thrown if the password is invalid</returns>
        /// <returns cref="Exception">Thrown if the login fails.</returns>
        Account Login(string username, string password);
    }
}
