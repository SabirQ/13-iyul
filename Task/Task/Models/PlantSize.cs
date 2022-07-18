using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class PlantSize:BaseEntity
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}
