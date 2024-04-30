using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly IInstituicaoService _service;
        public InstituicaoController(IInstituicaoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cargos = await _service.GetAllAsync();
                return View(cargos);
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
                var cargo = await _service.GetByIdAsync(id);
                if (cargo == null)
                {
                    return NotFound();
                }
                return View(cargo);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstituicaoModel instituicao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var retorno = await _service.AddAsync(instituicao);
                    TempData["MensagemSucesso"] = "Instituicao cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrado o Instituicao!";
                    return RedirectToAction("Index");
                }
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var cargo = await _service.GetByIdAsync(id);
                if (cargo == null)
                {
                    return NotFound();
                }
                return View(cargo);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstituicaoModel instituicao)
        {
            if (id != instituicao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(instituicao);
                    TempData["MensagemSucesso"] = "Instituicao atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Instituicao!";
                    return RedirectToAction("Index");
                }
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               bool instituicao =  await _service.DeleteAsync(id);
                if (instituicao)
                {
                    TempData["MensagemSucesso"] = "Funcionario excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Funcionario!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Funcionario!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
