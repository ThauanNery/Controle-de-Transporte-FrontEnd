using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DepartamentoRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<DepartamentoModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.DepartamentoGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DepartamentoModel>();
        }

        public async Task<List<DepartamentoModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.DepartamentoGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<DepartamentoModel>>();
        }

        public async Task<DepartamentoModel> CreateAsync(DepartamentoModel departamento)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.DepartamentoCreate.Replace("{institucaoId}", departamento.Instituicao.Id.ToString())}", departamento);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DepartamentoModel>();
        }

        public async Task<DepartamentoModel> UpdateAsync(DepartamentoModel departamento)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.DepartamentoUpdate.Replace("{id}", departamento.Id.ToString())}", departamento);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DepartamentoModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.DepartamentoDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
