using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class TipodeTransporteRepository : ITipodeTransporteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TipodeTransporteRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<TipoDeTransporteModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.TipoDeTransporteGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TipoDeTransporteModel>();
        }

        public async Task<List<TipoDeTransporteModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.TipoDeTransporteGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<TipoDeTransporteModel>>();
        }

        public async Task<TipoDeTransporteModel> CreateAsync(TipoDeTransporteModel tpTransporte)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.TipoDeTransporteCreate}", tpTransporte);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TipoDeTransporteModel>();
        }

        public async Task<TipoDeTransporteModel> UpdateAsync(TipoDeTransporteModel tpTransporte)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.TipoDeTransporteUpdate.Replace("{id}", tpTransporte.Id.ToString())}", tpTransporte);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TipoDeTransporteModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.TipoDeTransporteDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
