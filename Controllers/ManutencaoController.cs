using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class ManutencaoController : Controller
    {
        private readonly IManutencaoService _service;
        public ManutencaoController(IManutencaoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var manutencaos = await _service.GetAllAsync();
                return View(manutencaos);
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
                var manutencaos = await _service.GetByIdAsync(id);
                if (manutencaos == null)
                {
                    return NotFound();
                }
                return View(manutencaos);
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
        public async Task<IActionResult> Create([Bind("TipoManutencao,Custo")] ManutencaoModel manutencao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var retorno = await _service.AddAsync(manutencao);
                    TempData["MensagemSucesso"] = "Manutenção cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar o Manutenção!";
                    return RedirectToAction("Index");
                }
            }
            return View(manutencao);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var manutencao = await _service.GetByIdAsync(id);
                if (manutencao == null)
                {
                    return NotFound();
                }
                return View(manutencao);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoManutencao,Custo")] ManutencaoModel manutencao)
        {
            if (id != manutencao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(manutencao);
                    TempData["MensagemSucesso"] = "Manutenção atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Manutenção!";
                    return RedirectToAction("Index");
                }
            }
            return View(manutencao);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool manutencao = await _service.DeleteAsync(id);
                if (manutencao)
                {
                    TempData["MensagemSucesso"] = "Manutenção excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Manutenção!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Manutenção!";
                return RedirectToAction("Index");
            }
        }
    }
}
