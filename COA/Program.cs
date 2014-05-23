using DeveloperCommands;

namespace COA
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Load();
            using (var window = new RenderWindow())
            {
                window.Run();
            }
            Devcom.SaveConfig();
            Engine.Unload();
        }
    }
}
