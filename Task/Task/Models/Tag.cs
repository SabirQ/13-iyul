using System.Collections.Generic;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantTag> PlantTags { get; set; }
    }
}
