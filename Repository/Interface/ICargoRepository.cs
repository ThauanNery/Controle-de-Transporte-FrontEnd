using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Repository.Interface
{
    public interface ICargoRepository
    {
        Task<CargoModel> GetByIdAsync(int id);
        Task<List<CargoModel>> GetAllAsync();
        Task<CargoModel> CreateAsync(CargoModel cargo);
        Task<CargoModel> UpdateAsync(CargoModel cargo);
        Task<bool> DeleteByIdAsync(int id);
    }
}
