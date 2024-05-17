using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class TransporteService : ITransporteService
    {
        private readonly ITransporteRepository _repository;

        public TransporteService(ITransporteRepository repository)
        {
            _repository = repository;
        }

        public async Task<TransporteModel> GetByIdAsync(int id)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var transporte = await _repository.GetByIdAsync(id);
                if (transporte != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return transporte;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um transporte por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<TransporteModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar transportes.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<TransporteModel> AddAsync(TransporteModel transporte)
        {
            try
            {
                await _repository.CreateAsync(transporte);
                return transporte;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um transporte.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<TransporteModel> UpdateAsync(TransporteModel transporte)
        {
            try
            {
                await _repository.UpdateAsync(transporte);
                return transporte;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um transporte.";
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
                string errorMessage = "Ocorreu um erro ao apagar um transporte.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
