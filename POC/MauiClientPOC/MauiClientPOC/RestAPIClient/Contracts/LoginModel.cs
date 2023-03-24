using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIClient.Contracts
{
    public class LoginModel
    {
        public Session Session { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public bool SessionIsValidRequest { get; set; }
    }
}
