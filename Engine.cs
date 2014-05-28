using System.Reflection;
using COA.Core.Entities;
using COA.Graphics;
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
            MH.Load();
            EntityFactory.Load();
            Devcom.Load(true, Assembly.GetEntryAssembly());
            ContextFilter.DefaultAdminFilter.ContextTypes = new[] {typeof(Context)};
            ContextFilter.DefaultAdminFilter.FilterPolicy = ContextFilterPolicy.IncludeDerived;
        }

        /// <summary>
        /// Releases all handles and unmanaged resources used by the engine.
        /// </summary>
        public static void Unload()
        {
            TextureCache.Clear();
        }
    }
}