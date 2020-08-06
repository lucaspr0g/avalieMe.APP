using System;
using System.Collections.Generic;
using System.Text;

namespace AvalieMe.APP.Models
{
    public class TokenAcesso
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
