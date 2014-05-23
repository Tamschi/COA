using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace COA
{
    public sealed class Pipe : DynamicObject
    {
        private readonly Dictionary<string, Pipe> _pipes;
        private byte[] _data;

        private Pipe()
        {
            _pipes = new Dictionary<string, Pipe>();
            _data = null;
        }

        public void Dispose()
        {
            foreach (var pair in _pipes)
            {
                pair.Value.Dispose();
            }
            _pipes.Clear();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name.ToLower();
            Pipe pipe = null;
            bool success = _pipes.TryGetValue(name, out pipe);
            result = pipe;
            return success;
        }

        public static implicit operator byte[](Pipe pipe)
        {
            return pipe._data;
        }

        public static implicit operator string(Pipe pipe)
        {
            return Encoding.UTF8.GetString(pipe._data);
        }

        public static Pipe FromFolder(string path, params string[] filterExclude)
        {
            var pipe = new Pipe();
            foreach (var folderPath in Directory.GetDirectories(path))
            {
                pipe._pipes[Path.GetDirectoryName(folderPath).ToLower()] = FromFolder(folderPath, filterExclude);
            }
            foreach (var filePath in Directory.GetFiles(path).Where(str => filterExclude.All(filter => !str.ToLower().EndsWith(filter))))
            {
                pipe._pipes[Path.GetFileNameWithoutExtension(filePath).ToLower()] = FromFile(filePath);
            }
            return pipe;
        }

        public static Pipe FromFile(string path)
        {
            return new Pipe {_data = File.ReadAllBytes(path)};
        }
    }
}
