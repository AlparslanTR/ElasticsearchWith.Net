using BlogUI.Dtos;
using BlogUI.Models;
using BlogUI.Services.BlogService;
using Microsoft.AspNetCore.Mvc;

namespace BlogUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _service;

        public BlogController(BlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(AddBlogDto request)
        {
           await _service.Save(request);
           return RedirectToAction(nameof(BlogController.Search));
        }

        [HttpGet]
        public async Task <IActionResult> Search()
        {
            return View(await _service.Search(string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            return View(await _service.Search(searchText));
        }
    }
}
