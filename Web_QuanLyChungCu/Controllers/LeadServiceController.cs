using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Controllers;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class LeadServiceController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            SetViewBagService();
            SetViewBagLead();
            return View(db.LeadService);
        }
        [HttpGet]
        public ActionResult CreateLeadService()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLeadService(LeadService objLeadService)
        {
            if (objLeadService.LeadId != null && objLeadService.ServiceId != null)
            {
                db.LeadService.Add(objLeadService);
                db.SaveChanges();
                return View();
            }
            ViewBag.ThongBao = "Vui lòng chọn khách hàng, dịch vụ";
            return View();
        }
        [HttpGet]
        public ActionResult ModifyLeadService(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            LeadService objLeadService = db.LeadService.FirstOrDefault(x => x.Id == id);
            if (objLeadService == null)
            {
                return HttpNotFound();
            }
            return View(objLeadService);
        }
        [HttpPost]
        public ActionResult ModifyLeadService(LeadService objLeadService)
        {
            db.Entry(objLeadService).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "LeadService");
        }
        [HttpGet]
        public ActionResult LeadServiceDelete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            LeadService objLeadService = db.LeadService.FirstOrDefault(x => x.Id == id);
            if (objLeadService == null)
            {
                return HttpNotFound();
            }
            db.Entry(objLeadService).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "LeadService");
        }
        public void SetViewBagLead()
        {
            var dao = new LeadDao();
            ViewBag.LeadId = new SelectList(dao.ListAll(), "ID", "Name", "vui lòng chọn");
        }
        public void SetViewBagService()
        {
            var dao = new ServiceDao();
            ViewBag.ServiceId = new SelectList(dao.ListAll(), "ID", "Name", "vui lòng chọn");
        }
    }
}