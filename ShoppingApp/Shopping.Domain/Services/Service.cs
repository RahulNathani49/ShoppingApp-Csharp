using Shopping.Core.Entities;
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
            
            if (hs != null)
            {
                PasswordResult ps = PasswordHasher.CheckPassword(password, hs);
                if (ps == PasswordResult.Correct)
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

        public List<Products> GetProducts()
        {
            List<Products> products = userRepository.GetProducts();
            return products;
        }

        public int getUserId(string userName)
        {
           return userRepository.getUserId(userName);

       
        }

        public bool addToCart(int userID, int productId, int quantity)
        {

          bool exists=  userRepository.checkIfItemExists(userID,productId);
            if (exists)
            {
                return userRepository.updateQuantity(userID,productId,quantity);

            }
            else
            {
              return userRepository.addToCart(userID,productId,quantity);
            }
         
            
        }
    }

}
