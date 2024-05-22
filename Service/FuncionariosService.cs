using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class FuncionariosService : IFuncionariosService
    {
        private readonly IFuncionariosRepository _repository;
        private readonly ICargoRepository _cargoRepository;
        private readonly IDepartamentoRepository _departyamentoRepository;

        public FuncionariosService(IFuncionariosRepository repository, ICargoRepository cargoRepository, IDepartamentoRepository departyamentoRepository)
        {
            _repository = repository;
            _cargoRepository = cargoRepository;
            _departyamentoRepository = departyamentoRepository;
        }

        public async Task<FuncionariosModel> GetByIdAsync(int id)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var departamento = await _repository.GetByIdAsync(id);
                if (departamento != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return departamento;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um funcionario por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<FuncionariosModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar funcionarios.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<FuncionariosModel> AddAsync(FuncionariosModel funcionario)
        {
            try
            {
                int cargoId = funcionario.CargoId;
                CargoModel cargo = await ObterCargoPorId(cargoId);
                int departamentoId = funcionario.DepartamentoId;
                DepartamentoModel departamento = await ObterDepartamentoPorId(departamentoId);
                if (cargo != null && departamento != null)
                {

                    funcionario.Cargo = cargo;
                    funcionario.Departamento = departamento;

                    await _repository.CreateAsync(funcionario);
                }
                return funcionario;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um funcionario.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<FuncionariosModel> UpdateAsync(FuncionariosModel funcionarios)
        {
            try
            {
                await _repository.UpdateAsync(funcionarios);
                return funcionarios;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um funcionarios.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao apagar um funcionarios.";
                throw new Exception(errorMessage, ex);
            }
        }

        private async Task<CargoModel> ObterCargoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var cargo = await _cargoRepository.GetByIdAsync(id);

            return cargo;
        }
        private async Task<DepartamentoModel> ObterDepartamentoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var departamento = await _departyamentoRepository.GetByIdAsync(id);

            return departamento;
        }

    }
}
