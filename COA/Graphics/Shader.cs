using System;
using System.Drawing.Drawing2D;
using System.IO;
using DeveloperCommands;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public class Shader : IDisposable
    {
        protected readonly int ProgramHandle;
        protected readonly int VertexShaderHandle;
        protected readonly int FragmentShaderHandle;

        public const int PositionAttribute = 0;
        public const int TexCoordAttribute = 1;
        public const int NormalAttribute = 2;

        private readonly int _unifMatModel;
        private readonly int _unifMatViewProj;
        private readonly int _unifDiffuseTexture;
        private readonly int _unifTint;

        public void SetMatModel(Matrix4 matrix)
        {
            GL.UniformMatrix4(_unifMatModel, false, ref matrix);
        }

        public void SetMatViewProj(Matrix4 matrix)
        {
            GL.UniformMatrix4(_unifMatViewProj, false, ref matrix);
        }

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            // Create program object
            ProgramHandle = GL.CreateProgram();
            LoadShader(vertexShaderPath, ShaderType.VertexShader, ProgramHandle, out VertexShaderHandle);
            LoadShader(fragmentShaderPath, ShaderType.FragmentShader, ProgramHandle, out FragmentShaderHandle);
            GL.LinkProgram(ProgramHandle);
            Devcom.Print(GL.GetProgramInfoLog(ProgramHandle));

            _unifMatModel = GL.GetUniformLocation(ProgramHandle, "mModel");
            _unifMatViewProj = GL.GetUniformLocation(ProgramHandle, "mViewProjection");
            _unifDiffuseTexture = GL.GetUniformLocation(ProgramHandle, "DiffuseMap");
            _unifTint = GL.GetUniformLocation(ProgramHandle, "Tint");

            GL.ProgramUniform1(ProgramHandle, _unifDiffuseTexture, (int)Material.DiffuseMapUnit);

            // Make sure nothing is broken
            if ((_unifMatModel | _unifMatViewProj | _unifDiffuseTexture | _unifTint) < 0)
            {
                Devcom.Print("Error binding shader variables");
            }
        }

        public void Use()
        {
            GL.UseProgram(ProgramHandle);
        }

        public void EnableAttribs()
        {
            GL.EnableVertexAttribArray(PositionAttribute);
            GL.EnableVertexAttribArray(TexCoordAttribute);
        }

        public void DisableAttribs()
        {
            GL.DisableVertexAttribArray(PositionAttribute);
            GL.DisableVertexAttribArray(TexCoordAttribute);
        }

        private static void LoadShader(string filename, ShaderType type, int program, out int address)
        {
            // Create shader object
            address = GL.CreateShader(type);

            // Load source code into shader object
            using (var reader = new StreamReader(filename))
            {
                GL.ShaderSource(address, reader.ReadToEnd());
            }

            // Compile that shit and attach it to the program object
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            var log = GL.GetShaderInfoLog(address).Trim();
            if (log.Length > 0)
            {
                Devcom.Print("Shader error log for '" + filename + "':\n" + log);
            }
        }

        public void Dispose()
        {
            GL.DetachShader(ProgramHandle, FragmentShaderHandle);
            GL.DeleteShader(FragmentShaderHandle);
            GL.DetachShader(ProgramHandle, VertexShaderHandle);
            GL.DeleteShader(VertexShaderHandle);
            GL.DeleteProgram(ProgramHandle);
        }
    }
}
