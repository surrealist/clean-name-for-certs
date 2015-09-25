using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GF.CleanNameForCerts {
  public static class Utils {

    public static IEnumerable<string> ToLines(this string lines) {
      var s = lines.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
      return s;
    }

    public static IEnumerable<string> ClearSpacesAndTabs(this IEnumerable<string> lines) {
      foreach (var line in lines) {
        var s = line.Trim();
        int n;

        do {
          n = s.Length;
          s = s.Replace("\t", " ");
          s = s.Replace("  ", " ");
        } while (s.Length != n);

        yield return s;
      }
    }

    public static IEnumerable<string> ClearPrefixes(this IEnumerable<string> lines) {
      foreach (var line in lines) {
        string s;

        s = Regex.Replace(line, @"^(?i)MR.", "");
        s = Regex.Replace(s, @"^(?i)MS.", "");
        s = Regex.Replace(s, @"^(?i)MISS", "");
        yield return s;
      }
    }

    public static IEnumerable<string> Capitalized(this IEnumerable<string> lines) {
      foreach (var line in lines) {
        string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var newText = "";
        foreach (var word in words) {
          newText += char.ToUpper(word[0]) + word.Substring(1).ToLower() + " ";
        }
        yield return newText.Trim();
      }
    }
  }
}
