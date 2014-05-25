using System;
using System.Threading.Tasks;
using DeveloperCommands;

namespace COA
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.Load();
            Start();
            while (true)
            {
                Console.Write(Context.Default.Prompt);
                Devcom.SendCommand(Console.ReadLine());
            }
        }

        static async void Start()
        {
            await Task.Run(() =>
            {
                using (var window = new RenderWindow())
                {
                    window.Run();
                }
                Devcom.SaveConfig();
                Engine.Unload();
                Environment.Exit(0);
            });
        }
    }
}
