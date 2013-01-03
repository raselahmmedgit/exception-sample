using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RnD.ExceptionSample.Helper;
using RnD.ExceptionSample.Models;
using RnD.ExceptionSample.ViewModels;
using System.Text;

namespace RnD.ExceptionSample.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Product/

        public ViewResult Index()
        {
            //var products = _db.Products.Include(p => p.Category);
            //return View(products.ToList());
            return View();
        }

        // for display datatable
        public ActionResult GetProducts(DataTableParamModel param)
        {
            var products = _db.Products.ToList();

            var viewProducts = products.Select(pro => new ProductTableModels() { ProductId = Convert.ToString(pro.ProductId), Name = pro.Name, Price = Convert.ToString(pro.Price), CategoryId = pro.Category == null ? null : Convert.ToString(pro.Category.CategoryId), CategoryName = pro.Category == null ? null : pro.Category.Name });

            IEnumerable<ProductTableModels> filteredProducts;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredProducts = viewProducts.Where(pro => (pro.Name ?? "").Contains(param.sSearch) || (pro.Price ?? "").Contains(param.sSearch) || (pro.CategoryName ?? "").Contains(param.sSearch)).ToList();
            }
            else
            {
                filteredProducts = viewProducts;
            }

            var viewOdjects = filteredProducts.Skip(param.iDisplayStart).Take(param.iDisplayLength);

            var result = from proMdl in viewOdjects
                         select new[] { proMdl.ProductId, proMdl.Name, proMdl.Price, proMdl.CategoryId, proMdl.CategoryName };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = products.Count(),
                iTotalDisplayRecords = filteredProducts.Count(),
                aaData = result
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Product/Details/By ID

        public ActionResult Details(int id)
        {
            Product product = _db.Products.Find(id);

            var model = new ProductViewModel()
                                    {
                                        ProductId = product.ProductId,
                                        Name = product.Name,
                                        Price = product.Price,
                                        CategoryId = product.CategoryId,
                                        CategoryName = product.Category.Name
                                    };

            //return View(product);
            //return PartialView("_Details", product);
            //return View("_Details", product);
            return View("_Details", model);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            var categories = _db.Categories.ToList<Category>().PopulateDropdownList("CategoryId", "Name").ToList();
            //var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name").ToList();
            //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");

            var model = new ProductViewModel()
                                         {
                                             ddlCategories = categories
                                         };

            //return View();
            //return PartialView("_Create");
            //return View("_Create");
            return View("_Create", model);
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Products.Add(product);
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    //return Content(Boolean.TrueString);
                    //return Json("Success", JsonRequestBehavior.AllowGet);
                    return Json(new { msg = "Product saved successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                }

                //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);

                //return View(product);
                //return View("_Create", product);
                //return Content("Please review your form.");
                //return Json("Success", JsonRequestBehavior.AllowGet);

                return Json(new { msg = ExceptionHelper.ModelStateErrorFormat(ModelState), status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex, log: true), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/Edit/By ID

        public ActionResult Edit(int id)
        {
            Product product = _db.Products.Find(id);

            var categories = _db.Categories.ToList<Category>().PopulateDropdownList("CategoryId", "Name", isEdit: true).ToList();
            //var categories = SelectListItemExtension.PopulateDropdownList(_db.Categories.ToList<Category>(), "CategoryId", "Name", isEdit: true).ToList();
            //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);

            var model = new ProductViewModel()
                                {
                                    ProductId = product.ProductId,
                                    Name = product.Name,
                                    Price = product.Price,
                                    CategoryId = product.CategoryId,
                                    ddlCategories = categories
                                };

            //return View(product);
            //return PartialView("_Edit", product);
            //return View("_Edit", product);
            return View("_Edit", model);
        }

        //
        // POST: /Product/Edit/By ID

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _db.Products.Find(product.ProductId);

                    if (model != null)
                    {
                        model.Name = product.Name;
                        model.Price = product.Price;
                        model.CategoryId = product.CategoryId;

                        _db.Entry(model).State = EntityState.Modified;
                        _db.SaveChanges();

                        //return RedirectToAction("Index");
                        //return Content(Boolean.TrueString);
                        //return Json("Success", JsonRequestBehavior.AllowGet);
                        return Json(new { msg = "Product updated successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { msg = "Product is null.", status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);

                }

                //ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name", product.CategoryId);

                //return View(product);
                //return View("_Edit", product);
                //return Content("Please review your form.");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ModelStateErrorFormat(ModelState), status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/Delete/By ID

        public ActionResult Delete(int id)
        {
            Product product = _db.Products.Find(id);

            var model = new ProductViewModel()
                                    {
                                        ProductId = product.ProductId,
                                        Name = product.Name,
                                        Price = product.Price,
                                        CategoryId = product.CategoryId
                                    };

            //return View(product);
            //return PartialView("_Delete", product);
            //return View("_Delete", product);
            return View("_Delete", model);
        }

        //
        // POST: /Product/Delete/By ID

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = _db.Products.Find(id);
                if (product != null)
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();

                    //return RedirectToAction("Index");
                    //return Content(Boolean.TrueString);
                    //return Json("Success", JsonRequestBehavior.AllowGet);
                    return Json(new { msg = "Product deleted successfully.", status = MessageType.success.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { msg = "Product is null.", status = MessageType.info.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Content("Error Occured!");
                //return Json("Success", JsonRequestBehavior.AllowGet);
                return Json(new { msg = ExceptionHelper.ExceptionMessageFormat(ex), status = MessageType.error.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult GetCategory(string proId)
        {
            int productId = Convert.ToInt32(proId);

            var product = _db.Products.Where(x => x.CategoryId == productId).FirstOrDefault();

            var category = product.Category;

            CategoryTableModels categoryTableModel = category == null ? null : new CategoryTableModels
            {
                Name = category.Name,
            };


            return PartialView("_Category", categoryTableModel);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
