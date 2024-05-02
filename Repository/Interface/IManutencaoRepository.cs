using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Repository.Interface
{
    public interface IManutencaoRepository
    {
        Task<ManutencaoModel> GetByIdAsync(int id);
        Task<List<ManutencaoModel>> GetAllAsync();
        Task<ManutencaoModel> CreateAsync(ManutencaoModel manutencao);
        Task<ManutencaoModel> UpdateAsync(ManutencaoModel manutencao);
        Task<bool> DeleteByIdAsync(int id);
    }
}
