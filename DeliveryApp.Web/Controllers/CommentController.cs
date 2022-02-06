using DeliveryApp.Core.Dtos;
using DeliveryApp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _comment;

        public CommentController(ICommentService comment)
        {
            _comment = comment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentDto commentDto)
        {
            var response = await _comment.AddAsync(commentDto, "https://localhost:44369/api/Comment");
            return RedirectToAction("Index", "Home");
        }
    }
}
