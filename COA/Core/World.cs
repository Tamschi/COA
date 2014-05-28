using COA.Core.Entities;
using DeveloperCommands;

namespace COA.Core
{
    [Category("world")]
    public static class World
    {
        public const int MaxEntities = 32768;

        private static readonly Entity[] ents = new Entity[MaxEntities];
        private static ICamera _camera = null;
        private static Entity _control = null;
        private static int entc = 0;

        public static ICamera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        public static Entity ControlEntity
        {
            get { return _control; }
            set { _control = value; }
        }

        public static bool Contains(Entity ent)
        {
            for (int i = 0; i < entc; i++)
            {
                if (ents[i] == ent) return true;
            }
            return false;
        }

        public static int IndexOf(Entity ent)
        {
            for (int i = 0; i < entc; i++)
            {
                if (ents[i] == ent) return i;
            }
            return -1;
        }

        public static bool Remove(Entity ent)
        {
            int i = IndexOf(ent);
            if (i == -1) return false;
            if (ents[i] == null) return false;
            ents[i] = null;
            while (entc > 0)
            {
                if (ents[entc - 1] == null) entc--;
            }
            return true;
        }

        public static bool RemoveAt(int i)
        {
            if (i < 0 || i >= entc) return false;
            if (ents[i] == null) return false;
            ents[i] = null;
            while (entc > 0)
            {
                if (ents[entc - 1] == null) entc--;
            }
            return true;
        }

        public static bool Add(Entity ent)
        {
            if (entc == MaxEntities || ent.ID != -1) return false;
            ents[ent.ID = entc++] = ent;
            return true;
        }

        public static Entity GetEntityAt(int i)
        {
            if (i < 0 || i >= entc) return null;
            return ents[i];
        }

        public static void Update()
        {
            
        }

        [Command("id", "Prints basic information about a specified entity ID.")]
        public static void Ident(Context context, int index)
        {
            if (index >= MaxEntities || index < 0)
            {
                context.Notify("Index out of range.");
                return;
            }
            context.Notify(ents[index] == null ? "null" : ents[index].ToString());
        }
    }
}