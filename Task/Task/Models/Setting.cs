using Task_MVC.Models.Base;

namespace Task_MVC.Models
{
    public class Setting:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
