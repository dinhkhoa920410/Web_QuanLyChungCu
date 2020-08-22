using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class LoginController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string sTaiKhoan = f["txtTenDangNhap"].ToString().Trim();
            string sMatKhau = f["txtMatKhau"].ToString().Trim();
            sMatKhau = UserInfoController.CreateMD5(sMatKhau);

            if(db.UserInfo.FirstOrDefault(x => x.UserName == sTaiKhoan && x.PassWord == sMatKhau) != null)
            {
                Session["DangNhapAdmin"] = sTaiKhoan;
                ViewBag.ThongBao = "Đăng nhập Thành Công";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["TaiKhoan1"] = "FAIL LOGIN !!";
            }

            return RedirectToAction("Index","Login");
        }
        public ActionResult Logout()
        {
            Session["DangNhapAdmin"] = null;
            return RedirectToAction("Index","Login");
        }      
    }
}