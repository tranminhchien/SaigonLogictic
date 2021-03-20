using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Entity.Models
{
    public class StoreTbl
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public double? CreateDate { get; set; }
        public double? ModifyDate { get; set; }
    }
}
