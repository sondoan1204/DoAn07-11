using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn07_11.Models;

namespace DoAn07_11.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {           
            var ltinhthanh = data.TINHTHANHs.OrderBy(l => l.TEN);
            ViewBag.LTinhThanh = ltinhthanh;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadQuanHuyen(string typeid)
        {
            if (string.IsNullOrEmpty(typeid))
                return Json(HttpNotFound());
            var categoryList = GetQuanHuyenList(Convert.ToInt32(typeid));
            var categoryData = categoryList.Select(m => new SelectListItem()
            {
                Text = m.TEN,
                Value = m.ID_QUANHUYEN.ToString()
            });
            return Json(categoryData, JsonRequestBehavior.AllowGet);
        }
        private IList<QUANHUYEN> GetQuanHuyenList(int typeid)
        {
            return data.QUANHUYENs.OrderBy(c => c.TEN).Where(c => c.ID_TINHTHANH == typeid).ToList();
        }

        public ActionResult GetGiaDat(string Id)
        {
            int id = Convert.ToInt32(Id);
            var lgiadat = data.BANGGIADATs.OrderBy(p => p.TENDUONG).Where(p => p.ID_QUANHUYEN == id);
            return PartialView(lgiadat);
        }
        public ActionResult SearchTenduong(string searchString)
        {
            var tenduong = from t in data.BANGGIADATs
                         select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tenduong = tenduong.Where(s => s.TENDUONG.Contains(searchString));
            }
            return PartialView(tenduong);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}