using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class PlantColor:BaseEntity
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
