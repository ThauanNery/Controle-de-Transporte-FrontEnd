using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Rotas;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class MatriculaTransporteRepository : IMatriculaTransporteRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MatriculaTransporteRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<MatriculaTransporteModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.MatriculaTransporteGetById.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MatriculaTransporteModel>();
        }

        public async Task<List<MatriculaTransporteModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{RouteConst.MatriculaTransporteGetAll}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<MatriculaTransporteModel>>();
        }

        public async Task<MatriculaTransporteModel> CreateAsync(MatriculaTransporteModel matricula)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{RouteConst.MatriculaTransporteCreate}", matricula);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MatriculaTransporteModel>();
        }

        public async Task<MatriculaTransporteModel> UpdateAsync(MatriculaTransporteModel matricula)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{RouteConst.MatriculaTransporteUpdate.Replace("{id}", matricula.Id.ToString())}", matricula);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<MatriculaTransporteModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{RouteConst.MatriculaTransporteDelete.Replace("{id}", id.ToString())}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
