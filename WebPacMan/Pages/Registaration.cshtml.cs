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

namespace WebPacMan.Pages
{
    public class RegistarationModel : PageModel
    {
        private UserContext _db;
        [BindProperty]
        public User User { get; set; }

        public RegistarationModel(UserContext context)
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
                //_db.Users.Add(User);
                //await _db.SaveChangesAsync();
                //return RedirectToPage("Rules");
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == User.Email);
                if (user == null)
                {
                    _db.Users.Add(new User { Email = User.Email, Password = User.Password, NickName = User.NickName });
                    await _db.SaveChangesAsync();
                    await Authenticate(User.Email);

                    return RedirectToPage("Menu");
                }
                else
                    ModelState.AddModelError("", "Incorrect login and / or password");
            }
            return Page();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}