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
                                    select new {
                                        Text = d.Name,
                                        Value = d.ID.ToString(),
                                        Description = d.Desc
                                    }).ToList();
                ViewBag.ProductList = JsonConvert.SerializeObject(_SelectItems);
            }
            return View("frmOrder");
        }
    }
}