using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Entity.Models
{
    public class OrderTbl
    {
        public string Id { get; set; }
        public string StoreId { get; set; }
        public int TotalWeight { get; set; }
        public string Status { get; set; }
        public double? CreateDate { get; set; }
        public double? ModifyDate{ get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string CustomerConfirmId { get; set; }
        public string Notes { get; set; }
        public string Shipment { get; set; }
        public double? PickupDate { get; set; }
    }
}
