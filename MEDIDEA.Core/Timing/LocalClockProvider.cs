using System;

namespace MEDIDEA.Core.Timing
{
    public class LocalClockProvider : IClockProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTimeKind Kind
        {
            get
            {
                return DateTimeKind.Local;
            }
        }

        public bool SupportsMultipleTimezone
        {
            get
            {
                return false;
            }
        }

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            if (dateTime.Kind == DateTimeKind.Utc)
                return dateTime.ToLocalTime();
            return dateTime;
        }

        internal LocalClockProvider()
        {
        }
    }
}