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

        private readonly VBO<Vector3> _vboPositions;
        private readonly VBO<Vector2> _vboTexCoords;
        private readonly VBO<Vector3> _vboNormals; 

        private readonly int _vCount;

        private Mesh(Vector3[] vertices, Vector2[] texcoords, Vector3[] normals)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException("vertices");
            }

            _vaoHandle = GL.GenVertexArray();
            GL.BindVertexArray(_vaoHandle);

            _vboPositions = new VBO<Vector3>(vertices, Shader.PositionAttribute, VertexAttribFormat.Vector3);

            if (texcoords != null)
            {
                _vboTexCoords = new VBO<Vector2>(texcoords, Shader.TexCoordAttribute, VertexAttribFormat.Vector2);
            }

            if (normals != null)
            {
                _vboNormals = new VBO<Vector3>(normals, Shader.NormalAttribute, VertexAttribFormat.Vector3);
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
            _vboPositions.Dispose();
            if (_vboTexCoords != null) _vboTexCoords.Dispose();
            if (_vboNormals != null) _vboNormals.Dispose();
        }
    }
}