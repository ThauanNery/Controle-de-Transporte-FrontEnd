using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Repository.Interface
{
    public interface IDepartamentoRepository
    {
        Task<DepartamentoModel> GetByIdAsync(int id);
        Task<List<DepartamentoModel>> GetAllAsync();
        Task<DepartamentoModel> CreateAsync(DepartamentoModel departamento);
        Task<DepartamentoModel> UpdateAsync(DepartamentoModel departamento);
        Task<bool> DeleteByIdAsync(int id);
    }
}
