using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public InstituicaoRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<InstituicaoModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.InstituicaoGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<InstituicaoModel>();
        }

        public async Task<List<InstituicaoModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.InstituicaoGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<InstituicaoModel>>();
        }

        public async Task<InstituicaoModel> CreateAsync(InstituicaoModel cargo)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.InstituicaoCreate}", cargo);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<InstituicaoModel>();
        }

        public async Task<InstituicaoModel> UpdateAsync(InstituicaoModel cargo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.InstituicaoUpdate.Replace("{id}", cargo.Id.ToString())}", cargo);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<InstituicaoModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.InstituicaoDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
