using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class ManutencaoRepository : IManutencaoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ManutencaoRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<ManutencaoModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.ManutencaoGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ManutencaoModel>();
        }

        public async Task<List<ManutencaoModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.ManutencaoGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ManutencaoModel>>();
        }

        public async Task<ManutencaoModel> CreateAsync(ManutencaoModel manutencao)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.ManutencaoCreate}", manutencao);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ManutencaoModel>();
        }

        public async Task<ManutencaoModel> UpdateAsync(ManutencaoModel manutencao)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.ManutencaoUpdate.Replace("{id}", manutencao.Id.ToString())}", manutencao);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ManutencaoModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.ManutencaoDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
