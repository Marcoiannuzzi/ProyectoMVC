using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoMVC.Data;
using ProyectoMVC.Models;

namespace ProyectoMVC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: ProductoController
        public ActionResult Index()
        {

            var products = _context.Products;
            var productList = new List<ProductoViewModel>();

            foreach (var item in products)
            {   
                var product = new ProductoViewModel()
                {
                    Id= item.Id,
                    Name = item.Name,
                    Description= item.Description,
                    Price=item.Price,
                    Stock=item.Stock,
                };
                productList.Add(product);

            }
            return View(productList);
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            var selectProduct = _context.Products.FirstOrDefault(x=>x.Id == id);
            if (selectProduct == null) 
            {
                return View("Index");
            }
            var detailProduct = new ProductoViewModel()
            {
                Id=selectProduct.Id,
                Name=selectProduct.Name,
                Description=selectProduct.Description,
                Price=selectProduct.Price,
                Stock=selectProduct.Stock,
            };

            return View(detailProduct);
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductoViewModel newproduct)
        {
                try
                {
                    var product = new Producto()
                    {
                        Id = newproduct.Id,
                        Name = newproduct.Name,
                        Description = newproduct.Description,
                        Price = newproduct.Price,
                        Stock = newproduct.Stock,
                    };

                    if (!_context.Products.Any(x => x.Name == product.Name))
                    {
                        _context.Products.Add(product);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View();
                }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if(_context.Products.Any(x=>x.Id== id))
                {
                    var selectProduct = _context.Products.FirstOrDefault(x => x.Id == id);
                    var productUpdate = new ProductoViewModel()
                    {
                        Id = selectProduct.Id,
                        Name = selectProduct.Name,
                        Description = selectProduct.Description,
                        Price = selectProduct.Price,
                        Stock = selectProduct.Stock,
                    };

                    return View(productUpdate);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
           
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Edit(ProductoViewModel product)
        {
            try
            {
                var productUpdate = new Producto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                };
                _context.Products.Update(productUpdate);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var productforDelete = _context.Products.FirstOrDefault(x => x.Id == id);
            if (productforDelete != null)
            {
                _context.Products.Remove(productforDelete);
                await _context.SaveChangesAsync();
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
