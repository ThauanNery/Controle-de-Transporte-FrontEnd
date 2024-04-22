using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface ICargoService
    {
        Task<CargoModel> GetByIdAsync(int id);
        Task<List<CargoModel>> GetAllAsync();
        Task<CargoModel> AddAsync(CargoModel cargo);
        Task<CargoModel> UpdateAsync(CargoModel cargo);
        Task<bool> DeleteAsync(int id);
    }
}
