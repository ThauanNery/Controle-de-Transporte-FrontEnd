using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UsuarioRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<UsuarioModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.UsuarioGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UsuarioModel>();
        }

        public async Task<List<UsuarioModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.UsuarioGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<UsuarioModel>>();
        }

        public async Task<UsuarioModel> CreateAsync(UsuarioModel usuario)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.UsuarioCreate.Replace("{funcionarioId}", usuario.Funcionarios.Id.ToString())}", usuario);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UsuarioModel>();
        }

        public async Task<UsuarioModel> UpdateAsync(UsuarioModel usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.UsuarioUpdate.Replace("{id}", usuario.Id.ToString())}", usuario);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UsuarioModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.UsuarioDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
