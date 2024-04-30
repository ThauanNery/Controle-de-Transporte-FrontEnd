using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class TipodeTransporteService : ITipodeTransporteService
    {
        private readonly ITipodeTransporteRepository _repository;

        public TipodeTransporteService(ITipodeTransporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<TipoDeTransporteModel> GetByIdAsync(int id)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var cargo = await _repository.GetByIdAsync(id);
                if (cargo != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return cargo;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um Tipo de Transportel por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<TipoDeTransporteModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar Tipo de Transportel.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<TipoDeTransporteModel> AddAsync(TipoDeTransporteModel cargo)
        {
            try
            {
                await _repository.CreateAsync(cargo);
                return cargo;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um Tipo de Transportel.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<TipoDeTransporteModel> UpdateAsync(TipoDeTransporteModel cargo)
        {
            try
            {
                await _repository.UpdateAsync(cargo);
                return cargo;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um Tipo de Transportel.";
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
                string errorMessage = "Ocorreu um erro ao apagar um Tipo de Transportel.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
