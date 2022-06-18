﻿using Data.Repositories;
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
            HashedPassword hs = PasswordHasher.HashPassword("pass@!23");
            Users user = new Users("harmeet", hs.Hash,hs.Salt,DateTime.Now,false) ;
            BookRepository bk = new BookRepository();
            
            bk.CreateBook(user);
        }
    }
}
