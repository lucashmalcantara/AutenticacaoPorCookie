using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutenticacaoPorCookie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutenticacaoPorCookie.Pages.Conta
{
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnPost(EntradaLoginModel entradaLoginModel)
        {
            if (!ModelState.IsValid)
                return Page();


            if (!UsuarioEstaAutenticado(entradaLoginModel.Usuario, entradaLoginModel.Senha))
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, entradaLoginModel.Usuario)
            };

            var usuarioIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(usuarioIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            return Redirect("/");
        }

        private bool UsuarioEstaAutenticado(string usuario, string senha)
        {
            // Simulação de autenticação no banco de dados.
            return true;
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Conta/Login");
        }
    }
}