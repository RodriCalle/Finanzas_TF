using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Finanzas_TF.Models;

namespace Finanzas_TF.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityController(
          UserManager<Usuario> userManager,
          RoleManager<IdentityRole> roleManager,
          SignInManager<Usuario> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        private async Task CreateSuperUser(string email, string password, string nombre)
        {
            var userExists = await _userManager.FindByNameAsync(email);

            if (userExists == null)
            {
                var superUser = new Usuario()
                {
                    UserName = email,
                    Email = email,
                    Fullname = nombre,
                };
                if (await _roleManager.FindByNameAsync("Admin") == null)
                {
                    var role = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "Admin",
                        Id = Guid.NewGuid().ToString(),
                        NormalizedName = "ADMIN"
                    });
                }

                await _userManager.CreateAsync(superUser, password);
                await _userManager.AddToRoleAsync(superUser, "Admin");

            }
        }

        private async Task CreateUser(string email, string password, string nombre)
        {
            var userExists = await _userManager.FindByNameAsync(email);

            if (userExists == null)
            {
                var superUser = new Usuario()
                {
                    UserName = email,
                    Email = email,
                    Fullname = nombre,
                };
                if (await _roleManager.FindByNameAsync("Usuario") == null)
                {
                    var role = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "Usuario",
                        Id = Guid.NewGuid().ToString(),
                        NormalizedName = "USUARIO"
                    });
                }
                await _userManager.CreateAsync(superUser, password);
                await _userManager.AddToRoleAsync(superUser, "USUARIO");

            }
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await CreateSuperUser("fabriccio.tcortes@outlook.com", "W3tp0s123!", "Fabriccio Tornero");
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result != Microsoft.AspNetCore.Identity.SignInResult.Success)
                {
                    ViewBag.Error = "Credenciales incorrectas";
                    return View(userInfo);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = "Credenciales incorrectas";
            return View(userInfo);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}