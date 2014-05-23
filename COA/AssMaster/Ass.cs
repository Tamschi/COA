using System;
using System.Dynamic;

namespace COA
{
    public static class Ass
    {
        public const string HeadPath = "res";

        private static bool _loaded = false;
        private static Res _res = null;

        public static void Load(params string[] exclude)
        {
            if (_loaded) return;
            _loaded = true;
            _res = new Res(exclude);
        }

        public static void Unload()
        {
            if (!_loaded) return;
            _loaded = false;
            _res.Dispose();
        }

        public static dynamic R
        {
            get { return _res; }
        }

        public sealed class Res : DynamicObject
        {
            private static bool _instantiated = false;
            private readonly Pipe _head;
            public Res(params string[] exclude)
            {
                if (_instantiated)
                {
                    throw new InvalidOperationException("Res is already instantiated.");
                }
                _instantiated = true;
                _head = Pipe.FromFolder(HeadPath, exclude);
            }

            public void Dispose()
            {
                _head.Dispose();
            }

            public override bool TryGetMember(GetMemberBinder binder, out dynamic result)
            {
                return _head.TryGetMember(binder, out result);
            }
        }
    }
}