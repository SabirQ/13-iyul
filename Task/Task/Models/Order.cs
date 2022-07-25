using System;
using System.Collections.Generic;
using Task_MVC.Models.Base;

namespace Task.Models
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? Status { get; set; }
        public string Address { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
