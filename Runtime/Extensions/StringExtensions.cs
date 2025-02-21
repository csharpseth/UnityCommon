using System.Text;
using System.Text.RegularExpressions;

namespace MooshieGames.Common
{
	public static class StringExtensions
	{
    		public static string AddSpacesToPascalCase(this string input)
    		{
        		return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
    		}
	}
}
