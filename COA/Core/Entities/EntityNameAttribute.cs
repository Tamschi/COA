using System;

namespace COA.Core.Entities
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EntityNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public EntityNameAttribute(string name)
        {
            Name = name;
        }
    }
}
