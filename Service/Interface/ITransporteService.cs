using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface ITransporteService
    {
        Task<TransporteModel> GetByIdAsync(int id);
        Task<List<TransporteModel>> GetAllAsync();
        Task<TransporteModel> AddAsync(TransporteModel transporte);
        Task<TransporteModel> UpdateAsync(TransporteModel transporte);
        Task<bool> DeleteAsync(int id);
    }
}
