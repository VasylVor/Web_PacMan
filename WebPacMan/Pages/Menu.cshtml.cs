using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPacMan.Models;
using WebPacMan.Services;

namespace WebPacMan.Pages
{

    public class MenuModel : PageModel
    {
        IManager _manager;
        private GameField _game;
        private string Email { get; set; }
        public MenuModel(IManager manager)
        {
            _manager = manager;
        }

        public void OnGet(string email)
        {
            //Email = email;
            //Email = User.Identity.Name;
        }

        public IActionResult OnPostGame()
        {
            _game = new GameField();
            _manager.Add(_game.Id, _game);
            return RedirectToPage("Game", new { _game.Id });
        }

        public IActionResult OnPostHightScores()
        {
            return RedirectToPage("HightScores");
        }

        public IActionResult OnPostRules()
        {
            return RedirectToPage("Rules");
        }

        public IActionResult OnPostAbout()
        {
            return RedirectToPage("About");
        }
    }
}
