using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class MatriculaTransporteController : Controller
    {
        private readonly IMatriculaTransporteService _service;
        public MatriculaTransporteController(IMatriculaTransporteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var matriculas = await _service.GetAllAsync();
                return View(matriculas);
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
                var matriculas = await _service.GetByIdAsync(id);
                if (matriculas == null)
                {
                    return NotFound();
                }
                return View(matriculas);
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
        public async Task<IActionResult> Create([Bind("Matricula")] MatriculaTransporteModel mat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var retorno = await _service.AddAsync(mat);
                    TempData["MensagemSucesso"] = "Matricula de Transporte cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar a Matricula de Transporte!";
                    return RedirectToAction("Index");
                }
            }
            return View(mat);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var matricula = await _service.GetByIdAsync(id);
                if (matricula == null)
                {
                    return NotFound();
                }
                return View(matricula);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula")] MatriculaTransporteModel mat)
        {
            if (id != mat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(mat);
                    TempData["MensagemSucesso"] = "Matricula de Transporte atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar a Matricula de Transporte!";
                    return RedirectToAction("Index");
                }
            }
            return View(mat);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool matricula = await _service.DeleteAsync(id);
                if (matricula)
                {
                    TempData["MensagemSucesso"] = "Matricula de Transporte excluida com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir a Matricula de Transporte!";
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
