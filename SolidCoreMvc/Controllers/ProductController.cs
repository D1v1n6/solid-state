using Microsoft.AspNetCore.Mvc;
using SolidCoreMvc.Models;
using SolidCoreMvc.Services;

namespace SolidCoreMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
