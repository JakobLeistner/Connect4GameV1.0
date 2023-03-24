using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIClient.Contracts
{
    public class UserReturnModel
    {
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public string LoginName { get; set; }
        public bool DeviceAccess { get; set; }
        public int Id { get; set; }
        public Session Session { get; set; }
        public int IdleTime { set; get; }
    }
}
