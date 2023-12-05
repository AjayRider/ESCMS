using ESCMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCMS.Controllers
{
    public class MainController : Controller
    {
         
        DbHelper objProduct = new DbHelper();

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<ProductModel> lstProduct = new List<ProductModel>();
            lstProduct = objProduct.GetAllProducts().ToList();

            return View(lstProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                objProduct.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            ProductModel product = objProduct.GetProductData(ProductId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int ProductId, [Bind] ProductModel productmodel)
        {
            if (ProductId!= productmodel.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objProduct.UpdateProduct(productmodel);
                return RedirectToAction("Index");
            }
            return View(productmodel);
        }

        [HttpGet]
        public IActionResult Details(int? ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }
            ProductModel productmodel = objProduct.GetProductData(ProductId);

            if (productmodel == null)
            {
                return NotFound();
            }
            return View(productmodel);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductModel productmodel= objProduct.GetProductData(id);

            if (productmodel == null)
            {
                return NotFound();
            }
            return View(productmodel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? ProductId)
        {
            objProduct.DeleteEmployee(ProductId);
            return RedirectToAction("Index");
        }
    }
}

