using LineStickersManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LineStickersManager.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            ViewBag.ProductList = "[]";
            LineStickersManagerEntities db = new LineStickersManagerEntities();

            if (db.tblProducts.Any(m => m.ValidateDateTime >= DateTime.Now))
            {
                var _SelectItems = (from d 
                                    in db.tblProducts
                                    where d.ValidateDateTime >= DateTime.Now
                                    select new {
                                        Text = d.Name,
                                        Value = d.ID.ToString(),
                                        Description = d.Desc,
                                        Price = d.Price
                                    }).ToList();
                ViewBag.ProductList = JsonConvert.SerializeObject(_SelectItems);
            }
            return View("frmOrder");
        }
        [HttpPost]
        public ActionResult UploadFiles(UploadFilesModel uploadFilesModel) {
            ViewBag.Info = string.Empty;
            if (Request.Files != null && Request.Files.Count > 0)
            { }
            else
            {

            }

            return Json("");
        }
    }
}