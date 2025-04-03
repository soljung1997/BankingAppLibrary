using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppLibrary.Exceptions
{
    public enum AccountExceptionType
    {
        ACCOUNT_DOES_NOT_EXIST,
        ACCOUNT_ALREADY_EXIST,
        CREDIT_LIMIT_HAS_BEEN_EXCEEDED,
        INSUFFICIENT_FUNDS,
        NAME_NOT_ASSOCIATED_WITH_ACCOUNT,
        NO_OVERDRAFT_FOR_THIS_ACCOUNT,
        PASSWORD_INCORRECT,
        USER_DOES_NOT_EXIST,
        USER_ALREADY_EXIST,
        USER_NOT_LOGGED_IN
    }
}
