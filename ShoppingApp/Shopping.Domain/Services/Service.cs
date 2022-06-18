using Shopping.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Authentication;

namespace Shopping.Domain.Services
{
    public class Service
    {
        private readonly UserRepository userRepository;
           


        public Service()
        {
            userRepository = new UserRepository();
        }

        public bool getPassword(string userName,string password)
        {

            
            HashedPassword hs=  userRepository.getPassword(userName);
            PasswordResult ps = new PasswordResult();
            if (hs != null)
            {
              ps = PasswordHasher.CheckPassword(password, hs);
                if (ps.Equals(PasswordResult.Correct))
                {
                 bool check= userRepository.setLoggedIn(userName,true);
                    return check;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
