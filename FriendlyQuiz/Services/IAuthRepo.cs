using FriendlyQuiz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendlyQuiz.Services
{
    public interface IAuthRepo
    {
        Task<User> Login(string userName, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExist(string userName);
    }
}
