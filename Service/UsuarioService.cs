using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Service.Interface;
using System.Net;

namespace Controle_de_Transporte_FrontEnd.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioModel> BuscarPorLogin(string login)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var usuario = await _repository.BuscarPorLogin(login);
                if (usuario != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return usuario;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um Usuario por Login.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<UsuarioModel> GetByIdAsync(int id)
        {
            var statusHttp = HttpStatusCode.NotFound;
            try
            {
                var usuario = await _repository.GetByIdAsync(id);
                if (usuario != null)
                {
                    statusHttp = HttpStatusCode.OK;
                }
                return usuario;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar um usuario por Id.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<List<UsuarioModel>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao buscar usuario.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<UsuarioModel> AddAsync(UsuarioModel usuario)
        {
            try
            {
                await _repository.CreateAsync(usuario);
                return usuario;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao adicionar um usuario.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<UsuarioModel> UpdateAsync(UsuarioModel usuario)
        {
            try
            {
                await _repository.UpdateAsync(usuario);
                return usuario;
            }
            catch (Exception ex)
            {
                string errorMessage = "Ocorreu um erro ao atualizar um usuario.";
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
                string errorMessage = "Ocorreu um erro ao apagar um usuario.";
                throw new Exception(errorMessage, ex);
            }
        }
    }
}
