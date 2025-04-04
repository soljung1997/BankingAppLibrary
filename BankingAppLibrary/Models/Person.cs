using BankingAppLibrary.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingAppLibrary.Exceptions;

namespace BankingAppLibrary.Models
{
    public class Person
    {
        // Private field for storing the password
        private string password;

        // Public event for login event handling
        public event EventHandler<LoginEventArgs> OnLogin;

        // Properties with specified accessors
        public string SIN { get; }
        public string Name { get; }
        public bool IsAuthenticated { get; private set; }

        // Constructor
        public Person(string name, string sin)
        {
            Name = name;
            SIN = sin;

            // Password is set to the first three characters of SIN
            password = sin.Substring(0, 3);
        }

        // Login method
        public void Login(string passwordAttempt)
        {
            if (passwordAttempt != password)
            {
                IsAuthenticated = false;

                // Raise OnLogin event with failure details
                OnLogin?.Invoke(this, new LoginEventArgs(Name, false, LoginEventType.Login));

                // Throw an AccountException
                throw new AccountException(AccountExceptionType.PASSWORD_INCORRECT);
            }

            IsAuthenticated = true;

            // Raise OnLogin event with success details
            OnLogin?.Invoke(this, new LoginEventArgs(Name, true, LoginEventType.Login));
        }

        // Logout method
        public void Logout()
        {
            IsAuthenticated = false;

            // Raise OnLogin event with logout details
            OnLogin?.Invoke(this, new LoginEventArgs(Name, true, LoginEventType.Logout));
        }

        // Override ToString() method
        public override string ToString()
        {
            return $"{Name} [{SIN}] {(IsAuthenticated ? "authenticated" : "not authenticated")}";
        }
    }

}
