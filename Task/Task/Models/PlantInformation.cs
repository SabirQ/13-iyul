using System.Collections.Generic;
using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class PlantInformation:BaseEntity
    {
        public string Shipping { get; set; }
        public string AboutReturnRequest { get; set; }
        public string Guarentee { get; set; }
        public List<Plant> Plants { get; set; }
    }
}
