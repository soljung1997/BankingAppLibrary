using BankingAppLibrary.Models;
using BankingAppLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppLibrary.Events
{
    public class LoginEventArgs : EventArgs
    {
        // Properties
        public string PersonName { get; }
        public bool Success { get; }
        public DayTime Time { get; }
        public LoginEventType EventType { get; }

        // Constructor
        public LoginEventArgs(string name, bool success, LoginEventType loginEventType)
            : base() // Invoke the base class constructor 
        {
            // Assign values to the properties
            PersonName = name;
            Success = success;
            EventType = loginEventType;

            // Initialize Time using the Now property in the static Util class
            Time = Util.Now;


        }
    }
}
