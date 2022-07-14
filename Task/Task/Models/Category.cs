using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
    }
}
