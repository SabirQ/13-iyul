using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantSize> PlantSizes { get; set; }
    }
}
