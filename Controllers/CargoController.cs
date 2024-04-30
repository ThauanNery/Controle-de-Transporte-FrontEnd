using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class CargoController : Controller
    {
        private readonly ICargoService _cargoService;
        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var cargos = await _cargoService.GetAllAsync();
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
                var cargo = await _cargoService.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([Bind("NomeCargo")] CargoModel cargo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var retorno = await _cargoService.AddAsync(cargo);
                    TempData["MensagemSucesso"] = "Cargo cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar o Cargo!";
                    return RedirectToAction("Index");
                }
            }
            return View(cargo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var cargo = await _cargoService.GetByIdAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCargo")] CargoModel cargo)
        {
            if (id != cargo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cargoService.UpdateAsync(cargo);
                    TempData["MensagemSucesso"] = "Cargo atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Cargo!";
                    return RedirectToAction("Index");
                }
            }
            return View(cargo);
        }     
       
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               bool cargo =  await _cargoService.DeleteAsync(id);
                if (cargo)
                {
                    TempData["MensagemSucesso"] = "Cargo excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Cargo!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Cargo!";
                return RedirectToAction("Index");
            }
        }
    }
}
