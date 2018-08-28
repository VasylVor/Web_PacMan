using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Models;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace WebPacMan.Services
{
    public class DbCommand : IDbConmmand
    {
        readonly UserContext _context;
        public User user { get; set; }

        public DbCommand(UserContext context)
        {
            _context = context;
        }

        public string GetNickName(string userEmail)
        {
            user = _context.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            return user.NickName;
        }

        public async Task AddScoreAsync(int score, string userEmail)
        {
            user = await _context.Users.Where(u => u.Email == userEmail).FirstOrDefaultAsync();
            if (score > user.Score)
            {
                user.Score = score;
                _context.Attach(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public void HightScore()
        {
            var Users = _context.Users.OrderBy(u => u.Score);
        }
    }
}
