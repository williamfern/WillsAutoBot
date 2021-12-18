using System;

namespace WillsAutoBot.Utilities.Extensions
{
    public static class DateExtensions
    {
        public static DateTime RoundDownToMinutes(this DateTime dateTime, int minutes)
        {
            var delta = dateTime.Ticks % TimeSpan.FromMinutes(minutes).Ticks;
            return new DateTime(dateTime.Ticks - delta, dateTime.Kind);
        }

        public static DateTime RoundToHour(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);

        public static DateTime RoundToHalfDay(this DateTime dateTime)
        {
            return dateTime.Hour < 12 ? dateTime.ToBeginningOfDay() : dateTime.ToMidOfDay();
        }

        public static DateTime ToBeginningOfDay(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
        
        public static DateTime ToMidOfDay(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0, dateTime.Kind);

        public static DateTime ToBeginningOfMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind);

        public static bool EqualsUpToMinutes(this DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.Year == dateTime2.Year && dateTime1.Month == dateTime2.Month && dateTime1.Day == dateTime2.Day &&
                   dateTime1.Hour == dateTime2.Hour && dateTime1.Minute == dateTime2.Minute && dateTime1.Kind == dateTime2.Kind;
        }   
    }
}
