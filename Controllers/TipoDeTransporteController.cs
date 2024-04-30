using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class TipoDeTransporteController : Controller
    {
        private readonly ITipodeTransporteService _service;
        public TipoDeTransporteController(ITipodeTransporteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var transporte = await _service.GetAllAsync();
                return View(transporte);
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
                var transporte = await _service.GetByIdAsync(id);
                if (transporte == null)
                {
                    return NotFound();
                }
                return View(transporte);
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
        public async Task<IActionResult> Create([Bind("NomeTransporte")] TipoDeTransporteModel transporte)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var retorno = await _service.AddAsync(transporte);
                    TempData["MensagemSucesso"] = "Tipo de Transporte cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrado o Tipo de Transporte!";
                    return RedirectToAction("Index");
                }
            }
            return View(transporte);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var transporte = await _service.GetByIdAsync(id);
                if (transporte == null)
                {
                    return NotFound();
                }
                return View(transporte);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeTransporte")] TipoDeTransporteModel transporte)
        {
            if (id != transporte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(transporte);
                    TempData["MensagemSucesso"] = "Tipo de Transporte atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Tipo de Transporte!";
                    return RedirectToAction("Index");
                }
            }
            return View(transporte);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               bool tpTransporte = await _service.DeleteAsync(id);
                if (tpTransporte)
                {
                    TempData["MensagemSucesso"] = "Tipo de Transporte excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Tipo de Transporte!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Tipo de Transporte!";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
