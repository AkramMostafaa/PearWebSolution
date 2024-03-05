using Pear.BLL;
using Pear.BLL.Repositories;
using Pear.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PearWeb.PL.Controllers
{
    public class SupplierController : Controller
    {
        private readonly GenericRepository<Suppliers> _supplierRepo;
        //private readonly UnitOfWork _unitOfWork;
        public SupplierController()
        {
             _supplierRepo = new GenericRepository<Suppliers>();
        }
        // GET: Supplier
        public ActionResult Index()
        {
            var suppliers = _supplierRepo.GetAll();
            return View(suppliers);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Suppliers model)
        {
            if (ModelState.IsValid)
            {
                _supplierRepo.Add(model);
                //_unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id, string viewName = "Details")
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var supplier = _supplierRepo.Get(id.Value);
            if (supplier == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View(viewName, supplier);
        }
        [HttpGet]
        public ActionResult Edit(int id) 
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        public ActionResult Edit(int id,Suppliers model)
        {
            if(id!=model.SupplierID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                _supplierRepo.Update(model);
                //_unitOfWork.Complete();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public  ActionResult Delete(int id,Suppliers model)
        {
            if (id != model.SupplierID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                _supplierRepo.Delete(model);
                //_unitOfWork.Complete();
            }
            catch (Exception ex )
            {
                ModelState.AddModelError(string.Empty, ex.Message);


            }
            return View(model);
        }
    }
}