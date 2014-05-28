using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public class VBO<T> : IDisposable 
        where T : struct
    {
        private readonly int _handle;
        private readonly int _count;
        private readonly int _index;
        private readonly VertexAttribFormat _format;

        public VertexAttribFormat Format
        {
            get { return _format; }
        }

        public int AttributeIndex
        {
            get { return _index; }
        }

        public int Count
        {
            get { return _count; }
        }

        public VBO(T[] buffer, int attribIndex, VertexAttribFormat format,
            BufferUsageHint usage = BufferUsageHint.StaticDraw)
        {
            _index = attribIndex;
            _format = format;
            _handle = GL.GenBuffer();
            _count = buffer.Length;
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf(typeof(T))*buffer.Length), buffer, usage);
            SetVertexAttrib();
        }

        public void SetVertexAttrib()
        {
            GL.VertexAttribPointer(_index, _format.Components, _format.Type, false, _format.Stride, _format.Offset);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}