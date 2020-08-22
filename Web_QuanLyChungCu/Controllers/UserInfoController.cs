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
    public class UserInfoController : Controller
    {
        QuanLyChungCuEntities db = new QuanLyChungCuEntities();
        public ActionResult Index()
        {
            if (Session["DangNhapAdmin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.UserInfo);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            SetViewBagPosition();
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserInfo objUserInfo)
        {
            if (objUserInfo.Name != null)
            {
                objUserInfo.PassWord = CreateMD5(objUserInfo.PassWord);
                ViewBag.ThongBao = "Thêm Thành Công";
                db.UserInfo.Add(objUserInfo);
                db.SaveChanges();
                return View();
            }
            ViewBag.ThongBao = " Sai mã CaptCha ";
            return View();
        }
        [HttpGet]
        public ActionResult UserInfoModify(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            UserInfo objUserInfo = db.UserInfo.FirstOrDefault(x => x.Id == id);
            if (objUserInfo == null)
            {
                return HttpNotFound();
            }
            return View(objUserInfo);
        }
        [HttpPost]
        public ActionResult UserInfoModify(UserInfo objUserInfo)
        {
            db.Entry(objUserInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "UserInfo");
        }
        [HttpGet]
        public ActionResult UserInfoDelete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            UserInfo objUserInfo = db.UserInfo.FirstOrDefault(x => x.Id == id);
            if (objUserInfo == null)
            {
                return HttpNotFound();

            }
            db.Entry(objUserInfo).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "UserInfo");
        }
        
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public void SetViewBagPosition()
        {
            List<String> lstPosition = new List<String>()
            {
                "Nhân viên",
                "Quản Lý",
                "Bảo Vệ"
            };
            ViewBag.Position = new SelectList(lstPosition);
        }
    }
}