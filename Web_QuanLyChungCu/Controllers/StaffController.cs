using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class StaffController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Staff);
        }
        public ActionResult CreateStaff()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateStaff(Staff objStaff)
        {
            if (objStaff.Name != null && objStaff.StaffIdentify != null)
            {
                db.Staff.Add(objStaff);
                db.SaveChanges();
                return RedirectToAction("Index", "Staff");
            }
            ViewBag.ThongBao = "vui lòng nhập đầy đủ thông tin";
            return View();
        }
        [HttpGet]
        public ActionResult ModifyStaff(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Staff objStaff = db.Staff.FirstOrDefault(x => x.Id == id);
            if (objStaff == null)
            {
                return HttpNotFound();
            }
            return View(objStaff);
        }
        [HttpPost]
        public ActionResult ModifyStaff(Staff objStaff)
        {
            db.Entry(objStaff).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Staff");
        }
        [HttpGet]
        public ActionResult StaffDelete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Staff objStaff = db.Staff.FirstOrDefault(x => x.Id == id);
            if (objStaff == null)
            {
                return HttpNotFound();
            }
            db.Entry(objStaff).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "Staff");
        }
    }
}