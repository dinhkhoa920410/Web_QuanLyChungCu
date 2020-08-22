using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web_QuanLyChungCu.Models;

namespace Web_QuanLyChungCu.Controllers
{
    public class LeadDao
    {
        QuanLyChungCuEntities db = null;
        public LeadDao()
        {
            db = new QuanLyChungCuEntities();
        }
        public List<Lead> ListAll()
        {
            return db.Lead.ToList();
        }
    }
    public class ServiceDao
    {
        QuanLyChungCuEntities db = null;
        public ServiceDao()
        {
            db = new QuanLyChungCuEntities();
        }
        public List<Service> ListAll()
        {
            return db.Service.ToList();
        }
    }
    public class StaffDao
    {
        QuanLyChungCuEntities db = null;
        public StaffDao()
        {
            db = new QuanLyChungCuEntities();
        }
        public List<Staff> ListAll()
        {
            return db.Staff.ToList();
        }
    }
    public class ApartmentDao
    {
        QuanLyChungCuEntities db = null;
        public ApartmentDao()
        {
            db = new QuanLyChungCuEntities();
        }
        public List<Apartment> ListAll()
        {
            //return db.Apartment.Where(x => x.IsActive == true).ToList();
            return db.Apartment.ToList();
        }
    }
    public class Enum
    {
        public string Nam = "Nam";
        public string Nu = "Nữ";
        public string QuanLy = "Quản Lý";
        public string NhanVien = "Nhân viên";
    }
    
}