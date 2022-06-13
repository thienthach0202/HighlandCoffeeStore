using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStore.Models.ViewModels
{
    public class CoffeeViewModels
    {
        [Key]
        public long CoffeeID { get; set; }
        [Display(Name = "Tên Món")]
        public string Title { get; set; }
        [Display(Name = "Kích cở ly")]
        public string Size { get; set; }
        [Display(Name = "Loại Đồ Ăn")]
        public string Genre { get; set; }
        [Display(Name = "Giá")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public IFormFile ImageCoffee { get; set; }
    }
}
