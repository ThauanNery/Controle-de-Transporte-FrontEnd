using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class TransporteRepository : ITransporteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TransporteRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<TransporteModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.TransporteGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TransporteModel>();
        }

        public async Task<List<TransporteModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.TransporteGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<TransporteModel>>();
        }

        public async Task<TransporteModel> CreateAsync(TransporteModel transporte)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.TransporteCreate.Replace("{tipoTransporteId}", transporte.TipoTransportes.Id.ToString()).Replace("{funcionarioId}", transporte.Funcionario.Id.ToString()).Replace("{matriculaTransporteId}", transporte.MatriculaTransporte.Id.ToString())}", transporte);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TransporteModel>();
        }

        public async Task<TransporteModel> UpdateAsync(TransporteModel transporte)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.TransporteUpdate.Replace("{id}", transporte.Id.ToString())}", transporte);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TransporteModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.TransporteDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
