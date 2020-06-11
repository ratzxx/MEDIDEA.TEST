﻿using System;

namespace MEDIDEA.Core.Timing
{
    public class UtcClockProvider : IClockProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        public DateTimeKind Kind
        {
            get
            {
                return DateTimeKind.Utc;
            }
        }

        public bool SupportsMultipleTimezone
        {
            get
            {
                return true;
            }
        }

        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime.ToUniversalTime();
            return dateTime;
        }

        internal UtcClockProvider()
        {
        }
    }
}