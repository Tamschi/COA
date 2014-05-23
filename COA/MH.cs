using System;
using DeveloperCommands;
using Manhood;

namespace COA
{
    /// <summary>
    /// Manhood provider class.
    /// </summary>
    [Category("man")]
    public static class MH
    {
        private static ManEngine _manhood = null;
        private static readonly ManRandom _random = new ManRandom();
        private static bool _loaded = false;

        public static void Load()
        {
            if (_loaded) return;
            _manhood = new ManEngine("res\\manhood\\base.man");
            _manhood.Errors += ManhoodOnErrors;
            _loaded = true;
        }

        private static void ManhoodOnErrors(object sender, ManhoodErrorEventArgs e)
        {
            Devcom.PrintFormat("Manhood errors ({0})", e.Errors.Count);
            foreach (var error in e.Errors)
            {
                Devcom.Print(error.Message);
            }
        }

        public static string GenerateText(string pattern)
        {
            return _manhood.GenerateText(_random, pattern);
        }

        public static string GenerateText(ManRandom rng, string pattern)
        {
            return _manhood.GenerateText(rng, pattern);
        }

        public static string GenerateText(long seed, string pattern)
        {
            return _manhood.GenerateText(new ManRandom(seed), pattern);
        }

        [Command("generate", "Interprets a pattern and prints the result.")]
        private static void Generate(Context context, string pattern)
        {
            var og = _manhood.GenerateOutputGroup(_random, pattern);
            foreach (var output in og)
            {
                context.Notify(String.Concat(output.Key, ": ", output.Value.ToString()));
            }
        }
    }
}
