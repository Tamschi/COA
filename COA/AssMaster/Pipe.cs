using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace COA
{
    public sealed class Pipe : DynamicObject
    {
        private readonly Dictionary<string, Pipe> _pipes;
        private Stream _stream;

        private Pipe()
        {
            _pipes = new Dictionary<string, Pipe>();
            _stream = null;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name.ToLower();
            Pipe pipe = null;
            bool success = _pipes.TryGetValue(name, out pipe);
            result = pipe;
            return success;
        }

        public static implicit operator Stream(Pipe pipe)
        {
            return pipe._stream;
        }

        public static Pipe FromFolder(string path)
        {
            var pipe = new Pipe();
            foreach (var folderPath in Directory.GetDirectories(path))
            {
                pipe._pipes[Path.GetDirectoryName(folderPath).ToLower()] = FromFolder(folderPath);
            }
            foreach (var filePath in Directory.GetFiles(path))
            {
                pipe._pipes[Path.GetFileNameWithoutExtension(filePath).ToLower()] = FromFile(filePath);
            }
            return pipe;
        }

        public static Pipe FromFile(string path)
        {
            return new Pipe {_stream = File.OpenRead(path)};
        }
    }
}
