using System;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    /// <summary>
    /// Represents formatted image data that is used by a framebuffer.
    /// </summary>
    public class Renderbuffer : IDisposable
    {
        private readonly int _rboHandle;

        private Renderbuffer()
        {
            _rboHandle = GL.GenRenderbuffer();
        }

        public static Renderbuffer CreateDepthStencil(int width, int height)
        {
            var rbo = new Renderbuffer();
            rbo.Bind();
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, width, height);
            return rbo;
        }

        public void AttachTo(Framebuffer fbo)
        {
            fbo.Bind();
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, _rboHandle);
        }

        public void Bind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _rboHandle);
        }

        public void Dispose()
        {
            GL.DeleteRenderbuffer(_rboHandle);
        }
    }
}