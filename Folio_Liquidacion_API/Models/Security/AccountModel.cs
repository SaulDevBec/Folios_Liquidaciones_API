using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Models.Security
{
    public class AccountModel
    {
        private string user;
        private string userIni;
        private string password;
        private string message;
        private string redirectTo;
        private UserModel mUser;

        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public string UserIni { get => userIni; set => userIni = value; }
        public string Message { get => message; set => message = value; }
        public string RedirectTo { get => redirectTo; set => redirectTo = value; }
        public UserModel MUser { get => mUser; set => mUser = value; }
    }
}