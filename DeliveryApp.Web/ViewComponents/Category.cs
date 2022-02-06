using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryApp.Web.Services;

namespace DeliveryApp.Web.ViewComponents
{
    public class Category: ViewComponent
    {
        private readonly ICategoryService _category;

        public Category(ICategoryService category)
        {
            _category = category;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

           var categories =await _category.GetAsync("https://localhost:44369/api/Types");

               return View(categories);
        }


    }
}
