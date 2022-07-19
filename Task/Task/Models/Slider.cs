using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        [Required]
        public string Discount { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Article { get; set; }
        [Required]
        public string ButtonUrl { get; set; }
        [Required]
        public byte Order { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
