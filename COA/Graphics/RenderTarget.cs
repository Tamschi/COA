using System;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    /// <summary>
    /// Provides a rendering surface for OpenGL scenes which exposes the rendered frame as a texture.
    /// </summary>
    public class RenderTarget : IDisposable
    {
        private readonly Renderbuffer _rbo;
        private readonly Framebuffer _fbo;
        private readonly Texture _texture;

        public Texture Texture
        {
            get { return _texture; }
        }

        public RenderTarget()
        {
            _fbo = new Framebuffer();
            _texture = Texture.Create(Convars.ResolutionWidth, Convars.ResolutionHeight);
            _texture.AttachTo(_fbo);
            _rbo = Renderbuffer.CreateDepthStencil(Convars.ResolutionWidth, Convars.ResolutionHeight);
            _rbo.AttachTo(_fbo);
        }

        public void Bind()
        {
            _fbo.Bind();
        }

        public static void BindDefault()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Dispose()
        {
            _fbo.Dispose();
            _rbo.Dispose();
            _texture.Dispose();
        }
    }
}