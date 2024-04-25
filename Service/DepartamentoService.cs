using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _repository;
        private readonly IInstituicaoRepository _instituicaorepository;

        public DepartamentoService(IDepartamentoRepository repository, IInstituicaoRepository instituicaorepository)
        {
            _repository = repository;
            _instituicaorepository = instituicaorepository;
        }

        public async Task<DepartamentoModel> GetByIdAsync(int id)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var departamento = await _repository.GetByIdAsync(id);
                if (departamento != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return departamento;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um departamento por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<DepartamentoModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar Cargos.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<DepartamentoModel> AddAsync(DepartamentoModel departamento)
        {
            try
            {
                await _repository.CreateAsync(departamento);
                return departamento;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um departamento.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<DepartamentoModel> UpdateAsync(DepartamentoModel departamento)
        {
            try
            {
                await _repository.UpdateAsync(departamento);
                return departamento;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um departamento.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao apagar um departamento.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
