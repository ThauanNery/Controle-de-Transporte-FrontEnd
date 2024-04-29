using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class FuncionariosService : IFuncionariosService
    {
        private readonly IFuncionariosRepository _repository;

        public FuncionariosService(IFuncionariosRepository repository)
        {
            _repository = repository;
        }

        public async Task<FuncionariosModel> GetByIdAsync(int id)
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
                string errorMessage = "Ocorreu um erro ao buscar um funcionario por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<FuncionariosModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar funcionarios.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<FuncionariosModel> AddAsync(FuncionariosModel funcionarios)
        {
            try
            {
                await _repository.CreateAsync(funcionarios);
                return funcionarios;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um funcionario.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<FuncionariosModel> UpdateAsync(FuncionariosModel funcionarios)
        {
            try
            {
                await _repository.UpdateAsync(funcionarios);
                return funcionarios;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um funcionarios.";
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
                string errorMessage = "Ocorreu um erro ao apagar um funcionarios.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
