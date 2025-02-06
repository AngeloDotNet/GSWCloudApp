using System.ComponentModel;
using System.Reflection;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides extension methods for enumerations.
/// </summary>
public static class EnumerationExtensions
{
    /// <summary>
    /// Gets the description attribute of an enumeration value.
    /// </summary>
    /// <param name="value">The enumeration value.</param>
    /// <returns>The description of the enumeration value if it exists; otherwise, the enumeration value as a string.</returns>
    public static string GetDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString())!;

        if (fi.GetCustomAttribute(typeof(DescriptionAttribute), false) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return value.ToString();
    }
}
