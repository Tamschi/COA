using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public class Material : IDisposable
    {
        public const TextureUnit DiffuseMapUnit = TextureUnit.Texture0;
        public const TextureUnit SpecularMapUnit = TextureUnit.Texture1;
        public const TextureUnit NormalMapUnit = TextureUnit.Texture2;

        private readonly Shader _shader;
        private Texture _diffuseTexture;
        private Color4 _tint;

        private Material()
        {
            
        }

        public Material FromFile(string path)
        {
            
        }

        public void Apply()
        {
            _diffuseTexture.Bind(DiffuseMapUnit);
        }

        public void Dispose()
        {
            _diffuseTexture.Dispose();
        }
    }
}
