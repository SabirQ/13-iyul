using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class PlantImage:BaseEntity
    {
        public string Name { get; set; }
        public string Alternative { get; set; }
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public bool? Primary { get; set; }
    }
}
