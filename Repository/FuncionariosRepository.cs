using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class FuncionariosRepository : IFuncionariosRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FuncionariosRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<FuncionariosModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.FuncionariosGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<FuncionariosModel>();
        }

        public async Task<List<FuncionariosModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.FuncionariosGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<FuncionariosModel>>();
        }

        public async Task<FuncionariosModel> CreateAsync(FuncionariosModel funcionarios)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.FuncionariosCreate.Replace("{cargoId}", funcionarios.Cargo.Id.ToString()).Replace("{departamentoId}", funcionarios.Departamento.Id.ToString())}", funcionarios);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<FuncionariosModel>();
        }

        public async Task<FuncionariosModel> UpdateAsync(FuncionariosModel funcionarios)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.FuncionariosUpdate.Replace("{id}", funcionarios.Id.ToString())}", funcionarios);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<FuncionariosModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.FuncionariosDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
