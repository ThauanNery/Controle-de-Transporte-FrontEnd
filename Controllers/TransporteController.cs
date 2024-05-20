using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class TransporteController : Controller
    {
        private readonly ITransporteService _service;
        private readonly IFuncionariosService _funcService;
        private readonly ITipodeTransporteService _tpService;
        private readonly IMatriculaTransporteService _matService;
        private readonly IManutencaoService _manuService;
        public TransporteController(ITransporteService service, IFuncionariosService funcService, ITipodeTransporteService tpService, IMatriculaTransporteService matService, IManutencaoService manuService)
        {
            _service = service;
            _funcService = funcService;
            _tpService = tpService;
            _matService = matService;
            _manuService = manuService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var transportes = await _service.GetAllAsync();
                return View(transportes);
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

        public async Task<IActionResult> Create()
        {
            var func = await ObterFuncionarios();
            ViewBag.Funcionarios = new SelectList(func, "Id", "NomeFuncionario");

            var mat = await ObterMatriculasTransporte();
            ViewBag.Matriculas = new SelectList(mat, "Id", "Matricula");
            
            var tp = await ObterTiposTransporte();
            ViewBag.TiposTransporte = new SelectList(tp, "Id", "NomeTransporte");
            
            var manu = await ObterManutencaos();
            ViewBag.Manutencoes = new SelectList(manu, "Id", "TipoManutencao");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransporteModel transporte)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    int funcionarioId = transporte.FuncionarioId;
                    FuncionariosModel funcionario = await ObterFuncionarioPorId(funcionarioId);
                    int matTranspId = transporte.MatriculaTransporteId;
                    MatriculaTransporteModel matTransp = await ObterMatriculaTransportePorId(matTranspId);
                    int tpTranspId = transporte.TipoTransporteId;
                    TipoDeTransporteModel tpTransp = await ObterTipoTransportePorId(tpTranspId);
                    int? manuId = transporte.ManutencaoId;
                    ManutencaoModel? manu = null;

                    // Se manuId não for null, chamamos ObterManutencaoPorId
                    if (manuId != null)
                    {
                        manu = await ObterManutencaoPorId(manuId.Value);
                    }


                    if (funcionario != null && matTransp != null && tpTransp != null)
                    {

                        transporte.Funcionario = funcionario;
                        transporte.MatriculaTransporte = matTransp;
                        transporte.TipoTransportes = tpTransp;
                        transporte.Manutencao = manu;
                        transporte.DataInicio.ToShortDateString();
                        transporte.DataFim.GetValueOrDefault().ToShortDateString();

                        var retorno = await _service.AddAsync(transporte);
                        TempData["MensagemSucesso"] = "Transporte cadastrado com sucesso!";
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        return NotFound("Funcionario, MatriculaTransporte e/ou TipoDeTransporte não encontrada");
                    }


                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrado o Transporte!";
                    return RedirectToAction("Index");
                }
            }
            return View(transporte);
        }




        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                var func = await ObterFuncionarios();
                ViewBag.Funcionarios = new SelectList(func, "Id", "NomeFuncionario");

                var mat = await ObterMatriculasTransporte();
                ViewBag.Matriculas = new SelectList(mat, "Id", "Matricula");

                var tp = await ObterTiposTransporte();
                ViewBag.TiposTransporte = new SelectList(tp, "Id", "NomeTransporte");

                var manu = await ObterManutencaos();
                ViewBag.Manutencoes = new SelectList(manu, "Id", "TipoManutencao");

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
        public async Task<IActionResult> Edit(int id, TransporteModel transporte)
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
                    TempData["MensagemSucesso"] = "Transporte atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Transporte!";
                    return RedirectToAction("Index");
                }
            }
            return View(transporte);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool transporte = await _service.DeleteAsync(id);
                if (transporte)
                {
                    TempData["MensagemSucesso"] = "Transporte excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Transporte!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Transporte!";
                return RedirectToAction("Index");
            }
        }


        private async Task<List<TipoDeTransporteModel>> ObterTiposTransporte()
        {
            var tps = new List<TipoDeTransporteModel>();


            var tp = await _tpService.GetAllAsync();


            if (tps != null)
            {

                tp.AddRange(tps);
            }

            return tp;
        }
        private async Task<TipoDeTransporteModel> ObterTipoTransportePorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var tp = await _tpService.GetByIdAsync(id);

            return tp;
        }
        private async Task<List<FuncionariosModel>> ObterFuncionarios()
        {
            var funcs = new List<FuncionariosModel>();


            var func = await _funcService.GetAllAsync();


            if (funcs != null)
            {

                func.AddRange(funcs);
            }

            return func;
        }
        private async Task<FuncionariosModel> ObterFuncionarioPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var func = await _funcService.GetByIdAsync(id);

            return func;
        }
        private async Task<List<MatriculaTransporteModel>> ObterMatriculasTransporte()
        {
            var mats = new List<MatriculaTransporteModel>();


            var mat = await _matService.GetAllAsync();


            if (mats != null)
            {

                mat.AddRange(mats);
            }

            return mat;
        }
        private async Task<MatriculaTransporteModel> ObterMatriculaTransportePorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var mat = await _matService.GetByIdAsync(id);

            return mat;
        }
        private async Task<List<ManutencaoModel>> ObterManutencaos()
        {
            var manus = new List<ManutencaoModel>();


            var manu = await _manuService.GetAllAsync();


            if (manus != null)
            {

                manu.AddRange(manus);
            }

            return manu;
        }
        private async Task<ManutencaoModel> ObterManutencaoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var manu = await _manuService.GetByIdAsync(id);

            return manu;
        }
    }
}

