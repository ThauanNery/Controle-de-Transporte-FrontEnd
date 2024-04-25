using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface IFuncionariosService
    {
        Task<FuncionariosModel> GetByIdAsync(int id);
        Task<List<FuncionariosModel>> GetAllAsync();
        Task<FuncionariosModel> AddAsync(FuncionariosModel funcionarios);
        Task<FuncionariosModel> UpdateAsync(FuncionariosModel funcionarios);
        Task<bool> DeleteAsync(int id);
    }
}
