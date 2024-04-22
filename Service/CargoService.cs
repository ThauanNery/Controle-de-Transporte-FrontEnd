using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CargoModel> GetByIdAsync(int id)
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
                string errorMessage = "Ocorreu um erro ao buscar um Cargo por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<CargoModel>> GetAllAsync()
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

        public async Task<CargoModel> AddAsync(CargoModel cargo)
        {
            try
            {
                await _repository.CreateAsync(cargo);
                return cargo;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um Cargo.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<CargoModel> UpdateAsync(CargoModel cargo)
        {
            try
            {
                await _repository.UpdateAsync(cargo);
                return cargo;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um Cargo.";
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
                string errorMessage = "Ocorreu um erro ao apagar um Cargo.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
