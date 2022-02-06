using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = _httpContextAccessor.HttpContext.Request
.Cookies["DeliveryApp"];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var roles = jwtSecurityToken.Claims.First(claim=>claim.Type== "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (!roles.Value.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var comments = await _commentService.GetAllAsync("https://localhost:44369/api/Comment");
            return View(comments);
        }
        [HttpPost]
        public async Task<IActionResult> PublishComment(CommentPublishDto commentPublishDto)
        {
            await _commentService.UpdateAsync(commentPublishDto, "https://localhost:44369/api/Comment");
            return RedirectToAction("Index", "Comment");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteAsync($"https://localhost:44369/api/Comment?id={commentId}");
            return RedirectToAction("Index", "Comment");
        }
    }
}
