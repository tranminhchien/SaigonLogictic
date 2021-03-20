using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    public interface ITimezoneListUtil
    {
        SelectListItem[] TimezoneList();
    }

    public class TimezoneListUtil : ITimezoneListUtil
    {
        public SelectListItem[] TimezoneList()
        {
            var tzs = TimeZoneInfo.GetSystemTimeZones();
            return tzs.Select(tz => new SelectListItem()
            {
                Text = tz.DisplayName,
                Value = tz.Id
            }).ToArray();
        }
    }
}
