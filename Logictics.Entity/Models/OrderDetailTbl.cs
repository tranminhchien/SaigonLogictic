using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Entity.Models
{
    public class OrderDetailTbl
    {
        public string Id { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int? Quality { get; set; }
        public int? Weight { get; set; }
        public int? Price { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public double? CreateDate { get; set; }
        public double? ModifyDate { get; set; }
    }
}
