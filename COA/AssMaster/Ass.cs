using System;
using System.Dynamic;

namespace COA
{
    public static class Ass
    {
        public const string HeadPath = "res";

        private static bool _loaded = false;
        private static Res _res = null;

        public static void Load()
        {
            if (_loaded) return;
            _loaded = true;
            _res = new Res();
        }

        public static dynamic R
        {
            get { return _res; }
        }

        public sealed class Res : DynamicObject
        {
            private static bool _instantiated = false;
            private readonly Pipe _head;
            public Res()
            {
                if (_instantiated)
                {
                    throw new InvalidOperationException("Res is already instantiated.");
                }
                _instantiated = true;
                _head = Pipe.FromFolder(HeadPath);
            }

            public override bool TryGetMember(GetMemberBinder binder, out dynamic result)
            {
                return _head.TryGetMember(binder, out result);
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                return false;
            }
        }
    }
}