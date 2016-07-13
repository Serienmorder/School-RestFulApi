using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Oauth.Helpers
{
    public class HashHelper
    {
            public static string Sha512(string input)
            {
            string salt = "poiu)(&*asdfjhkl(*&^2q3w54jhklNL0987)*(&&^(**%&^$54763hjiasdfjhknmklxcvbuHJLKKJHGIGHFafou";
                using (var sha = SHA512.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(input + salt);
                    var hash = sha.ComputeHash(bytes);
                
                    return Convert.ToBase64String(hash);
                }
            }
            public static string Sha512(string username, string password)
            {
                return Sha512(username.ToLower() + password);
            }
        
    }
}