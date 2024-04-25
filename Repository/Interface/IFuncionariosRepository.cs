using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Repository.Interface
{
    public interface IFuncionariosRepository
    {
        Task<FuncionariosModel> GetByIdAsync(int id);
        Task<List<FuncionariosModel>> GetAllAsync();
        Task<FuncionariosModel> CreateAsync(FuncionariosModel funcionarios);
        Task<FuncionariosModel> UpdateAsync(FuncionariosModel funcionarios);
        Task<bool> DeleteByIdAsync(int id);
    }
}
