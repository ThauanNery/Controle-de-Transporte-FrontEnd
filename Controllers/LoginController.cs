using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public LoginController(IUsuarioService service)
        {
            _usuarioService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EntrarAsync(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string login = model.Login;
                    UsuarioModel usuario = await ObterLogin(login);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(model.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou Senha inválido!";
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        private async Task<UsuarioModel> ObterLogin(string login)
        {
            
            var usuario = await _usuarioService.BuscarPorLogin(login);
            return usuario;
        }
    }
}
