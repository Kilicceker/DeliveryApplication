using System.Net.Http;
using System.Threading.Tasks;

namespace DeliveryApp.Web.HttpService
{
    public interface IApiService<T> where T: class
    {
        Task<T> GetAsync(string url, HttpClient httpClient);
        Task<string> AddAsync(T model, string url, HttpClient httpClient);
        Task DeleteAsync(string url, HttpClient httpClient);
        Task UpdateAsync(T model,string url, HttpClient httpClient);
    }
}
