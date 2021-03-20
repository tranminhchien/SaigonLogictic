using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utils
{
    public interface IDatetimeUtil
    {
        double ConvertLocalTimeToTimestamp(DateTime localTime);
        double ConvertUniversalTimeToTimestamp(DateTime universalTime);
    }

    public class DatetimeUtil : IDatetimeUtil
    {
        public double ConvertUniversalTimeToTimestamp(DateTime universalTime)
        {
            DateTime milestones = new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime();
            return (universalTime - milestones).TotalMilliseconds;
        }

        public double ConvertLocalTimeToTimestamp(DateTime localTime)
        {
            DateTime milestones = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
            return (localTime - milestones).TotalMilliseconds;
        }
    }
}
