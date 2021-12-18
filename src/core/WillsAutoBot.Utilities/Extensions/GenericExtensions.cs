using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WillsAutoBot.Utilities.Extensions
{
    /// <summary>
    /// This represents the extension entity for generic.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Checks whether the given instance is <c>null</c> or empty.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to check.</param>
        /// <returns>Returns <c>True</c>, if the original instance is <c>null</c> or empty; otherwise returns <c>False</c>.</returns>
        public static bool IsNullOrDefault<T>(this T instance)
        {
            return instance == null || instance.Equals(default(T));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given instance is <c>null</c> or empty.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to check.</param>
        /// <returns>Returns the original instance, if the instance is NOT <c>null</c>; otherwise throws an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is <see langword="null"/></exception>
        public static T ThrowIfNullOrDefault<T>(this T instance) => instance.ThrowIfNullOrDefault(typeof(T).FullName);

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the given instance is <c>null</c> or empty.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to check.</param>
        /// <param name="parameterName">The name of the parameter in the calling location.</param>
        /// <returns>Returns the original instance, if the instance is NOT <c>null</c>; otherwise throws an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="instance"/> is <see langword="null"/></exception>
        public static T ThrowIfNullOrDefault<T>(this T instance, string parameterName)
        {
            if (instance.IsNullOrDefault())
            {
                throw new ArgumentNullException(parameterName);
            }

            return instance;
        }

        /// <summary>
        /// Converts an instance to a querystring value.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="instance">Instance to convert.</param>
        /// <returns>Returns the querystring converted.</returns>
        public static string ToQueryString<T>(this T instance)
        {
            instance.ThrowIfNullOrDefault();

            var serialised = JsonSerializer.Serialize(instance);
            var deserialised = JsonSerializer.Deserialize<Dictionary<string, string>>(serialised);
            var items = deserialised.Select(p => string.Join("=", p.Key, p.Value));
            var qs = string.Join("&", items);

            return qs;
        }
    }
}
