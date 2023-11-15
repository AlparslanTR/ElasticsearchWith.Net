using System.ComponentModel.DataAnnotations;

namespace ECommerceUI.Dtos
{
    public class ECommerceSearch
    {
        [Display(Name = "Kategori")]
        public string? Category { get; set; }
        [Display(Name = "Cinsiyet")]
        public string? Gender { get; set; }
        [Display(Name = "Ürün Tarihi Başlangıç")]
        [DataType(DataType.Date)]
        public DateTime? OrderDateStart { get; set; }
        [Display(Name = "Ürün Tarihi Bitiş")]
        public DateTime? OrderDateEnd { get; set; }
        [Display(Name = "Müşteri Ad Soyad")]
        public string? CustomerName { get; set; }
    }
}
