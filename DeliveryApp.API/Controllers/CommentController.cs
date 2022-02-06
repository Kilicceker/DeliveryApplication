using DeliveryApp.Core.Dtos;
using DeliveryApp.Core.Services.Abstract;
using DeliveryApp.Shared.Result.ComplexTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAllComments();
            if (comments == null)
                return BadRequest(comments);
            return Ok(comments);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentDto commentDto)
        {
            var comment = await _commentService.AddAsync(commentDto);
            if (comment.ResultStatus == ResultStatus.Error)
                return BadRequest(comment);
            return Created(string.Empty,comment);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedComment = await _commentService.DeleteAsync(id);
            if (deletedComment.ResultStatus == ResultStatus.Error)
                return BadRequest(deletedComment);
            return NoContent();
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Publish(CommentPublishDto commentPublishDto)
        {
            var comment = await _commentService.PublishAsync(commentPublishDto.CommentId);
            if (comment.ResultStatus == ResultStatus.Error)
                return BadRequest(comment);
            return NoContent();
        }
    }
}
