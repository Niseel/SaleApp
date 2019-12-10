using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Session
{
    public class LoginSession
    {
        public int LoginID { get; set; }
        public string LoginName { get; set; }
        public string LoginMail { get; set; }
        public int LoginStatus { get; set; }
        public int LoginLevel { get; set; }

    }
}
