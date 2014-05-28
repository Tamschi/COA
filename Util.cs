using System;
using System.Text.RegularExpressions;
using OpenTK.Graphics;

namespace COA
{
    public static class Util
    {
        public static int ParseOrDefault(string input, int defaultValue = 0)
        {
            int output;
            if (!Int32.TryParse(input, out output))
            {
                output = defaultValue;
            }
            return output;
        }

        public static Color4 ParseColor4(string input)
        {
            var color = new Color4(0f, 0f, 0f, 1f);
            var match = Regex.Match(input, @"(?<r>\d+(\.\d+)?)(,|\s+|,\s+)(?<g>\d+(\.\d+)?)(,|\s+|,\s+)(?<b>\d+(\.\d+)?)(,|\s+|,\s+)(?<a>\d+(\.\d+)?)?",
                RegexOptions.ExplicitCapture);
            if (match.Success)
            {
                var groups = match.Groups;
                if (groups["a"].Value != "")
                {
                    color.A = Single.Parse(groups["a"].Value);
                }
                color.R = Single.Parse(groups["r"].Value);
                color.G = Single.Parse(groups["g"].Value);
                color.B = Single.Parse(groups["b"].Value);
            }
            return color;
        }
    }
}