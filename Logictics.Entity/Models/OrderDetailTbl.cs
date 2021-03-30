using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Entity.Models
{
    public class OrderDetailTbl
    {
        public string id { get; set; }
        public string productCategoryId { get; set; }
        public string productCode { get; set; }
        public string description { get; set; }
        public int? quality { get; set; }
        public int? weight { get; set; }
        public int? price { get; set; }
        public string orderId { get; set; }
        public string status { get; set; }
        public double? createDate { get; set; }
        public double? modifyDate { get; set; }
    }
}
