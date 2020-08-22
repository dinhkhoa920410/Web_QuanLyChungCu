using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class ApartmentController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Apartment);
        }
        public ActionResult ApartmentCreate()
        {
            SetViewBagLead();
            return View();
        }
        [HttpPost]
        public ActionResult ApartmentCreate(Apartment objApartment)
        {
            db.Apartment.Add(objApartment);
            db.SaveChanges();
            return RedirectToAction("Index", "Apartment");
        }
        [HttpGet]
        public ActionResult ApartmentModify(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Apartment objApartment = db.Apartment.FirstOrDefault(x => x.Id == id);
            if (objApartment == null)
            {
                return HttpNotFound();
            }
            return View(objApartment);
        }
        [HttpPost]
        public ActionResult ApartmentModify(Apartment objApartment)
        {
            db.Entry(objApartment).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Apartment");
        }
        [HttpGet]
        public ActionResult ApartmentDelete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Apartment objApartment = db.Apartment.SingleOrDefault(n => n.Id == id);
            if (objApartment == null)
            {
                return HttpNotFound();

            }
            db.Entry(objApartment).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "Apartment");
        }







        public void SetViewBagLead()
        {
            var dao = new LeadDao();
            ViewBag.LeadId = new SelectList(dao.ListAll(), "ID", "Name", "vui lòng chọn");
        }
    }
}