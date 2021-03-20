using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Service.ViewModel
{
    public class OrderdetailCreateModel
    {
        public string Id { get; set; }
        public string productCode { get; set; }
        public string  description { get; set; }
        public int quality { get; set; }
        public int weight { get; set; }
        public int price { get; set; }
        public string categoryId { get; set; }
    }
}
