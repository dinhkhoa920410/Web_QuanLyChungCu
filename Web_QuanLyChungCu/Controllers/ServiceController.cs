using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class ServiceController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Service);
        }
        public ActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateService(Service objService)
        {
            if (objService.Name != null && objService.Code != null)
            {
                db.Service.Add(objService);
                db.SaveChanges();
                return View();
            }
            ViewBag.ThongBao = "vui lòng nhập đầy đủ thông tin";
            return View();
        }
        [HttpGet]
        public ActionResult ModifyService(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Service objService = db.Service.FirstOrDefault(x => x.Id == id);
            if (objService == null)
            {
                return HttpNotFound();
            }
            return View(objService);
        }
        [HttpPost]
        public ActionResult ServiceModify(Service objService)
        {
            db.Entry(objService).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Service");
        }
        [HttpGet]
        public ActionResult ServiceDelete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Service objService = db.Service.FirstOrDefault(x => x.Id == id);
            if (objService == null)
            {
                return HttpNotFound();
            }
            db.Entry(objService).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "Service");
        }
    }
}