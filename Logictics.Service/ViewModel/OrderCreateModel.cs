﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Service.ViewModel
{
    public class OrderCreateModel
    {
        public string Id { get; set; } = "";
        public string StoreId { get; set; } = "";
        public string SenderId { get; set; } = "";
        public string RecipientId { get; set; } = "";
        public int? TotalWeight { get; set; }
        public string Note { get; set; } = "";
        public string Shipment { get; set; } = "";
        public List<OrderdetailCreateModel> listOrdertail { get; set; }

    }
}
