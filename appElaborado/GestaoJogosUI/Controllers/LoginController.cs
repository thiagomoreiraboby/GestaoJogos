﻿using System.Security.Claims;
using System.Threading.Tasks;
using Dominio.Model;
using Dominio.Servico;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoJogosUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _context;
        public LoginController(IUsuarioRepositorio context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.LogarAsync(usuario);
                if (user != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Nome));
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nome));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                    });
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Usuario e Senha invalida!");
            return View(nameof(Index));
        }
    }
}