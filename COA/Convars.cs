using DeveloperCommands;

namespace COA
{
    [Category]
    public class Convars
    {
        [Convar("res_width", "The current resolution width.")]
        public static int ResolutionWidth = 1024;

        [Convar("res_height", "The current resolution height.")]
        public static int ResolutionHeight = 768;

        [Convar("fullscreen", "Determines if the game runs in fullscreen mode or not.")]
        public static bool Fullscreen = false;
    }
}