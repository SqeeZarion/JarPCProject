using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebAuthCommon
{
    public class AuthOption
    {
        public string Issuer { get; set; } //той, хто згенерував токен
        public string Audience { get; set; } //для кого призначений
        public string Secret { get; set; } // для генерації симетричному шифруванні
        public int TokenLifetime { get; set; } //життя токену

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedSecret = sha256.ComputeHash(Encoding.ASCII.GetBytes(Secret));
                return new SymmetricSecurityKey(hashedSecret);
            }
        }
    }
}