using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Models;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public interface ICommentService
    {
        Task<Comment> GetAsync(string url);
        Task<CommentList> GetAllAsync(string url);
        Task<string> AddAsync(CommentDto Comment, string url);
        Task DeleteAsync(string url);
        Task UpdateAsync(CommentPublishDto update, string url);
    }
}
