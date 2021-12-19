using System;

namespace WillsAutoBot.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static long GetReversedTicks(this DateTime dateTime) => DateTimeOffset.MaxValue.Ticks - dateTime.Ticks;

        public static long GetReversedTicks(this DateTimeOffset dateTimeOffset) =>
            DateTimeOffset.MaxValue.Ticks - dateTimeOffset.UtcTicks;

        public static string GetReversedTicksAsPaddedString(this DateTime dateTime) =>
            GetReversedTicks(dateTime).ToString().PadLeft(20, '0');

        public static string GetReversedTicksAsPaddedString(this DateTimeOffset dateTimeOffset) =>
            GetReversedTicks(dateTimeOffset).ToString().PadLeft(20, '0');

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts 03/18/2019 4:02 AM +00 to Unix timestamp
        /// </summary>
        /// <param name="date">The date to convert</param>
        /// <returns>The unix timestamp (in milliseconds).</returns>
        public static long ConvertDateTimeToUnixTimestamp(string date)
        {
            var dateTime = Convert.ToDateTime(date);
            return (long)((Convert.ToDateTime(dateTime) - UnixEpoch).TotalSeconds * 1000.0);
        }

        /// <summary>
        /// Converts a date to Unix timestamp
        /// </summary>
        /// <param name="date">The date to convert</param>
        /// <returns>The unix timestamp (in milliseconds).</returns>
        public static long ToUnixTimestamp(this DateTime date) => date.Subtract(UnixEpoch).Ticks / 10_000;

        /// <summary>
        /// Converts a date to Unix (Epoch) timestamp in seconds.
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>The Unix (Epoch) timestamp in seconds.</returns>
        public static long ToUnixTimeSeconds(this DateTime date) => new DateTimeOffset(date).ToUnixTimeSeconds();

        /// <summary>
        /// Converts to ISO 8601 date time string of format yyyy-MM-ddTHH:mm:ssZ.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToIso8601Date(this DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

        /// <summary>
        /// Get Australian eastern standard DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset GetAusEasternStandardTime(this DateTime dateTime) =>
            TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time"));
    }
}