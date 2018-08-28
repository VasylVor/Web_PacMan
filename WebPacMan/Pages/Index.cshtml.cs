using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebPacMan.Models;
using WebPacMan.Services;

namespace WebPacMan.Pages
{
    public class IndexModel : PageModel
    {
        private UserContext _db;
        [BindProperty]
        public User User { get; set; }

        public IndexModel(UserContext context)
        {
            _db = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostRegsteration()
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == User.Email && u.Password == User.Password);
                if (user != null)
                {
                    await Authenticate(User.Email);
                    return RedirectToPage("Menu", new { User.Email });
                }
                else
                    ModelState.AddModelError("", "Incorrect login and/or password!!");
            }
            return Page();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
