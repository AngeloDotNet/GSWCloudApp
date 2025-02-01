using System.ComponentModel;

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
        var enumString = value.ToString();
        var fi = value.GetType().GetField(enumString)!;
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length != 0 ? attributes[0].Description : enumString;
    }
}
