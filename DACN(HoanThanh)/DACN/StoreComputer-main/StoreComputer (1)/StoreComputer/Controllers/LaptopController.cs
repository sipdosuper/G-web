using PagedList;
using StoreComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using PagedList;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls.Expressions;
using System.Security.Cryptography;

namespace StoreComputer.Controllers
{
    public class LaptopController : Controller
    {
        Model1 db = new Model1();
        // GET: Laptop
        public ActionResult Index(int? page)
        {
            Model1 db = new Model1();
            return View();
        }
        public ActionResult DanhSachLaptop(int ?page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 6; //hiện bao nhiu tên trong 1 trang
            var listdanhSach = (from s in db.HangHoa
                                where s.maLoai == 1
                                orderby s.maHang select s).ToPagedList(pageNumber, pageSize);
            return View(listdanhSach);
        }
        public ActionResult ChiTietLaptop(int id)
        {
            Model1 db = new Model1();
            HangHoa HangHoa = db.HangHoa.Find(id);

            return View(HangHoa);
        }
        public ActionResult TimKiemLaptop(String search)
        {
            Model1 db = new Model1();
            var laptop = db.HangHoa.Where(p => p.tenHang.Contains(search)).ToList();
            return View(laptop) ;
        }
        public ActionResult DanhMucLaptop(int id)
        {
            Model1 db = new Model1();
            var danhMuc = db.HangHoa.Where(p => p.maNCC == id).ToList();
            return View(danhMuc);
        }
        public ActionResult giamDan(int ?page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 6; //hiện bao nhiu tên trong 1 trang
            var laptop = (from s in db.HangHoa where s.LoaiHang.tenLoai == "Laptop" orderby s.giaMoi descending select s).ToPagedList(pageNumber,pageSize);
            return View(laptop);
        }
        public ActionResult tangDan(int ?page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 6; //hiện bao nhiu tên trong 1 trang
            var laptop = (from s in db.HangHoa where s.LoaiHang.tenLoai == "Laptop" orderby s.giaMoi ascending select s).ToPagedList(pageNumber,pageSize);
            return View(laptop);
        }
    }
}