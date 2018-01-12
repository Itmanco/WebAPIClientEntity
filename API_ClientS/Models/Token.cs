using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ClientS.Models
{
    public class Token
    {
        public Users User { get; set; }
        public string TokenValue { get; set; }

        public Token()
        {

        }

        public Token(Users user)
        {
            this.User = user;
        }

        public Token(string token, Users user)
        {
            TokenValue = token;
            this.User = user;
        }
    }
}
