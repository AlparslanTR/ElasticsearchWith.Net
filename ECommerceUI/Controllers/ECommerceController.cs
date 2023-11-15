using ECommerceUI.Dtos;
using ECommerceUI.Services.ECommerceService;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceUI.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly ECommerceService _service;

        public ECommerceController(ECommerceService service)
        {
            _service = service;
        }

        public async Task <IActionResult> Search([FromQuery] SearchPage searchPage)
        {
            var (list,totalCount,pageLinkCount) = await _service.Search(searchPage.Search, searchPage.Page, searchPage.PageSize);

            searchPage.List = list;
            searchPage.TotalCount = totalCount;
            searchPage.PageLinkCount = pageLinkCount;

            return View(searchPage);
        }
    }
}
