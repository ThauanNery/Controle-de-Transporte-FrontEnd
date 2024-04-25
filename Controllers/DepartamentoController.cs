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
                    // Supondo que você tenha o ID da instituição disponível no objeto departamento
                    int instituicaoId = departamento.InstituicaoId;

                    // Obtenha a instituição pelo ID
                    InstituicaoModel instituicao = await ObterInstituicaoPorId(instituicaoId);

                    // Verifique se a instituição foi encontrada
                    if (instituicao != null)
                    {
                        // Atribua o ID da instituição ao departamento
                        departamento.Instituicao = instituicao;
                        //departamento.InstituicaoId = instituicao.Id;

                        // Adicione o departamento com a instituição correta
                        var retorno = await _service.AddAsync(departamento);
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
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
                }
            }
            return View(departamento);
        }




        public async Task<IActionResult> Edit(int id)
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Erro = ex.Message });
                }
            }
            return View(departamento);
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
