using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Color:BaseEntity
    {
        [Required, StringLength(maximumLength: 20)]
        public string Name { get; set; }
        public List<PlantColor> PlantColors { get; set; }
    }
}
