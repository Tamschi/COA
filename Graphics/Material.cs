using System;
using System.IO;
using System.Text.RegularExpressions;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public class Material : IDisposable
    {
        public const TextureUnit DiffuseMapUnit = TextureUnit.Texture0;
        public const TextureUnit SpecularMapUnit = TextureUnit.Texture1;
        public const TextureUnit NormalMapUnit = TextureUnit.Texture2;

        private Texture _diffuseTexture;
        private Color4 _tint;

        private Material()
        {
            
        }

        public Material FromFile(string path)
        {
            var mat = new Material();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();
                    if (line.StartsWith("#") || line.Length == 0) continue;
                    var match = Regex.Match(line, @"^(?<key>[\w\d_\-]+)\s+=\s+(?<value>\S+)", RegexOptions.ExplicitCapture);
                    if (!match.Success) continue;
                    var groups = match.Groups;
                    string value = groups["value"].Value;
                    switch (groups["key"].Value.ToLower())
                    {
                        case "diffusemap":
                            _diffuseTexture = TextureCache.GetTexture(value);
                            break;
                        case "tint":
                            _tint = Util.ParseColor4(value);
                            break;
                        default:
                            continue;
                    }
                }
            }
            return mat;
        }

        public void Apply()
        {
            _diffuseTexture.Bind(DiffuseMapUnit);
            // TODO: Apply tint to shader here
        }

        public void Dispose()
        {
            _diffuseTexture.Dispose();
        }
    }
}
