using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public List<PlantColor> PlantColors { get; set; }
    }
}
