using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public struct VertexAttribFormat
    {
        public VertexAttribPointerType Type;
        public int Components;
        public int Stride;
        public int Offset;

        public VertexAttribFormat(VertexAttribPointerType type, int components, int stride = 0, int offset = 0)
        {
            Type = type;
            Components = components;
            Stride = stride;
            Offset = offset;
        }

        public static readonly VertexAttribFormat Vector3 = new VertexAttribFormat(VertexAttribPointerType.Float, 3);
        public static readonly VertexAttribFormat Vector2 = new VertexAttribFormat(VertexAttribPointerType.Float, 2);
    }
}