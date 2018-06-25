using Blockchain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicImplementation
{
    public class HashSHA256 : IHash
    {
        public string Calculate(string value)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
