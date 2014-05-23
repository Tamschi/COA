﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeveloperCommands;

namespace COA.Core
{
    public static class EntityFactory
    {
        private static readonly Dictionary<string, Type> entityTypes = new Dictionary<string, Type>();
        private static bool _loaded = false;

        public static void Load()
        {
            if (_loaded) return;
            var entBaseType = typeof(Entity);
            foreach (var type in Assembly.GetAssembly(typeof(EntityFactory)).GetTypes()
                .Where(type => type.IsSubclassOf(entBaseType)))
            {
                var attr = type.GetCustomAttribute<EntityNameAttribute>();
                if (attr == null) continue;
                entityTypes[attr.Name.ToLower()] = type;
            }
            _loaded = true;
        }

        public static string GetName(Type entType)
        {
            if (!entType.IsSubclassOf(typeof(Entity))) return "";
            var pairs = entityTypes.Where(p => p.Value == entType);
            var keyValuePairs = pairs as KeyValuePair<string, Type>[] ?? pairs.ToArray();
            return !keyValuePairs.Any() ? null : keyValuePairs.First().Key;
        }

        public static Entity CreateEntity(string name)
        {
            name = name.ToLower();
            Type entType;
            if (entityTypes.TryGetValue(name, out entType)) return (Entity)Activator.CreateInstance(entType);
            Devcom.Print("Attempted to create non-existent entity type '" + name + "'");
            return null;
        }
    }
}