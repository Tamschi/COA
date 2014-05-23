using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace COA.Core
{
    public abstract class Entity
    {
        public Vector3 Position;
        public Vector3 Angles;
        public Vector3 Scale;

        private readonly string _entName;

        public string EntityName
        {
            get { return _entName; }
        }

        public Entity()
        {
            _entName = EntityFactory.GetName(GetType());
        }

        public override string ToString()
        {
            return String.Concat(_entName, ": ", Position);
        }
    }
}
