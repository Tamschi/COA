using System;
using DeveloperCommands;

namespace COA
{
    [Category]
    public static class Commands
    {
        [Command("exit", "Immediately terminates the process.")]
        public static void Exit(Context context)
        {
            Environment.Exit(0);
        }

        [Command("die", "Causes instant death.")]
        public static void Die(Context context)
        {
            context.Notify("Doesn't do anything yet.");
        }
    }
}