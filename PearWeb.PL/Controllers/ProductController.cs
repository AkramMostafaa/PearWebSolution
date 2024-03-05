using AutoMapper;
using Pear.BLL;
using Pear.BLL.Repositories;
using Pear.DAL.Data;
using PearWeb.PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PearWeb.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly GenericRepository<Products> _productRepo;
        private readonly GenericRepository<Suppliers> _supplierRepo;

        public ProductController()
        {
            _productRepo = new GenericRepository<Products>();
            _supplierRepo=new GenericRepository<Suppliers>();


        }
        // GET: Product
        public ActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var suppliers = _supplierRepo.GetAll();
            var supplierList = suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            });
            ViewBag.Suppliers = supplierList;
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductsVM VM)
        {
           
           

                var suppliers = _supplierRepo.GetAll();
                var supplierList = suppliers.Select(s => new SelectListItem
                {
                    Value = s.SupplierID.ToString(),
                    Text = s.SupplierName
                });
                ViewBag.Suppliers = supplierList;
                Products product = new Products
                {
                    
                    ProductName = VM.ProductName,
                    QuantityPerUnit = VM.QuantityPerUnit,
                    ReorderLevel = VM.ReorderLevel,
                    UnitPrice = VM.UnitPrice,
                    UnitsInStock = VM.UnitsInStock,
                    UnitsOnOrder = VM.UnitsOnOrder,
                };
                _productRepo.Add(product);

                //_unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            
            
            return View(VM);

        }
        [HttpGet]
        public ActionResult Details(int? id,string viewName="Details")
        {
            if (id == null)
                return new  HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suppliers = _supplierRepo.GetAll();
            var supplierList = suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierID.ToString(),
                Text = s.SupplierName
            });
            ViewBag.Suppliers = supplierList;

            var product = _productRepo.Get(id.Value);
            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var productVM = new ProductsVM
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                SupplierID = product.SupplierID,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };

            return View(viewName, productVM);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductsVM VM)
        {   
            if (id != VM.ProductID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Products product = new Products
                {
                    ProductID=VM.ProductID,
                    ProductName = VM.ProductName,
                    QuantityPerUnit = VM.QuantityPerUnit,
                    ReorderLevel = VM.ReorderLevel,
                    UnitPrice = VM.UnitPrice,
                    UnitsInStock = VM.UnitsInStock,
                    UnitsOnOrder = VM.UnitsOnOrder,
                };
                _productRepo.Update(product);
                //_unitOfWork.Complete();
            }
            catch (Exception ex )
            {

                ModelState.AddModelError(string.Empty, ex.Message); 
            }
            return View(VM);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public   ActionResult Delete(int id, Products model)
        {
            var product = _productRepo.Get(id);
            if (product == null)
            {
                return HttpNotFound();  
            }
            if (id != model.ProductID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                _productRepo.Delete(product);
                //var result =await _unitOfWork.Complete();
                //if (result >= 1) { Console.WriteLine("hello"); }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);


            }
            return View(model);
        }
    }
}