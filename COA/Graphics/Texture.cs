using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public class Texture : IDisposable
    {
        private readonly int _texHandle;

        private Texture()
        {
            _texHandle = GL.GenTexture();
        }

        public static Texture FromFile(string path)
        {
            var texture = new Texture();
            var bmp = (Bitmap)Image.FromFile(path);
            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

            texture.Bind();

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texture;
        }

        public static Texture Create(int width, int height)
        {
            var texture = new Texture();
            texture.Bind();

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texture;
        }

        public static void SetActiveUnit(int unit)
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + unit));
        }

        public void AttachTo(Framebuffer fbo)
        {
            fbo.Bind();
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _texHandle, 0);
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _texHandle);
        }

        public void Bind(int unit)
        {
            GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + unit));
            GL.BindTexture(TextureTarget.Texture2D, _texHandle);
            GL.ActiveTexture(TextureUnit.Texture0);
        }

        public void Bind(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, _texHandle);
            GL.ActiveTexture(TextureUnit.Texture0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(_texHandle);
        }
    }
}
