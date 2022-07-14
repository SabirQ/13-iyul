using Task.Models.Base;

namespace Task.Models
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
