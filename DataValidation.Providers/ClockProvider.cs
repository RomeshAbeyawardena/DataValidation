using System;
using DataValidation.Interfaces;
using Microsoft.Extensions.Internal;

namespace DataValidation.Providers
{
    public class ClockProvider : IClockProvider
    {
        public ClockProvider(ISystemClock systemClock)
        {
            UtcNow = systemClock.UtcNow;
        }

        public DateTimeOffset UtcNow { get; }
        public DateTime DateTime => UtcNow.DateTime;

        public DateTimeOffset AddHours(int hours)
        {
            return UtcNow.AddHours(hours);
        }

        public DateTimeOffset AddMinutes(int minutes)
        {
            return UtcNow.AddMinutes(minutes);
        }

        public DateTimeOffset AddSeconds(int seconds)
        {
            return UtcNow.AddSeconds(seconds);
        }

        public DateTimeOffset AddDays(int days)
        {
            return UtcNow.AddDays(days);
        }

        public DateTimeOffset AddMonths(int months)
        {
            return UtcNow.AddMonths(months);
        }

        public DateTimeOffset AddYears(int years)
        {
            return UtcNow.AddYears(years);
        }
    }
}