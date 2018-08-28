using System;
using Microsoft.Extensions.Internal;

namespace DataValidation.Interfaces
{
    public interface IClockProvider : ISystemClock
    {
        DateTime DateTime { get; }
        DateTimeOffset AddHours(int hours);
        DateTimeOffset AddMinutes(int minutes);
        DateTimeOffset AddSeconds(int seconds);
        DateTimeOffset AddDays(int days);
        DateTimeOffset AddMonths(int months);
        DateTimeOffset AddYears(int years);
    }
}