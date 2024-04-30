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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
                }
            }
            return View(transporte);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }
    }
}
