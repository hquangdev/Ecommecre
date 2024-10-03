namespace EcomercreMVC.ViewsModel
{
    public class CartVM
    {
            public int MaHh { get; set; }
            public String TenHh { get; set; }
            public String Hinh { get; set; }
            public double DonGia { get; set; }
            public int SoLuong { get; set; }

        public double ThanhTien => DonGia * SoLuong;
       
    }
}
