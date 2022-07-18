using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class PlantCategory:BaseEntity
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
