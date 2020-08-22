using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
                return View(db.UserInfo);
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
                return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(UserInfo User)
        {

            db.UserInfo.Add(User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {

                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UserInfo User = db.UserInfo.SingleOrDefault(n => n.Id == id);
                if (User == null)
                {
                    return HttpNotFound();

                }
                return View(User);

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(UserInfo User)
        {
            db.Entry(User).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UserInfo User = db.UserInfo.SingleOrDefault(n => n.Id == id);
                if (User == null)
                {
                    return HttpNotFound();

                }
                return View(User);

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Xoa(UserInfo model)
        { 
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLySanPham");
        }
    }
}