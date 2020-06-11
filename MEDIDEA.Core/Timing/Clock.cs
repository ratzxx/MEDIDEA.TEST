using System;

namespace MEDIDEA.Core.Timing
{
    public static class  Clock
    {
        private static IClockProvider _provider;

        public static IClockProvider Provider
        {
            get
            {
                return Clock._provider;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Can not set Clock.Provider to null!");
                Clock._provider = value;
            }
        }

        static Clock()
        {
            Clock.Provider = (IClockProvider)ClockProviders.Unspecified;
        }

        public static DateTime Now
        {
            get
            {
                return Clock.Provider.Now;
            }
        }

        public static DateTimeKind Kind
        {
            get
            {
                return Clock.Provider.Kind;
            }
        }

        public static bool SupportsMultipleTimezone
        {
            get
            {
                return Clock.Provider.SupportsMultipleTimezone;
            }
        }

        public static DateTime Normalize(DateTime dateTime)
        {
            return Clock.Provider.Normalize(dateTime);
        }
    }
}
