using System;

namespace MEDIDEA.Core.Timing
{
    public class UnspecifiedClockProvider : IClockProvider
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
                return DateTimeKind.Unspecified;
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
            return dateTime;
        }

        internal UnspecifiedClockProvider()
        {
        }
    }
}