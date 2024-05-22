using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class TransporteService : ITransporteService
    {
        private readonly ITransporteRepository _repository;
        private readonly IFuncionariosRepository _funcService;
        private readonly ITipodeTransporteRepository _tpService;
        private readonly IMatriculaTransporteRepository _matService;
        private readonly IManutencaoRepository _manuService;
        public TransporteService(ITransporteRepository repository, IFuncionariosRepository funcService, ITipodeTransporteRepository tpService, IMatriculaTransporteRepository matService, IManutencaoRepository manuService)
        {
            _repository = repository;
            _funcService = funcService;
            _tpService = tpService;
            _matService = matService;
            _manuService = manuService;   
        }

        public async Task<TransporteModel> GetByIdAsync(int id)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var transporte = await _repository.GetByIdAsync(id);
                if (transporte != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return transporte;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um transporte por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<TransporteModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar transportes.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<TransporteModel> AddAsync(TransporteModel transporte)
        {
            try
            {
                int funcionarioId = transporte.FuncionarioId;
                FuncionariosModel funcionario = await ObterFuncionarioPorId(funcionarioId);
                int matTranspId = transporte.MatriculaTransporteId;
                MatriculaTransporteModel matTransp = await ObterMatriculaTransportePorId(matTranspId);
                int tpTranspId = transporte.TipoTransporteId;
                TipoDeTransporteModel tpTransp = await ObterTipoTransportePorId(tpTranspId);
                int? manuId = transporte.ManutencaoId;
                ManutencaoModel? manu = null;

                // Se manuId não for null, chamamos ObterManutencaoPorId
                if (manuId != null)
                {
                    manu = await ObterManutencaoPorId(manuId.Value);
                }
                if (funcionario != null && matTransp != null && tpTransp != null)
                {
                    transporte.Funcionario = funcionario;
                    transporte.MatriculaTransporte = matTransp;
                    transporte.TipoTransportes = tpTransp;
                    transporte.Manutencao = manu;
                    transporte.DataInicio.ToShortDateString();
                    transporte.DataFim.GetValueOrDefault().ToShortDateString();

                    await _repository.CreateAsync(transporte);
                    
                }
                return transporte;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um transporte.";
                throw new Exception(errorMessage, ex);
            }
        }
        // public async Task<TransporteModel> AddAsync(TransporteModel transporte)
        //{
        //    try
        //    {
        //        await _repository.CreateAsync(transporte);
        //        return transporte;
        //    }
        //    catch (Exception ex)
        //    {
        //        string errorMessage = "Ocorreu um erro ao adicionar um transporte.";
        //        throw new Exception(errorMessage, ex);
        //    }
        //}

        public async Task<TransporteModel> UpdateAsync(TransporteModel transporte)
        {
            try
            {
                await _repository.UpdateAsync(transporte);
                return transporte;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um transporte.";
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
                string errorMessage = "Ocorreu um erro ao apagar um transporte.";
                throw new Exception(errorMessage, ex);
            }
        }

        private async Task<List<TipoDeTransporteModel>> ObterTiposTransporte()
        {
            var tps = new List<TipoDeTransporteModel>();


            var tp = await _tpService.GetAllAsync();


            if (tps != null)
            {

                tp.AddRange(tps);
            }

            return tp;
        }
        private async Task<TipoDeTransporteModel> ObterTipoTransportePorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var tp = await _tpService.GetByIdAsync(id);

            return tp;
        }
        private async Task<List<FuncionariosModel>> ObterFuncionarios()
        {
            var funcs = new List<FuncionariosModel>();


            var func = await _funcService.GetAllAsync();


            if (funcs != null)
            {

                func.AddRange(funcs);
            }

            return func;
        }
        private async Task<FuncionariosModel> ObterFuncionarioPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var func = await _funcService.GetByIdAsync(id);

            return func;
        }
        private async Task<List<MatriculaTransporteModel>> ObterMatriculasTransporte()
        {
            var mats = new List<MatriculaTransporteModel>();


            var mat = await _matService.GetAllAsync();


            if (mats != null)
            {

                mat.AddRange(mats);
            }

            return mat;
        }
        private async Task<MatriculaTransporteModel> ObterMatriculaTransportePorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var mat = await _matService.GetByIdAsync(id);

            return mat;
        }
        private async Task<List<ManutencaoModel>> ObterManutencaos()
        {
            var manus = new List<ManutencaoModel>();


            var manu = await _manuService.GetAllAsync();


            if (manus != null)
            {

                manu.AddRange(manus);
            }

            return manu;
        }
        private async Task<ManutencaoModel> ObterManutencaoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            var manu = await _manuService.GetByIdAsync(id);

            return manu;
        }
    }
}
