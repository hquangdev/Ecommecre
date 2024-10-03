using EcomercreMVC.Data;
using EcomercreMVC.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomercreMVC.Controllers
{
    public class HanghoaController : Controller
    {
        private readonly Hshop2023Context db;

        public HanghoaController(Hshop2023Context context) {
            
         db = context;
            
        }

        public IActionResult Index(int? loai)
        {
            var hanghoas = db.HangHoas.AsQueryable();

            if (loai.HasValue)
            {
                hanghoas = hanghoas.Where(p => p.MaLoai == loai.Value);
            }

            var result = hanghoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTa = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            }).ToList();  // Chuyển đổi thành danh sách để thực thi truy vấn

            return View(result); // Trả về view với danh sách hàng hóa
        }

        //phan tim kiem
        public IActionResult Timkiem(String? query)
        {

            var hanghoas = db.HangHoas.AsQueryable(); //AsQueryble() huyển đổi các tập hợp dữ liệu (collections) sang dạng IQueryable<T>

            if (query != null)
            {
                hanghoas = hanghoas.Where(p => p.TenHh.Contains(query));
            }

            var result = hanghoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTa = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            }).ToList();  // Chuyển đổi thành danh sách để thực thi truy vấn

            return View(result); 
        }

        //chuyen toi trang chi tiet san pham
        public IActionResult Detail(int? id)
        {
            var data = db.HangHoas.Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);     //SingleOrDefault: tìm kiếm một bản ghi duy nhất 

            if (data == null)
            {
                TempData["Mess"] = $"khong tim thay san pham co ma {id}";
                return Redirect("/404");
            }

            var result = new ChiTietHangHoa()
            {
                MaHh = data.MaHh,
                TenHh = data.TenHh,
                DonGia = data.DonGia ?? 0,
                Hinh = data.Hinh ?? "",
                MoTaNgan = data.MoTaDonVi ?? "",
                ChiTiet = data.MoTa ?? string.Empty,
                DiemDanhGia = 5,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10

            };
            return View(result);
        }
      

    }
}
