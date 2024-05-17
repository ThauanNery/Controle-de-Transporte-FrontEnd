using Controle_de_Transporte_FrontEnd.Models;

namespace Controle_de_Transporte_FrontEnd.Service.Interface
{
    public interface IMatriculaTransporteService
    {
        Task<MatriculaTransporteModel> GetByIdAsync(int id);
        Task<List<MatriculaTransporteModel>> GetAllAsync();
        Task<MatriculaTransporteModel> AddAsync(MatriculaTransporteModel matricula);
        Task<MatriculaTransporteModel> UpdateAsync(MatriculaTransporteModel matricula);
        Task<bool> DeleteAsync(int id);
    }
}
