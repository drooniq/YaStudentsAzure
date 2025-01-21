using System.Net.Http.Json;
using YaStudentFrontend.Models;

namespace YaStudentFrontend.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var response = await _httpClient.GetAsync("api/Student"); // Makes a request to https://yastudents-api-app.ashyglacier-4533aebf.swedencentral.azurecontainerapps.io/api/Student
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Student>>();
            }

            return new List<Student>(); // Or handle error here
        }
    }

}
