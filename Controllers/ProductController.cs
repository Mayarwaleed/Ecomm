using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using TequliasRestaurant.Models;

namespace Ecomm.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _products;

        public ProductController(ApplicationDbContext context)
        {
            _products = new Repository<Product>(context); // Initialize repository
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _products.GetAllAsync(); // Get all products from the database
            return View(products); // Pass the products to the view
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _products.GetByIdAsync(id, new QueryOptions<Product>());

            if (product == null) // Check if the product exists
            {
                return NotFound(); // Return a 404 if not found
            }

            return View(product); // Return the product details view
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View(); // Return the view for creating a new product
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Price,Stock")] Product product)
        {
            if (ModelState.IsValid) // Validate the input
            {
                await _products.AddAsync(product); // Add the product to the repository
                return RedirectToAction(nameof(Index)); // Redirect to the list of products
            }

            return View(product); // Return to the create view with the model in case of an error
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _products.GetByIdAsync(id, new QueryOptions<Product>());

            if (product == null) // Check if the product exists
            {
                return NotFound(); // Return a 404 if not found
            }

            return View(product); // Return the edit view with the product details
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Price,Stock")] Product product)
        {
            if (id != product.ProductId) // Check if the ID matches
            {
                return NotFound(); // Return a 404 if IDs do not match
            }

            if (ModelState.IsValid) // Validate the input
            {
                try
                {
                    await _products.UpdateAsync(product); // Update the product in the repository
                }
                catch
                {
                    // Handle any errors (e.g., concurrency issues)
                    return NotFound(); // Return 404 in case of failure
                }

                return RedirectToAction(nameof(Index)); // Redirect to the list of products
            }

            return View(product); // Return to the edit view if model validation fails
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _products.GetByIdAsync(id, new QueryOptions<Product>());

            if (product == null) // Check if the product exists
            {
                return NotFound(); // Return a 404 if not found
            }

            return View(product); // Return the delete confirmation view
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _products.DeleteAsync(id); // Delete the product from the repository
            return RedirectToAction(nameof(Index)); // Redirect to the list of products
        }
    }
}
