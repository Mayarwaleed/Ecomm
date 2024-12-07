using Ecomm.Data;
using Ecomm.Models;
using Ecomm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;
        public ProductController(ApplicationDbContext context)
        {
            products= new Repository<Product>(context);
        }
        public async Task< IActionResult >Index()
        {
            return View(await products.GetAllAsync());
        }
    }
}
