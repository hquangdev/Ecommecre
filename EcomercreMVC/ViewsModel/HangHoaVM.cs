namespace EcomercreMVC.ViewsModel
{
    public class HangHoaVM
    {

        public int MaHh { get; set; }   
        public String TenHh { get; set; }    
        public String Hinh { get; set; }    
        public Double DonGia { get; set; }  
        public String MoTa {  get; set; }

        public String TenLoai { get; set; }
    }

    public class ChiTietHangHoa
    {

        public int MaHh { get; set; }
        public String TenHh { get; set; }
        public String Hinh { get; set; }
        public Double DonGia { get; set; }
        public String MoTaNgan { get; set; }
        public String TenLoai { get; set; }

        public String ChiTiet { get; set; }

        public int DiemDanhGia { get; set; }

        public int SoLuongTon { get; set; }
    }
}
