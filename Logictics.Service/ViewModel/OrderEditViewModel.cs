using Logictics.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Service.ViewModel {

    public class OrderEditViewModel {
        public OrderTbl orderTbl { get; set; }

        public List<OrderDetailTbl> listDetailTbl { get; set; }

        public UserAdmin Sender { get; set; }

        public UserAdmin Recipient { get; set; }

        public StoreTbl store { get; set; }

        public List<CategoryProductTbl> listCategoryTbl { get; set; }
    }
}