using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class HomeController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}