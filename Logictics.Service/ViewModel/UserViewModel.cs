using System;
using System.Collections.Generic;
using System.Text;

namespace Logictics.Service.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
