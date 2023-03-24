using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIClient.Contracts
{
    public class Session
    {
        public string Language { get; set; }
        public DateTime LastActivity { get; set; }
        public string LoginName { get; set; }
        public DateTime LoginTime { get; set; }
        public string SessionId { get; set; }
        public string TrackingId { get; set; }
        public int UserId { get; set; }
    }
}
