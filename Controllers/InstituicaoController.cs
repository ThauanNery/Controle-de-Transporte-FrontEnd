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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
                }
            }
            return View(instituicao);
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
