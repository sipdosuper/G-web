using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreComputer.Models;

using PagedList;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Xml.Schema;
using System.Globalization;

namespace StoreComputer.Controllers
{
    public class AdminController : Controller
    {

        Model1 db = new Model1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangNhap(FormCollection collection)
        {
            Model1 db = new Model1();
            var tendn = collection["user"];
            var matkhau = collection["password"];
            TaiKhoan kh = db.TaiKhoan.SingleOrDefault(n => n.TaiKhoan1 == tendn && n.MatKhau == matkhau);
            if (kh != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        // Doanh Thu //
        public ActionResult DoanhThu(int? page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 5; //hiện bao nhiu tên trong 1 trang
            var doanhThu = db.DatHang
             .Where(p => p.ngayDatHang.Value.Month == 12 && p.ngayDatHang.Value.Year == 2023)
             .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThu = (decimal)doanhThu.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture = new CultureInfo("vi-VN");
            string format = TongDoanhThu.ToString("C0",culture);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT12 = format;

            var doanhThuT2 = db.DatHang
            .Where(p => p.ngayDatHang.Value.Month == 2 && p.ngayDatHang.Value.Year == 2023)
            .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThuT2 = (decimal)doanhThuT2.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture2 = new CultureInfo("vi-VN");
            string format2 = TongDoanhThuT2.ToString("C0", culture2);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT2 = format2;

            var doanhThuT3 = db.DatHang
           .Where(p => p.ngayDatHang.Value.Month == 3 && p.ngayDatHang.Value.Year == 2023)
           .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThuT3 = (decimal)doanhThuT3.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture3 = new CultureInfo("vi-VN");
            string format3 = TongDoanhThuT3.ToString("C0", culture3);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT3 = format3;

            var doanhThuT4 = db.DatHang
           .Where(p => p.ngayDatHang.Value.Month == 4 && p.ngayDatHang.Value.Year == 2023)
           .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThuT4 = (decimal)doanhThuT4.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture4 = new CultureInfo("vi-VN");
            string format4 = TongDoanhThuT4.ToString("C0", culture4);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT4 = format4;

            var doanhThuT5 = db.DatHang
           .Where(p => p.ngayDatHang.Value.Month == 5 && p.ngayDatHang.Value.Year == 2023)
           .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThuT5 = (decimal)doanhThuT5.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture5 = new CultureInfo("vi-VN");
            string format5 = TongDoanhThuT5.ToString("C0", culture5);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT5 = format5;

            var doanhThuT6 = db.DatHang
           .Where(p => p.ngayDatHang.Value.Month == 6 && p.ngayDatHang.Value.Year == 2023)
           .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThuT6 = (decimal)doanhThuT2.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture6 = new CultureInfo("vi-VN");
            string format6 = TongDoanhThuT6.ToString("C0", culture6);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT6 = format6;

            var doanhThuT7 = db.DatHang
           .Where(p => p.ngayDatHang.Value.Month == 7 && p.ngayDatHang.Value.Year == 2023)
           .ToList();

            // Tính tổng doanh thu
            decimal TongDoanhThuT7 = (decimal)doanhThuT7.Sum(n => n.tongTien);
            // Format tiền tệ
            CultureInfo culture7 = new CultureInfo("vi-VN");
            string format7 = TongDoanhThuT7.ToString("C0", culture7);

            // Truyền dữ liệu đến view để hiển thị
            ViewBag.TongDoanhThuT7 = format7;
            return View() ;
        }
        public ActionResult timKiemDoanhThu(int searchMonth,int searchYear)
        {
            var timkiemDT = db.DatHang
            .Where(p => p.ngayDatHang.Value.Month == searchMonth && p.ngayDatHang.Value.Year == searchYear)
            .ToList();
            decimal DT = (decimal)timkiemDT.Sum(n => n.tongTien);

            CultureInfo culture7 = new CultureInfo("vi-VN");
            string format = DT.ToString("C0", culture7);
            ViewBag.DT = format;
            ViewBag.thang = searchMonth;
            ViewBag.nam = searchYear;
            return View();

        }
        public ActionResult HangHoa(int? page)
        {

            Model1 db = new Model1();

            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 5; //hiện bao nhiu tên trong 1 trang
            var listHangHoa = db.HangHoa.OrderBy(x => x.maHang).ToPagedList(pageNumber, pageSize);

            return View(listHangHoa);
        }
        public ActionResult timKiemHangHoa(string SearchHH)
        {
            var hangHoa = db.HangHoa.Where(p => p.tenHang.Contains(SearchHH)).ToList();
            return View(hangHoa);
        }
        public ActionResult ThemHangHoa()
        {
            Model1 db = new Model1();
            var hangHoa = db.HangHoa.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemHangHoa(HangHoa model,HttpPostedFileBase fileAnh)
        {
            if(fileAnh.ContentLength > 0)
            {
                string rootFolder = Server.MapPath("/Data/");
                string pathImage = rootFolder + fileAnh.FileName;
                fileAnh.SaveAs(pathImage);
                model.hinhAnh = "Data/" + fileAnh.FileName;
            }
            Model1 db = new Model1();
                db.HangHoa.Add(model);
                db.SaveChanges();
                return RedirectToAction("HangHoa");
        }

        public ActionResult editHangHoa(int? id)
        {
            var listhangHoa = db.HangHoa.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            HangHoa hangHoa = listhangHoa.Find(p => p.maHang == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editHangHoa(HangHoa hangHoa, HttpPostedFileBase fileAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listhangHoa = db.HangHoa.ToList();
                    var updateModel = listhangHoa.Find(p => p.maHang == hangHoa.maHang);
                    updateModel.maHang = hangHoa.maHang;
                    updateModel.maLoai = hangHoa.maLoai;
                    updateModel.maNCC = hangHoa.maNCC;
                    updateModel.tenHang = hangHoa.tenHang;
                    updateModel.ngayNhap = hangHoa.ngayNhap;
                    updateModel.giaMoi = hangHoa.giaMoi;
                    updateModel.giaCu = hangHoa.giaCu;
                    updateModel.giamGia = hangHoa.giamGia;
                    updateModel.soLuong = hangHoa.soLuong;
                    string rootFolder = Server.MapPath("/Data/");
                    string pathImage = rootFolder + fileAnh.FileName;
                    fileAnh.SaveAs(pathImage);
                    hangHoa.hinhAnh = "Data/" + fileAnh.FileName;
                    updateModel.hinhAnh = hangHoa.hinhAnh;
                    updateModel.moTa = hangHoa.moTa;
                    updateModel.maTaChiTiet = hangHoa.maTaChiTiet;
                    updateModel.trangThaiSP = hangHoa.trangThaiSP;

                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Sửa dữ liệu thông tin hàng hóa không thành công");
                return View(hangHoa);
            }

            return RedirectToAction("HangHoa");
        }
        public ActionResult DetailHangHoa(int? id)
        {
            var listhangHoa = db.HangHoa.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            HangHoa hangHoa = listhangHoa.Find(p => p.maHang == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }
        public ActionResult XoaHangHoa(int? id)
        {
            var listHangHoa = db.HangHoa.ToList();
            HangHoa hangHoa = listHangHoa.Find(p => p.maHang == id);
            db.HangHoa.Remove(hangHoa);
            db.SaveChanges();
            return RedirectToAction("HangHoa");
        }
            /*-- Nha Cung Cap --*/
            public ActionResult DanhSachNhaCungCap(int? page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 5; //hiện bao nhiu tên trong 1 trang
            var nhaCungcap = db.NhaCungCap.OrderBy(x => x.maNCC).ToPagedList(pageNumber, pageSize);

            return View(nhaCungcap);
        }
        public ActionResult timKiemNCC(string SearchString)
        {
            var nhacungCap = db.NhaCungCap.Where(p => p.tenNCC.Contains(SearchString)).ToList();
            return View(nhacungCap);
        }
        public ActionResult editNCC(int? id)
        {
            var listnhaCungCap = db.NhaCungCap.ToList();
            if(id == null)
            {
                return HttpNotFound();
            }
            NhaCungCap ncc = listnhaCungCap.Find(p => p.maNCC == id);
            if(ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editNCC(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listnhaCungCap = db.NhaCungCap.ToList();
                    var updateModel = listnhaCungCap.Find(p => p.maNCC == ncc.maNCC);
                    updateModel.maNCC = ncc.maNCC;
                    updateModel.tenNCC = ncc.tenNCC;
                    updateModel.diaChi = ncc.diaChi;
                    updateModel.soDienThoai = ncc.soDienThoai;
                    db.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Sửa dữ liệu thông tin nhà cung cấp không thành công");
                return View(ncc);
            }
            
            return RedirectToAction("DanhSachNhaCungCap");
        }
        public ActionResult DetailNCC(int? id)
        {
            var listnhaCungCap = db.NhaCungCap.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            NhaCungCap ncc = listnhaCungCap.Find(p => p.maNCC == id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        public ActionResult DeleteNCC(int? id)
        {
            var listnhaCungCap = db.NhaCungCap.ToList();
            NhaCungCap ncc = listnhaCungCap.Find(p => p.maNCC == id);
            db.NhaCungCap.Remove(ncc);
            db.SaveChanges();
            return RedirectToAction("DanhSachNhaCungCap");
        }
        public ActionResult CreateNCC()
        {
            var listnhaCungCap = db.NhaCungCap.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNCC(NhaCungCap ncc)
        {
                db.NhaCungCap.Add(ncc);
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaCungCap");
        }
        /*-- Loại Hàng -- */
        public ActionResult DanhSachLoaiHang(int? page)
        {
            Model1 db = new Model1();

            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 5; //hiện bao nhiu tên trong 1 trang
            var listLoaiHang = db.LoaiHang.OrderBy(x => x.maLoai).ToPagedList(pageNumber, pageSize);
 
            return View(listLoaiHang);
        }
        public ActionResult timKiemLoaiHang(string Search)
        {
            var loaiHang = db.LoaiHang.Where(p => p.tenLoai.Contains(Search)).ToList();
            return View(loaiHang);
        }
        public ActionResult CreateLoaiHang()
        {
            var listLoaiHang = db.LoaiHang.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLoaiHang(LoaiHang lh)
        {
            var listLoaiHang = db.LoaiHang.ToList();
            if (lh.maLoai > 0)
            {
                db.LoaiHang.Add(lh);
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiHang");
            }
            else
            {
                ModelState.AddModelError("", "Thêm loại hàng không thành công , vui lòng kiểm tra lại thông tin");
                return View(lh);
            }
        }
        public ActionResult DetailLoaiHang(int? id)
        {
            var listLoaiHang = db.LoaiHang.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            LoaiHang lh = listLoaiHang.Find(p => p.maLoai == id);
            if (lh == null)
            {
                return HttpNotFound();
            }
            return View(lh) ;
        }

        public ActionResult EditLoaiHang(int? id)
        {
            var listLoaiHang = db.LoaiHang.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            LoaiHang lh = listLoaiHang.Find(p => p.maLoai == id);
            if (lh == null)
            {
                return HttpNotFound();
            }
            return View(lh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLoaiHang(LoaiHang lh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listLoaiHang = db.LoaiHang.ToList();
                    var updateModel = listLoaiHang.Find(p => p.maLoai == lh.maLoai);
                    updateModel.maLoai = lh.maLoai;
                    updateModel.tenLoai = lh.tenLoai;
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Sửa dữ liệu thông tin loại hàng không thành công");
                return View(lh);
            }

            return RedirectToAction("DanhSachLoaiHang");
        }
        public ActionResult DeleteLoaiHang(int? id)
        {
            var listLoaiHang = db.LoaiHang.ToList();
            LoaiHang lh = listLoaiHang.Find(p => p.maLoai == id);
            db.LoaiHang.Remove(lh);
            db.SaveChanges();
            return RedirectToAction("DanhSachLoaiHang");
            
        }
        /*-- Khách Hàng -- */

        public ActionResult DanhSachKhachHang(int? page)
        {
      

            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 3; //hiện bao nhiu tên trong 1 trang
            var listkhachHang = db.KhachHang.OrderBy(x => x.maKH).ToPagedList(pageNumber, pageSize);

            return View(listkhachHang);
            
        }
        public ActionResult timkiemKH(string SearchString)
        {
            var khachHang = db.KhachHang.Where(p => p.tenKH.Contains(SearchString)).ToList();
            return View(khachHang);
        }
        public ActionResult xoaKhachHang(int? id)
        {
            var kh = db.KhachHang.ToList();
            KhachHang khach = kh.Find(p => p.maKH == id);
            db.KhachHang.Remove(khach);
            db.SaveChanges();
            return RedirectToAction("DanhSachKhachHang");
        }
        public ActionResult DonHang()
        {
            var listdonhang = db.DatHang.ToList();
            return View(listdonhang);
        }
        public ActionResult chiTietDonHang(int id)
        {
            Model1 db = new Model1();
            ChiTietDatHang ct = db.ChiTietDatHang.FirstOrDefault(x => x.maHD == id);
            return View(ct);
        }
        public ActionResult suaDonHang(int id)
        {
            var listDonHang = db.DatHang.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            DatHang dh = listDonHang.Find(p => p.maHD == id);
            if (dh == null)
            {
                return HttpNotFound();
            }
            return View(dh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult suaDonHang(DatHang dh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listDatHang = db.DatHang.ToList();
                    var updateModel = listDatHang.Find(p => p.maHD == dh.maHD);
                    updateModel.tenKhachHang = dh.tenKhachHang;
                    updateModel.sdtKhachHang = dh.sdtKhachHang;
                    updateModel.diaChi = dh.diaChi;
                    updateModel.ghiChu = dh.ghiChu;
                    updateModel.tongTien = dh.tongTien;
                    updateModel.ptThanhToan = dh.ptThanhToan;
                    updateModel.tinhtrangThanhToan = dh.tinhtrangThanhToan;
                    updateModel.tinhtrangDonHang = dh.tinhtrangDonHang;
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Sửa dữ liệu thông tin đơn hàng không thành công");
                return View(dh);
            }

            return RedirectToAction("DonHang");
        }
        public ActionResult timKiemDonHang(string searchString)
        {
            var datHang = db.DatHang.Where(p => p.tenKhachHang.Contains(searchString)).ToList();
            return View(datHang);
        }
        public ActionResult danhSachTuVan(int? page)
        {
            if (page == null) page = 1;
            int pageNumber = page ?? 1;
            int pageSize = 10; //hiện bao nhiu tên trong 1 trang
            var listTV = db.TuVanKH.OrderBy(x => x.maTV).ToPagedList(pageNumber, pageSize);

            return View(listTV);
        }
        public ActionResult suaTuVan(int? id)
        {
            var listTV = db.TuVanKH.ToList();
            if (id == null)
            {
                return HttpNotFound();
            }
            TuVanKH tv = listTV.Find(p => p.maTV == id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            return View(tv);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult suaTuVan(TuVanKH tv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listtv = db.TuVanKH.ToList();
                    var updateModel = listtv.Find(p => p.maTV == tv.maTV);
                    updateModel.tinhTrang = tv.tinhTrang;
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Sửa dữ liệu tư vấn không thành công");
                return View(tv);
            }

            return RedirectToAction("danhSachTuVan");
        }
    }
}