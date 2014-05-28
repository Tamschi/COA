using System;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    /// <summary>
    /// Represents a drawing surface for color, depth, and stencil data.
    /// </summary>
    public class Framebuffer : IDisposable
    {
        private readonly int _fboHandle;

        public Framebuffer()
        {
            _fboHandle = GL.GenFramebuffer();
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _fboHandle);
        }

        public void Dispose()
        {
            GL.DeleteFramebuffer(_fboHandle);
        }
    }
}