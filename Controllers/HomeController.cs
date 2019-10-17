using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        //Main Product Page
        [HttpGet("Products")]
        public IActionResult Product()
        {
            WrapperModel viewMod = new WrapperModel();
            viewMod.AllProducts = dbContext.Products.ToList();
            return View(viewMod);
        }

        //Form submission for new product
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(WrapperModel product)
        {
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(product.NewProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Product");
            }
            else
            {
                return View("Product");
            }
        }


        //Categories page
        [HttpGet("Categories")]
        public IActionResult Category()
        {
            WrapperModel viewMod = new WrapperModel();
            viewMod.AllCategories = dbContext.Categories.ToList();
            return View(viewMod);
        }

        //Form submission for new category
        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(WrapperModel category)
        {
            if(ModelState.IsValid)
            {
                dbContext.Categories.Add(category.NewCategory);
                dbContext.SaveChanges();
                return RedirectToAction("Category");
            }
            else
            {
                return View("Category");
            }
        }

        //Adding New category to a specific product
        [HttpGet("Product/{ProductId}")]
        public IActionResult AddCategoryToProduct(int ProductId)
        {
            WrapperModel viewMod = new WrapperModel();
            viewMod.NewProduct = dbContext.Products.Include(p => p.Categories).ThenInclude(p => p.Category).FirstOrDefault(p => p.ProductId == ProductId);
            viewMod.AllCategories = dbContext.Categories.Where(c => c.Products.All(p => p.ProductId != ProductId)).ToList();
            return View(viewMod);
        }

        [HttpPost("AttachCat/{pId}")]
        public IActionResult AttachCat(int pId, int CategoryId)
        {
            if(ModelState.IsValid)
            {
                Association asc = new Association
                {
                    CategoryId = CategoryId,
                    ProductId = pId
                };
                dbContext.Add(asc);
                dbContext.SaveChanges();
                return RedirectToAction("AddCategoryToProduct", new{ProductId = pId});
            }
            else
            {
                return View("AddCategoryToProduct", pId);
            }
        }

        //Adding a new product to a specific category 
        [HttpGet("Category/{CategoryId}")]
        public IActionResult AddProductToCategory(int CategoryId)
        {
            WrapperModel viewMod = new WrapperModel();
            viewMod.NewCategory = dbContext.Categories.Include(c => c.Products).ThenInclude(c => c.Product).FirstOrDefault(c => c.CategoryId == CategoryId);
            viewMod.AllProducts = dbContext.Products.Where(p => p.Categories.All(c => c.CategoryId != CategoryId)).ToList();
            return View(viewMod);
        }
        
        [HttpPost("AttachProd/{cId}")]
        public IActionResult AttachProd(int cId, int ProductId)
        {
            if(ModelState.IsValid)
            {
                Association asc = new Association
                {
                    ProductId = ProductId,
                    CategoryId = cId
                };
                dbContext.Add(asc);
                dbContext.SaveChanges();
                return RedirectToAction("AddProductToCategory", new{CategoryId = cId});
            }
            else
            {
                return View("AddProductToCategory", cId);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
