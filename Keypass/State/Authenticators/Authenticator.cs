using Keypass.Domain;
using Keypass.Services.AuthenticationService;
using Keypass.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keypass.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }

        
        public bool IsLoggedIn => CurrentAccount != null;

        public Account CurrentAccount 
        {
            get { return _accountStore.CurrentAccount;}
            private set {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;

        public void Login(string username, string password)
        {
           CurrentAccount = _authenticationService.Login(username, password);
        }

        public void Logout()
        {
            CurrentAccount = null;
        }

        public RegistrationResult Register(string email, string username, string password, string confirmPassword)
        {
            return _authenticationService.Register(email, username, password,confirmPassword);
        }
    }
}
