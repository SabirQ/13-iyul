using System.Collections.Generic;
using Task.Models.Base;

namespace Task.Models
{
    public class PlantInformation:BaseEntity
    {
        public string Shipping { get; set; }
        public string AboutReturnRequest { get; set; }
        public string Guarentee { get; set; }
        public List<Plant> Plants { get; set; }
    }
}
