using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation
{
    public class Users
    {
        public int UserId { get; }
        public string UserName { get; set; }

        public byte[] HashPassword { get; set; }
        public byte[] Salt { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsLoggedIn { get; set; }

        public Users( string userName, byte[] hashPassword, byte[] salt, DateTime createdOn, bool isLoggedIn)
        {

            UserName = userName;
            HashPassword = hashPassword;
            Salt = salt;
            CreatedOn = createdOn;
            IsLoggedIn = isLoggedIn;
        }
    }
}
