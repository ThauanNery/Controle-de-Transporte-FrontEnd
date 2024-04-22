using Controle_de_Transporte_FrontEnd.Models;
using Controle_de_Transporte_FrontEnd.Repository.Interface;

namespace Controle_de_Transporte_FrontEnd.Repository
{
    public class CargoRepository : ICargoRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CargoRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private string BaseUrl => _configuration.GetValue<string>("AppSettings:ApiBaseUrl");

        public async Task<CargoModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/Cargo/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CargoModel>();
        }

        public async Task<List<CargoModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/Cargo");
            response.EnsureSuccessStatusCode(); 

            return await response.Content.ReadFromJsonAsync<List<CargoModel>>();
        }

        public async Task<CargoModel> CreateAsync(CargoModel cargo)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/cargo", cargo);
            response.EnsureSuccessStatusCode(); 

            return await response.Content.ReadFromJsonAsync<CargoModel>();
        }

        public async Task<CargoModel> UpdateAsync(CargoModel cargo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/cargo/{cargo.Id}", cargo);
            response.EnsureSuccessStatusCode(); 

            return await response.Content.ReadFromJsonAsync<CargoModel>();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/cargo/{id}");
            response.EnsureSuccessStatusCode(); 

            return response.IsSuccessStatusCode;
        }
    }
}
