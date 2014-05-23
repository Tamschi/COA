using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace COA
{
    public sealed class RenderWindow : GameWindow
    {
        public RenderWindow()
        {
            Width = Convars.ResolutionWidth;
            Height = Convars.ResolutionHeight;
            Title = "Coming of Age";
            Center();
        }

        private void Center()
        {
            var screen = Screen.PrimaryScreen.Bounds;
            X = screen.Width / 2 - Width / 2;
            Y = screen.Height / 2 - Height / 2;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0, 0, 1, 1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }
    }
}
