using EcomercreMVC.Data;
using EcomercreMVC.ViewsModel;
using Microsoft.AspNetCore.Mvc;

namespace EcomercreMVC.Viewcomponents
{

    public class MenuLoai : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoai(Hshop2023Context context) => db = context;


        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).ToList();  // Chuyển đổi kết quả thành danh sách

            return View(data);
        }
    }
}
