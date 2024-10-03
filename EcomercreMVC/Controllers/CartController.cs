using EcomercreMVC.Data;
using EcomercreMVC.ViewsModel;
using Microsoft.AspNetCore.Mvc;

namespace EcomercreMVC.Controllers
{
    public class CartController : Controller
    {
        public Hshop2023Context db;

        public CartController(Hshop2023Context context) {
            db = context;
        }

        const string CART_KEY = "MYCART";

        public List<CartVM> Cart => HttpContext.Session.Get <List<CartVM>>(CART_KEY);

        public IActionResult Index()
        {
            return View(Cart);
        }

        //theme vao gio hang
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            // Lấy giỏ hàng từ session, nếu không tồn tại thì khởi tạo giỏ hàng rỗng
            var giohang = HttpContext.Session.Get<List<CartVM>>(CART_KEY) ?? new List<CartVM>();

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var item = giohang.SingleOrDefault(p => p.MaHh == id);

            if (item == null)
            {
                var hanghoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);

                if (hanghoa == null)
                {
                    TempData["Mess"] = "Không tìm thấy hàng hóa";
                    return Redirect("/404");
                }

                // Tạo mới CartVM cho sản phẩm
                item = new CartVM
                {
                    MaHh = hanghoa.MaHh,
                    TenHh = hanghoa.TenHh,
                    DonGia = hanghoa.DonGia ?? 0,
                    Hinh = hanghoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };

                giohang.Add(item);
            }
            else
            {
                // Nếu sản phẩm đã có trong giỏ hàng, tăng số lượng
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(CART_KEY, giohang);

            return RedirectToAction("Index"); 
        }

        //Xoa san pham
        public IActionResult RemoveCart(int id)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p =>p.MaHh == id);

            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(CART_KEY, giohang);
            }


            return RedirectToAction("Index");
        }

        //cap nhat
        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> quantities)
        {
            // Lấy giỏ hàng từ session
            var giohang = HttpContext.Session.Get<List<CartVM>>(CART_KEY) ?? new List<CartVM>();

            foreach (var quantity in quantities)
            {
                var item = giohang.SingleOrDefault(p => p.MaHh == quantity.Key);
                if (item != null)
                {
                    // Cập nhật số lượng
                    item.SoLuong = quantity.Value;
                }
            }
            TempData["Mess"] = "Cap nhat thanh cong";
         
            HttpContext.Session.Set(CART_KEY, giohang);

            // Redirect đến trang giỏ hàng hoặc trang khác
            return RedirectToAction("Index");
        }



    }
}
