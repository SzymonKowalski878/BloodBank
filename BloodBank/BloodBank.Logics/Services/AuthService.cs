using BloodBank.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using BloodBank.Logics.Repositories;
using System.Security.Cryptography;
using BloodBank.Logics.Users.DataHolders;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace BloodBank.Logics.Services
{
    public class AuthService:IAuthService
    {
        private Lazy<IConfiguration> _configuration;
        protected IConfiguration Configuration => _configuration.Value;

        private Lazy<IUserRepository> _userRepository;
        protected IUserRepository UserRepository => _userRepository.Value;

        public AuthService(Lazy<IConfiguration> configuration,
            Lazy<IUserRepository> userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password,
                salt,
                10000);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt,
                0,
                hashBytes,
                0,
                16);

            Array.Copy(hash,
                0,
                hashBytes,
                16,
                20);

            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public bool VerifyPassword(string login,
            string password)
        {

            var passwordDb = UserRepository.GetUserPassword(login);

            if (string.IsNullOrEmpty(passwordDb) == true)
            {
                return false;
            }

            byte[] hashbytes = Convert.FromBase64String(passwordDb);

            byte[] salt = new byte[16];
            Array.Copy(hashbytes,
                0,
                salt,
                0,
                16);

            var pbkdf2 = new Rfc2898DeriveBytes(password,
                salt,
                10000);

            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashbytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public UserToken GenerateToken(string login, bool isWorker,bool isDonator)
        {
            var secretKey = Configuration["SecretKey"];

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim("IsWorker", isWorker.ToString()),
                    new Claim("IsDonator", isDonator.ToString())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserToken()
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
