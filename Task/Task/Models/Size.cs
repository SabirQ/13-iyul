using System.Collections.Generic;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantSize> PlantSizes { get; set; }
    }
}
