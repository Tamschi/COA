using System;
using System.IO;
using DeveloperCommands;
using OpenTK.Graphics.OpenGL;

namespace COA.Graphics
{
    public class Shader : IDisposable
    {
        protected readonly int ProgramHandle;
        protected readonly int VertexShaderHandle;
        protected readonly int FragmentShaderHandle;

        int attr_vcol, attr_vpos, attr_texcoords, uniform_mview;

        public int ColorAttribute
        {
            get { return attr_vcol; }
        }

        public int PositionAttribute
        {
            get { return attr_vpos; }
        }

        public int TexCoordAttribute
        {
            get { return attr_texcoords; }
        }

        public int MatrixUniform
        {
            get { return uniform_mview; }
        }

        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            // Create program object
            ProgramHandle = GL.CreateProgram();
            LoadShader(vertexShaderPath, ShaderType.VertexShader, ProgramHandle, out VertexShaderHandle);
            LoadShader(fragmentShaderPath, ShaderType.FragmentShader, ProgramHandle, out FragmentShaderHandle);
            GL.LinkProgram(ProgramHandle);
            Console.WriteLine(GL.GetProgramInfoLog(ProgramHandle));

            attr_vpos = GL.GetAttribLocation(ProgramHandle, "vPosition");
            attr_vcol = GL.GetAttribLocation(ProgramHandle, "vColor");
            uniform_mview = GL.GetUniformLocation(ProgramHandle, "modelview");

            // Make sure nothing is broken
            if ((attr_vcol | attr_vpos | uniform_mview) < 0)
            {
                Console.WriteLine("Error binding shader variables");
            }

            // Tell the shader how to read the data in the vPosition attribute.
            GL.VertexAttribPointer(attr_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            // Tell the shader how to read the data in the vColor attribute.
            GL.VertexAttribPointer(attr_vcol, 3, VertexAttribPointerType.Float, false, 0, 0);
        }

        public void Use()
        {
            GL.UseProgram(ProgramHandle);
        }

        public void EnableAttribs()
        {
            GL.EnableVertexAttribArray(attr_vpos);
            GL.EnableVertexAttribArray(attr_vcol);
        }

        public void DisableAttribs()
        {
            GL.DisableVertexAttribArray(attr_vpos);
            GL.DisableVertexAttribArray(attr_vcol);
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
