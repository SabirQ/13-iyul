using System.Collections.Generic;
using Task_MVC.Models;

namespace Task_MVC.ViewModels
{
    public class PlantVM
    {
        public Plant Plant { get; set; }
        public List<Plant> Plants { get; set; }
    }
}
