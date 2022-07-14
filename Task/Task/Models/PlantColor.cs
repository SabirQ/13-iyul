using Task.Models.Base;

namespace Task.Models
{
    public class PlantColor:BaseEntity
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
