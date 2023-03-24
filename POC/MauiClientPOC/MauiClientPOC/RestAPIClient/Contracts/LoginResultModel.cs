using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIClient.Contracts
{
    public class LoginResultModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public UserReturnModel UserModel { get; set; }
    }
}
