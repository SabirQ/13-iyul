using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
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
        [NotMapped]
        public List<int> CategoryIds { get; set; }
        [NotMapped]
        public List<int> TagsIds { get; set; }
        [NotMapped]
        public List<int> ColorsIds { get; set; }
        [NotMapped]
        public List<int> SizesIds { get; set; }


        [NotMapped]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        public IFormFile HoverPhoto { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
    }
}
