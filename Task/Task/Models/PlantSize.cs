using Task.Models.Base;

namespace Task.Models
{
    public class PlantSize:BaseEntity
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}
