using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Slider:BaseEntity
    {
        public string Image { get; set; }
        public string Discount { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string ButtonUrl { get; set; }
        public byte Order { get; set; }
    }
}
