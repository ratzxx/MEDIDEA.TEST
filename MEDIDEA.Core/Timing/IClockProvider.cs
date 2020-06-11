using System;

namespace MEDIDEA.Core.Timing
{
    public interface IClockProvider
    {
        DateTime Now { get; }
        DateTimeKind Kind { get; }
        bool SupportsMultipleTimezone { get; }
        DateTime Normalize(DateTime dateTime);
    }
}