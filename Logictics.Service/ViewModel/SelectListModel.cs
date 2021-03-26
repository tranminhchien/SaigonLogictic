using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Logictics.Service.ViewModel {

    public class SelectedOptionModel {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class ListSelectOptionModel {
        public List<SelectedOptionModel> selectOption { get; set; }

        public SelectList CreateListSelectStatusOrder(string valueDefault) {
            this.selectOption = new List<SelectedOptionModel>();
            this.selectOption.Add(new SelectedOptionModel() { key = "New", value = "New" });
            this.selectOption.Add(new SelectedOptionModel() { key = "Pending", value = "Pending" });
            this.selectOption.Add(new SelectedOptionModel() { key = "StoreCancel", value = "StoreCancel" });

            return new SelectList(this.selectOption, "key", "value", valueDefault);
        }
    }
}