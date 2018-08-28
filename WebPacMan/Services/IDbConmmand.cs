using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPacMan.Models;

namespace WebPacMan.Services
{
    interface IDbConmmand
    {
        User user { get; set; }

        string GetNickName(string userEmail);
        Task AddScoreAsync(int score, string userEmail);
    }
}
