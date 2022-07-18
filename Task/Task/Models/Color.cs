using System.Collections.Generic;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantColor> PlantColors { get; set; }
    }
}
