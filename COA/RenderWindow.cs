using System;
using System.Windows.Forms;
using COA.Core;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

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

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Environment.Exit(0);
            }
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            World.Update();
        }
    }
}
