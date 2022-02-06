using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.HttpService;
using DeliveryApp.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Services
{
    public class CommentService:ICommentService
    {
        private readonly HttpClient _client;
        private readonly IApiService<CommentDto> _service;
        private readonly IApiService<Comment> _comments;
        private readonly IApiService<CommentList> _commentList;
        private readonly IApiService<CommentPublishDto> _update;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentService(HttpClient client, IApiService<CommentDto> service, IApiService<CommentPublishDto> update, IHttpContextAccessor httpContextAccessor, IApiService<Comment> comments, IApiService<CommentList> commentList)
        {

            _client = client;
            _service = service;
            _update = update;
            _httpContextAccessor = httpContextAccessor; var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            _comments = comments;
            _commentList = commentList;
        }

        public async Task<string> AddAsync(CommentDto Comment, string url)
        {
            return await _service.AddAsync(Comment, url, _client);
        }

        public async Task DeleteAsync(string url)
        {
            await _service.DeleteAsync(url, _client);
        }

        public async Task<CommentList> GetAllAsync(string url)
        {
            return await _commentList.GetAsync(url, _client);
        }

        public async Task<Comment> GetAsync(string url)
        {
            return await _comments.GetAsync(url, _client);
        }
        
        public async Task UpdateAsync(CommentPublishDto update, string url)
        {
            await _update.UpdateAsync(update, url, _client);
        }
    }
}
