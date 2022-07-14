using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantTag> PlantTags { get; set; }
    }
}
