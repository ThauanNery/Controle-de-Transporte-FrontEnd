using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoService _service;
        private readonly IInstituicaoService _instService;
        public DepartamentoController(IDepartamentoService service, IInstituicaoService instService)
        {
            _service = service;
            _instService = instService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var departamento = await _service.GetAllAsync();
                return View(departamento);
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
                var departamento = await _service.GetByIdAsync(id);
                if (departamento == null)
                {
                    return NotFound();
                }
                return View(departamento);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        public async Task<IActionResult> Create()
        {
            var instituicoes = await ObterInstituicao();
            ViewBag.Instituicoes = new SelectList(instituicoes, "Id", "NomeInstituicao"); 

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartamentoModel departamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    int instituicaoId = departamento.InstituicaoId;                   
                    InstituicaoModel instituicao = await ObterInstituicaoPorId(instituicaoId);
                  
                    if (instituicao != null)
                    {
                        
                        departamento.Instituicao = instituicao;
                        
                        var retorno = await _service.AddAsync(departamento);
                        TempData["MensagemSucesso"] = "Departamento cadastrado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Retorne uma resposta adequada se a instituição não for encontrada
                        return NotFound("Instituição não encontrada");
                    }
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar o Departamento!";
                    return RedirectToAction("Index");
                }
            }
            return View(departamento);
        }




        public async Task<IActionResult> Edit(int id)
        {
           
            try
            {
                var instituicoes = await ObterInstituicao();
                ViewBag.Instituicoes = new SelectList(instituicoes, "Id", "NomeInstituicao");

                var departamento = await _service.GetByIdAsync(id);
                if (departamento == null)
                {
                    return NotFound();
                }
                return View(departamento);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartamentoModel departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(departamento);
                    TempData["MensagemSucesso"] = "Departamento atualizar com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar o Departamento!";
                    return RedirectToAction("Index");
                }
            }
            return View(departamento);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
              bool departamento = await _service.DeleteAsync(id);
                if (departamento)
                {
                    TempData["MensagemSucesso"] = "Departamento excluir com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao excluir o Departamento!";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao excluir o Departamento!";
                return RedirectToAction(nameof(Index));
            }
        }


        private async Task<List<InstituicaoModel>> ObterInstituicao()
        {
            var instituicao = new List<InstituicaoModel>();

         
            var instituicoes = await _instService.GetAllAsync();

           
            if (instituicoes != null)
            {

                instituicao.AddRange(instituicoes);
            }

            return instituicao;
        }
        private async Task<InstituicaoModel> ObterInstituicaoPorId(int id)
        {
            // Verifica se o ID é válido
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            // Busca a instituição pelo ID
            var instituicao = await _instService.GetByIdAsync(id);

            // Retorna a instituição encontrada ou null se não encontrada
            return instituicao;
        }

    }
}
