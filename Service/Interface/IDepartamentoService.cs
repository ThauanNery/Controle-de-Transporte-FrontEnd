using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface IDepartamentoService
    {
        Task<DepartamentoModel> GetByIdAsync(int id);
        Task<List<DepartamentoModel>> GetAllAsync();
        Task<DepartamentoModel> AddAsync(DepartamentoModel departamento);
        Task<DepartamentoModel> UpdateAsync(DepartamentoModel departamento);
        Task<bool> DeleteAsync(int id);
    }
}
