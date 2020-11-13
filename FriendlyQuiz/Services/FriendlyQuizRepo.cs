using FriendlyQuiz.DbContexts;
using FriendlyQuiz.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendlyQuiz.Services
{
    public class FriendlyQuizRepo : IDisposable,IFriendlyQuizRepo
    {
        private readonly QuizDbContext db;
        public FriendlyQuizRepo(QuizDbContext _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Add<T>(T entity) where T : class
        {
            db.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            db.Remove(entity);
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

        public async Task<User> GetUser(int id)
        {
            return await db.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await db.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
