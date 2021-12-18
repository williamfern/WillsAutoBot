using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WillsAutoBot.Utilities.Extensions
{
    /// <summary>
    /// This represents the extension class for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether the string value is either <c>null</c> or white space.
        /// </summary>
        /// <param name="value"><see cref="string"/> value to check.</param>
        /// <returns>Returns <c>True</c>, if the string value is either <c>null</c> or white space; otherwise returns <c>False</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given value is <c>null</c> or white space.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>Returns the original value, if the value is NOT <c>null</c>; otherwise throws an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        public static string ThrowIfNullOrWhiteSpace(this string value) => value.ThrowIfNullOrWhiteSpace(nameof(value));

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given value is <c>null</c> or white space.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="parameterName">The name of the parameter in the calling location.</param>
        /// <returns>Returns the original value, if the value is NOT <c>null</c>; otherwise throws an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        public static string ThrowIfNullOrWhiteSpace(this string value, string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Checks whether the string value is equal to the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value is equal to the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool IsEquivalentTo(this string value, string comparer)
        {
            return value.Equals(comparer, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Converts the string value to <see cref="int"/> value.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <returns>Returns the <see cref="int"/> value converted.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/></exception>
        public static int ToInt32(this string value)
        {
            value.ThrowIfNullOrWhiteSpace();

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts the string value to <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <returns>Returns the <see cref="bool"/> value converted.</returns>
        public static bool ToBoolean(this string value)
        {
            return !value.IsNullOrWhiteSpace() && Convert.ToBoolean(value);
        }


        /// <summary>
        /// Checks whether the given list of items contains the item or not, regardless of casing.
        /// </summary>
        /// <param name="items">List of items.</param>
        /// <param name="item">Item to check.</param>
        /// <returns>Returns <c>True</c>, if the list of items contains the item; otherwise returns <c>False</c>.</returns>
        public static bool ContainsEquivalent(this IEnumerable<string> items, string item)
        {
            items.ThrowIfNullOrDefault();

            return items.Any(p => p.IsEquivalentTo(item));
        }

        /// <summary>
        /// Checks whether the string value starts with the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value starts with the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool StartsWithEquivalent(this string value, string comparer)
        {
            value.ThrowIfNullOrWhiteSpace();

            return !comparer.IsNullOrWhiteSpace() && value.StartsWith(comparer, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Checks whether the string value ends with the comparer, regardless of casing.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Comparing value.</param>
        /// <returns>Returns <c>True</c>, if the string value ends with the comparer, regardless of casing; otherwise returns <c>False</c>.</returns>
        public static bool EndsWithEquivalent(this string value, string comparer)
        {
            value.ThrowIfNullOrWhiteSpace();

            return !comparer.IsNullOrWhiteSpace() && value.EndsWith(comparer, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string ToSafeResourceIdString(this string resourceId) => resourceId.Replace('/', '=');

        public static string ToSafeScopeString(this string scope) => scope.Replace('/', '=');

        public static string ToSafeKeyHashString(this string keyHash) => keyHash.Replace('/', '=').Replace('\\', '=').Replace('#', '=').Replace('?', '=');

        public static string ToSafeServiceString(this string service) => service.Replace('/', '=');

        public static string ToSafeCategoryString(this string category) => category.Replace('/', '=');

        public static string ToSafeTimeString(this string time) => time.Replace('/', '=');

        public static string ToSafeNameString(this string name) => name.Replace("'", string.Empty).Replace(",", string.Empty).Replace("&", string.Empty).Replace(' ', '_');
        public static string ToSafeCompanyName(this string companyName) => companyName.Replace("'", string.Empty).Replace("\"", string.Empty).Replace("&", string.Empty).Trim();
        public static string RemoveWhitespaces(this string input) => input.Replace(" ", string.Empty);
        public static string ToSafeEmailString(this string email) => email.RemoveWhitespaces().ToLower();

        /// <summary>
        /// Gets Left part of URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>The left part of the URL e.g. "http://www.domain.com" or "https://localhost:8080"</returns>
        public static string GetUrlLeftPart(this string url)
        {
            return new Uri(url).GetLeftPart(UriPartial.Authority);
        }

        /// <summary>
        /// Truncates a given string to a max length specified when the text exceed the given max length.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (value.IsNullOrDefault())
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// check if string is a valid numeric.
        /// </summary>
        /// <param name="value">string value to check</param>
        /// <returns></returns>
        public static bool IsNumeric(this string value) => int.TryParse(value, out _);

        /// <summary>
        /// Masks all alphanumeric characters with Xs.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A masked string</returns>
        public static string ToMaskedIdentifier(this string id)
        {
            if (id is null || id.Trim().IsNullOrWhiteSpace())
                return id;

            var rgx = new Regex("[a-zA-Z0-9]");
            return rgx.Replace(id, "x");
        }

        /// <summary>
        /// Converts a list into a comma separated string.
        /// </summary>
        /// <param name="value">A list of string.</param>
        /// <returns>A comma separated string.</returns>
        public static string ToCommaSeparated(this IEnumerable<string> value)
        {
            return value != null ? string.Join(",", value) : string.Empty;
        }
    }
}
