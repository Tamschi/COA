using System.Reflection;
using COA.Core;
using DeveloperCommands;

namespace COA
{
    public static class Engine
    {
        /// <summary>
        /// Loads and prepares all vital resources used by the engine.
        /// </summary>
        public static void Load()
        {
            Ass.Load(".man");
            MH.Load();
            EntityFactory.Load();
            Devcom.Load(true, Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Releases all handles and unmanaged resources used by the engine.
        /// </summary>
        public static void Unload()
        {
            Ass.Unload();
        }
    }
}