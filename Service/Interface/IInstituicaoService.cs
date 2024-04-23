using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface IInstituicaoService
    {
        Task<InstituicaoModel> GetByIdAsync(int id);
        Task<List<InstituicaoModel>> GetAllAsync();
        Task<InstituicaoModel> AddAsync(InstituicaoModel instituicao);
        Task<InstituicaoModel> UpdateAsync(InstituicaoModel instituicao);
        Task<bool> DeleteAsync(int id);
    }
}
