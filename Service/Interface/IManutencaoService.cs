using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface IManutencaoService
    {
        Task<ManutencaoModel> GetByIdAsync(int id);
        Task<List<ManutencaoModel>> GetAllAsync();
        Task<ManutencaoModel> AddAsync(ManutencaoModel manutencao);
        Task<ManutencaoModel> UpdateAsync(ManutencaoModel manutencao);
        Task<bool> DeleteAsync(int id);
    }
}
