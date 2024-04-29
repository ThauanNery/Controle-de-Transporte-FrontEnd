using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> GetByIdAsync(int id);
        Task<List<UsuarioModel>> GetAllAsync();
        Task<UsuarioModel> AddAsync(UsuarioModel usuario);
        Task<UsuarioModel> UpdateAsync(UsuarioModel usuario);
        Task<bool> DeleteAsync(int id);
    }
}
