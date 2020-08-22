using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class InvoiceController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Invoice);
        }
        public ActionResult InvoiceCreate(int? id)
        {

            SetViewBagStaff();
            SetViewBagLead();
            if (id != null)
            {
                var dao = new ApartmentDao();
                SelectList ApartmentList = new SelectList(dao.ListAll(), "ID", "ApartmentNumber" , id.ToString());
                ViewBag.ApartmentId = ApartmentList;
            }
            else
            {
                SetViewBagApartment();
            }
            return View();
        }
        [HttpPost]
        public ActionResult InvoiceCreate(string LeadId, string StaffId, string Code, string PayCash, string PaymentMethod, string DateCreate, string DueDate)
        {
            if (LeadId != "" || StaffId != "")
            {
                Invoice objInvoice = new Invoice();
                objInvoice.LeadId = Int32.Parse(LeadId);
                objInvoice.StaffId = StaffId;
                objInvoice.Code = Code;
                objInvoice.PayCash = PayCash;
                objInvoice.PaymentMethod = PaymentMethod;
                objInvoice.DateCreate = Convert.ToDateTime(DateCreate);
                objInvoice.DueDate = Convert.ToDateTime(DueDate);
                db.Invoice.Add(objInvoice);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Invoice");
        }
        [HttpGet]
        public ActionResult InvoiceModify(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Invoice objInvoice = db.Invoice.FirstOrDefault(x => x.Id == id);
            if (objInvoice == null)
            {
                return HttpNotFound();
            }
            return View(objInvoice);
        }
        [HttpPost]
        public ActionResult InvoiceModify(Invoice objInvoice)
        {
            db.Entry(objInvoice).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Invoice");
        }
        [HttpGet]
        public ActionResult InvoiceDelete(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Invoice objInvoice = db.Invoice.SingleOrDefault(n => n.Id == id);
            if (objInvoice == null)
            {
                return HttpNotFound();

            }
            db.Entry(objInvoice).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "Invoice");
        }



        public void SetViewBagLead()
        {
            var dao = new LeadDao();
            ViewBag.LeadId = new SelectList(dao.ListAll(), "ID", "Name", "vui lòng chọn");
        }
        public void SetViewBagStaff()
        {
            var dao = new StaffDao();
            ViewBag.StaffId = new SelectList(dao.ListAll(), "ID", "Name", "vui lòng chọn");
        }
        public void SetViewBagApartment()
        {
            var dao = new ApartmentDao();
            ViewBag.ApartmentId = new SelectList(dao.ListAll(), "Name", "vui lòng chọn");
        }
    }
}