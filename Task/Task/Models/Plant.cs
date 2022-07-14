using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class Plant:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }
        public string SKU { get; set; }
        public int PlantInformationId { get; set; }
        public PlantInformation PlantInformation { get; set; }
        public List<PlantImage> PlantImages { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
        public List<PlantTag> PlantTags { get; set; }
        public List<PlantColor> PlantColors { get; set; }
        public List<PlantSize> PlantSizes { get; set; }
    }
}
