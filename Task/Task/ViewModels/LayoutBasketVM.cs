using System.Collections.Generic;

namespace Task_MVC.ViewModels
{
    public class LayoutBasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
