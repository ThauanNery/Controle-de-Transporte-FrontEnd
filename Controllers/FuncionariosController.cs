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

                    int cargoId = funcionario.CargoId;
                    CargoModel cargo = await ObterCargoPorId(cargoId);
                    int departamentoId = funcionario.DepartamentoId;
                    DepartamentoModel departamento = await ObterDepartamentoPorId(departamentoId);


                        funcionario.Cargo = cargo;
                        funcionario.Departamento = departamento;

                        var retorno = await _service.AddAsync(funcionario);
                        return RedirectToAction(nameof(Index));
                   
                    
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
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
        public async Task<IActionResult> Edit(int id, FuncionariosModel funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(funcionario);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
                }
            }
            return View(funcionario);
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
        private async Task<CargoModel> ObterCargoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var cargo = await _cargoService.GetByIdAsync(id);

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
        private async Task<DepartamentoModel> ObterDepartamentoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var departamento = await _departService.GetByIdAsync(id);

            return departamento;
        }

    }
}
