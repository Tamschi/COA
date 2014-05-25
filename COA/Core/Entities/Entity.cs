using System;
using OpenTK;

namespace COA.Core.Entities
{
    public abstract class Entity
    {
        protected Vector3 _pos, _angles, _scale;

        public Vector3 Position
        {
            get { return _pos; }
            set
            {
                OnPositionChanging(_pos, ref value);
                _pos = value;
            }
        }

        public Vector3 Angles
        {
            get { return _angles; }
            set
            {
                OnAnglesChanging(_angles, ref value);
                _angles = value;
            }
        }

        public Vector3 Scale
        {
            get { return _scale; }
            set
            {
                OnScaleChanging(_scale, ref value);
                _scale = value;
            }
        }

        public int ID
        {
            get { return _id; }
            set
            {
                if (World.GetEntityAt(value) == this) _id = value;
            }
        }

        private int _id = -1;
        private readonly string _entName, _entTag;

        public string EntityName
        {
            get { return _entName; }
        }

        public string Tag
        {
            get { return _entTag; }
        }

        public Entity()
        {
            _entName = EntityFactory.GetName(GetType());
        }

        public Entity(string tag)
        {
            _entName = EntityFactory.GetName(GetType());
            _entTag = tag ?? "";
        }

        public abstract void Update();

        public void Kill()
        {
            World.Remove(this);
            _id = -1;
            OnDeath();
        }

        protected virtual void OnPositionChanging(Vector3 posOld, ref Vector3 posNew)
        {
        }

        protected virtual void OnAnglesChanging(Vector3 anglesOld, ref Vector3 anglesNew)
        {
        }

        protected virtual void OnScaleChanging(Vector3 scaleOld, ref Vector3 scaleNew)
        {
        }

        protected virtual void OnDeath()
        {
        }

        public override string ToString()
        {
            return String.Concat(_entName, " ", _entTag, ": ", Position);
        }
    }
}
