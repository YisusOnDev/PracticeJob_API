using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace PracticeJob.Core.Security
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public IConfiguration Configuration { get; set; }
        public PasswordGenerator(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string Hash(string password)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = Encoding.UTF8.GetBytes(Configuration["SecretKey"]);

            // derive a 256-bit subkey (use HMACSHA256 with 987,654 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 987654,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
