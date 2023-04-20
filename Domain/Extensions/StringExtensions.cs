namespace Notino.Data.MSSQL.Extensions;

using System.Linq;
using System.Text.RegularExpressions;

public static class StringExtensions
{
    /// <summary>
    ///  Gets string between specified substrings
    /// </summary>
    /// <returns>Array of strings between specified substrings</returns>
    public static string[] Between(this string source, string firstSubString, string secondSubString)
    {
        var regex = new Regex($@"{firstSubString}\s+(\w+)\s+\{secondSubString}", RegexOptions.IgnoreCase);

        return regex
            .Matches(source)
            .Select(m => m.Groups[1].Value)
            .ToArray();
    }
}
