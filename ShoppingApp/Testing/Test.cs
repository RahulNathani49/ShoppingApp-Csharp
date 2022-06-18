using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Authentication;

namespace Shopping.Presentation
{
    public class Test
    {
  public static void Main()
        {
            HashedPassword hs = PasswordHasher.HashPassword("key@123");
            Users user = new Users("Harmeet", hs.Hash,hs.Salt,DateTime.Now,false) ;
            BookRepository bk = new BookRepository();
            bk.CreateBook(user);
        }
    }
}
