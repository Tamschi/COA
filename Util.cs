using System;

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
    }
}