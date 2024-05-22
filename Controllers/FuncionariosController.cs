using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly IFuncionariosService _service;
        private readonly ICargoService _cargoService;
        private readonly IDepartamentoService _departService;
        public FuncionariosController(IFuncionariosService service, ICargoService cargoService, IDepartamentoService departService)
        {
            _service = service;
            _cargoService = cargoService;
            _departService = departService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var funcionarios = await _service.GetAllAsync();
                return View(funcionarios);
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
                var funcionario = await _service.GetByIdAsync(id);
                if (funcionario == null)
                {
                    return NotFound();
                }
                return View(funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        public async Task<IActionResult> Create()
        {
            var cargos = await ObterCargos();
            ViewBag.Cargos = new SelectList(cargos, "Id", "NomeCargo");
            
            var departamentos = await ObterDepartamentos();
            ViewBag.Departamentos = new SelectList(departamentos, "Id", "NomeDepartamento");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuncionariosModel funcionario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                      var retorno = await _service.AddAsync(funcionario);
                        TempData["MensagemSucesso"] = "Funcionario cadastrado com sucesso!";
                        return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrado o Funcionario!";
                    return RedirectToAction("Index");
                }
            }
            return View(funcionario);
        }

        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                var cargos = await ObterCargos();
                ViewBag.Cargos = new SelectList(cargos, "Id", "NomeCargo");
                var departamentos = await ObterDepartamentos();
                ViewBag.Departamentos = new SelectList(departamentos, "Id", "NomeDepartamento");

                var funcionario = await _service.GetByIdAsync(id);
                if (funcionario == null)
                {
                    return NotFound();
                }
                return View(funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuncionariosModel funcionarios)
        {
            if (id != funcionarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(funcionarios);
                    TempData["MensagemSucesso"] = "Funcionario atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Funcionario!";
                    return RedirectToAction("Index");
                }
            }
            return View(funcionarios);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
              bool funcionario = await _service.DeleteAsync(id);
                if (funcionario)
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
                return RedirectToAction("Index");
            }
        }

        #region
        private async Task<List<CargoModel>> ObterCargos()
        {
            var cargo = new List<CargoModel>();


            var cargos = await _cargoService.GetAllAsync();


            if (cargos != null)
            {

                cargo.AddRange(cargos);
            }

            return cargo;
        }
       
        private async Task<List<DepartamentoModel>> ObterDepartamentos()
        {
            var departamento = new List<DepartamentoModel>();


            var departamentos = await _departService.GetAllAsync();


            if (departamentos != null)
            {

                departamento.AddRange(departamentos);
            }

            return departamento;
        }
        #endregion
    }
}
