using System.Collections.Generic;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
    }
}
