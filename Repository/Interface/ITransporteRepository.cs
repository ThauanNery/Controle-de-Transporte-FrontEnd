using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Repository.Interface
{
    public interface ITransporteRepository
    {
        Task<TransporteModel> GetByIdAsync(int id);
        Task<List<TransporteModel>> GetAllAsync();
        Task<TransporteModel> CreateAsync(TransporteModel transporte);
        Task<TransporteModel> UpdateAsync(TransporteModel transporte);
        Task<bool> DeleteByIdAsync(int id);
    }
}
