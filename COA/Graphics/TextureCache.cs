using System.Collections.Generic;

namespace COA.Graphics
{
    public static class TextureCache
    {
        private static readonly Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();

        public static Texture GetTexture(string texturePath)
        {
            Texture texture;
            if (!_textures.TryGetValue(texturePath, out texture))
            {
                _textures[texturePath] = texture = Texture.FromFile(texturePath);
            }
            return texture;
        }

        public static void Clear()
        {
            foreach (var pair in _textures)
            {
                pair.Value.Dispose();
            }
            _textures.Clear();
        }
    }
}