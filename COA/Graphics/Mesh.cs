using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    /// <summary>
    /// Handles the buffering and rendering of mesh data.
    /// </summary>
    public class Mesh : IDisposable
    {
        private readonly int _vaoHandle;

        private readonly int _bVertices = -1;
        private readonly int _bTexCoords = -1;
        private readonly int _bNormals = -1;

        private readonly int _vCount;

        private Mesh(Vector3[] vertices, Vector2[] texcoords, Vector3[] normals)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException("vertices");
            }

            _vaoHandle = GL.GenVertexArray();
            GL.BindVertexArray(_vaoHandle);

            _bVertices = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _bVertices);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3.SizeInBytes * vertices.Length), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(Shader.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            if (texcoords != null)
            {
                _bTexCoords = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _bTexCoords);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector2.SizeInBytes*texcoords.Length), texcoords, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Shader.TexCoordAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);
            }

            if (normals != null)
            {
                _bNormals = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _bNormals);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector3.SizeInBytes * normals.Length), normals, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Shader.NormalAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            _vCount = vertices.Length;
        }

        public static Mesh FromData(MeshData data)
        {
            return new Mesh(data.GetVertices(), data.GetTexCoords(), data.GetNormals());
        }

        public static Mesh Quad(float width, float height)
        {
            return new Mesh(new []
            {
                new Vector3(0, 0, 0), 
                new Vector3(0, height, 0), 
                new Vector3(width, 0, 0), 
                new Vector3(0, height, 0),
                new Vector3(width, height, 0), 
                new Vector3(width, 0, 0) 
            },
            new []
            {
                new Vector2(0,0), 
                new Vector2(0,1),
                new Vector2(1,0), 
                new Vector2(0,1),
                new Vector2(1,1), 
                new Vector2(1,0)
            },
            null);
        }

        public void Draw(Shader shader, Matrix4 mModel, Matrix4 mViewProjection)
        {
            shader.SetMatModel(mModel);
            shader.SetMatViewProj(mViewProjection);

            shader.EnableAttribs();

            GL.BindVertexArray(_vaoHandle);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vCount);

            shader.DisableAttribs();
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(_vaoHandle);
            GL.DeleteBuffer(_bVertices);
            if (_bTexCoords != -1) GL.DeleteBuffer(_bTexCoords);
            if (_bNormals != -1) GL.DeleteBuffer(_bNormals);
        }
    }
}