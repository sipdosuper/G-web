using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreComputer.Models;

using PagedList;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace StoreComputer.Controllers
{
    public class LinhKienController : Controller
    {
        Model1 db = new Model1();
        // GET: LinhKien
        public ActionResult DanhSachLinhKien(int ?page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 6; //hiện bao nhiu tên trong 1 trang
            var danhSach = (from s in db.HangHoa
                            where s.maLoai != 1
                            orderby s.maHang
                            select s).ToPagedList(pageNumber, pageSize);

            return View(danhSach);
        }
        public ActionResult ChiTietLinhKien(int id)
        {
            Model1 db = new Model1();
            HangHoa HangHoa = db.HangHoa.Find(id);

            return View(HangHoa);
        }
        public ActionResult TimKiemLinhKien(String search)
        {
            Model1 db = new Model1();
            var linhkien = db.HangHoa.Where(p => p.tenHang.Contains(search)).ToList();
            return View(linhkien);
        }
        public ActionResult DanhMucLinhKien(int id)
        {
            Model1 db = new Model1();
            var danhMuc = db.HangHoa.Where(p => p.maLoai == id).ToList();
            return View(danhMuc);
        }
        public ActionResult TangDan(int ?page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 6; //hiện bao nhiu tên trong 1 trang
            var tangdan = (from s in db.HangHoa
                           where s.maLoai != 1
                           orderby s.giaMoi ascending
                           select s).ToPagedList(pageNumber,pageSize);
            return View(tangdan);
        }
        public ActionResult GiamDan(int ?page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 6; //hiện bao nhiu tên trong 1 trang
            var giamdan = (from s in db.HangHoa
                           where s.maLoai != 1
                           orderby s.giaMoi descending
                           select s).ToPagedList(pageNumber,pageSize);
            return View(giamdan);
        }
    }
}