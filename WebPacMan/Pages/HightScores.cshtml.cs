using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPacMan.Models;

namespace WebPacMan.Pages
{
    public class HightScoresModel : PageModel
    {
        private UserContext db;
        public List<User> Users { get; set; }


        public HightScoresModel(UserContext context)
        {
            db = context;
        }
        public void OnGet()
        {
            var score = db.Users.OrderByDescending(u => u.Score);
            Users = score.ToList();
        }
    }
}