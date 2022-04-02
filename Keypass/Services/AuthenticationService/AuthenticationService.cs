using Keypass.Domain;
using Keypass.Exceptions;
using Keypass.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace Keypass.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public Account Login(string username, string password)
        {
            Account storedAccount = _unitOfWork.Accounts.GetByUsername(username);

            if (storedAccount == null) {
                throw new UserNotFoundException(username);
            }

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedAccount.Password, password);

            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return storedAccount;
        }

        public RegistrationResult Register(string email, string username, string password, string confirmPassword)
        {

            RegistrationResult result = RegistrationResult.Success;

            if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }


            Account emailAccount = _unitOfWork.Accounts.GetByEmail(email);

            if (emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            Account usernameAccount = _unitOfWork.Accounts.GetByUsername(username);

            if (usernameAccount != null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                Account account = new Account { Email = email, Username = username, Password = hashedPassword};

                _unitOfWork.Accounts.Add(account);
                _unitOfWork.Save();
            }

            return result;
        }
    }
}
