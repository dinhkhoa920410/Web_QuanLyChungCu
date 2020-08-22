using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class LeadDetailsController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult LeadDetails(string sSearchLead)
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<Lead> objLead = db.Lead.ToList();
            if(sSearchLead != null)
            {
                objLead = db.Lead.Where(x => x.Name.Contains(sSearchLead) || x.Code.Contains(sSearchLead)).ToList();
            }
            return View(objLead);
        }
        [HttpGet]
        public ActionResult LeadCreate()
        {
            SetViewBagStaff();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult LeadCreate(Lead objLead)
        {
            db.Lead.Add(objLead);
            db.SaveChanges();
            return RedirectToAction("LeadDetails");
        }
        [HttpGet]
        public ActionResult LeadModify(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Lead objLead = db.Lead.SingleOrDefault(n => n.Id == id);
            if (objLead == null)
            {
                return HttpNotFound();
            }
            return View(objLead);

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult LeadModify(Lead objLead)
        {
            db.Entry(objLead).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("LeadDetails");
        }
        [HttpGet]
        public ActionResult LeadDelete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Lead objLead = db.Lead.SingleOrDefault(n => n.Id == id);
            if (objLead == null)
            {
                return HttpNotFound();

            }
            db.Entry(objLead).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("LeadDetails", "LeadDetails");
        }
        public void SetViewBagStaff()
        {
            var dao = new StaffDao();
            ViewBag.StaffId = new SelectList(dao.ListAll(), "ID", "Name", "vui lòng chọn");
        }
    }
}