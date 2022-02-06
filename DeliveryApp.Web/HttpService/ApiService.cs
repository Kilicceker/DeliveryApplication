//using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace DeliveryApp.Web.HttpService
{
    public class ApiService<T> : IApiService<T> where T: class
    {

        public async Task<string> AddAsync(T model,string url, HttpClient httpClient)
        {
            StringContent queryString = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8,
                                    "application/json");
            var reponse = await httpClient.PostAsync(url, queryString);
            return await reponse.Content.ReadAsStringAsync();
        }

        public async Task DeleteAsync(string url, HttpClient httpClient)
        {
            await httpClient.DeleteAsync(url);
        }

        public async Task<T> GetAsync(string url,HttpClient httpClient)
        {
            var response = await httpClient.GetAsync(url);
            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }

        public async Task UpdateAsync(T model, string url, HttpClient httpClient)
        {
            StringContent queryString = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8,
                                    "application/json");
            await httpClient.PutAsync(url,queryString);
        }
    }
}
