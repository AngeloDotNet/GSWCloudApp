using System.ComponentModel;

namespace GSWCloudApp.Common.Extensions;

public static class EnumerationExtensions
{
    public static string GetDescription(this Enum value)
    {
        var enumString = value.ToString();
        var fi = value.GetType().GetField(enumString)!;
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length != 0 ? attributes[0].Description : enumString;
    }
}