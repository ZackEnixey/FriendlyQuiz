using FriendlyQuiz.DbContexts;
using FriendlyQuiz.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FriendlyQuiz.Services
{
    public class AuthRepo : IAuthRepo,IDisposable
    {
        private readonly QuizDbContext db;
        public AuthRepo(QuizDbContext _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(db));
        }


        public async Task<User> Login(string userName, string password)
        {
            var user = await db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            if (!CheckPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;

        }

        private bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac= new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var range = Enumerable.Range(0, computedHash.Length);
                foreach(var i in range)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UserName = user.UserName.ToLower();
            await db.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string userName)
        {
           if(await db.Users.AnyAsync(x => x.UserName == userName))
            {
                return true;
            }
            return false;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
