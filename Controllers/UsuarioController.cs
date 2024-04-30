using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _service;
        private readonly IFuncionariosService _funcService;
        public UsuarioController(IUsuarioService service, IFuncionariosService funcService)
        {
            _service = service;
            _funcService = funcService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarios = await _service.GetAllAsync();
                return View(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var usuarios = await _service.GetByIdAsync(id);
                if (usuarios == null)
                {
                    return NotFound();
                }
                return View(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        public async Task<IActionResult> Create()
        {
            var funcionarios = await ObterFuncionarios();
            ViewBag.Funcionarios = new SelectList(funcionarios, "Id", "NomeFuncionario");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    int funcionarioId = usuario.FuncionarioId;
                    FuncionariosModel funcionario = await ObterFuncionarioPorId(funcionarioId);

                    if (funcionario != null)
                    {

                        usuario.Funcionarios = funcionario;

                        var retorno = await _service.AddAsync(usuario);
                        TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound("Funcionario não encontrada");
                    }
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrado o Usuario!";
                    return RedirectToAction("Index");
                }
            }
            return View(usuario);
        }




        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                var funcionarios = await ObterFuncionarios();
                ViewBag.Funcionarios = new SelectList(funcionarios, "Id", "NomeFuncionario");

                var usuario = await _service.GetByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioModel usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(usuario);
                    TempData["MensagemSucesso"] = "Usuario atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Usuario!";
                    return RedirectToAction("Index");
                }
            }
            return View(usuario);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               bool usuario = await _service.DeleteAsync(id);
                if (usuario)
                {
                    TempData["MensagemSucesso"] = "Usuario excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Usuario!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Usuario!";
                return RedirectToAction(nameof(Index));
            }
        }


        private async Task<List<FuncionariosModel>> ObterFuncionarios()
        {
            var funcionario = new List<FuncionariosModel>();


            var funcionarios = await _funcService.GetAllAsync();


            if (funcionarios != null)
            {

                funcionario.AddRange(funcionarios);
            }

            return funcionario;
        }
        private async Task<FuncionariosModel> ObterFuncionarioPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var funcionario = await _funcService.GetByIdAsync(id);

            return funcionario;
        }
    }
}
