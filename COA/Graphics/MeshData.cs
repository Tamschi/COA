using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using OpenTK;

namespace COA.Graphics
{
    /// <summary>
    /// Provides raw vertex data for a Mesh object.
    /// </summary>
    public class MeshData
    {
        private static readonly Regex regexVertex = new Regex(@"v\s+(?<v1>-?\d+(\.\d+)?)\s+(?<v2>-?\d+(\.\d+)?)\s+(?<v3>-?\d+(\.\d+)?)", RegexOptions.ExplicitCapture);
        private static readonly Regex regexFace = new Regex(@"f\s+(?<v1>\d+)(/(?<vt1>\d+)?/(?<vn1>\d)?)?\s+(?<v2>\d+)(/(?<vt2>\d+)?/(?<vn2>\d)?)?\s+(?<v3>\d+)(/(?<vt3>\d+)?/(?<vn3>\d)?)?", RegexOptions.ExplicitCapture);
        private static readonly Regex regexNormal = new Regex(@"vn\s+(?<vn1>-?\d+(\.\d+)?)\s+(?<vn2>-?\d+(\.\d+)?)\s+(?<vn3>-?\d+(\.\d+)?)", RegexOptions.ExplicitCapture);
        private static readonly Regex regexTexcoord = new Regex(@"vt\s+(?<vt1>-?\d+(\.\d+)?)\s+(?<vt2>-?\d+(\.\d+)?)", RegexOptions.ExplicitCapture);

        private static bool CheckRegex(string input, Regex regex, out GroupCollection groups)
        {
            var match = regex.Match(input);
            groups = match.Groups;
            return match.Success;
        }

        private readonly List<Vector3> _vertices = new List<Vector3>();
        private readonly List<Vector2> _texcoords = new List<Vector2>();
        private readonly List<Vector3> _normals = new List<Vector3>();

        /// <summary>
        /// Returns a boolean indicating if the data has a complete set of texture coordinates.
        /// </summary>
        public bool HasTexCoords
        {
            get { return _texcoords.Count == _vertices.Count; }
        }

        /// <summary>
        /// Returns a boolean indicating if the data has a complete set of normals.
        /// </summary>
        public bool HasNormals
        {
            get { return _normals.Count == _vertices.Count; }
        }

        public Vector3[] GetVertices()
        {
            return _vertices.ToArray();
        }

        public Vector2[] GetTexCoords()
        {
            return _texcoords.ToArray();
        }

        public Vector3[] GetNormals()
        {
            return _normals.ToArray();
        }

        public MeshData()
        {
        }

        public MeshData(IEnumerable<Vector3> vertices, IEnumerable<Vector2> texcoords = null, IEnumerable<Vector3> normals = null)
        {
            if (vertices != null) _vertices.AddRange(vertices);
            if (texcoords != null) _texcoords.AddRange(texcoords);
            if (normals != null) _normals.AddRange(normals);
        }

        public static MeshData FromObjFile(string path)
        {
            var data = new MeshData();
            var vertices = new List<Vector3>();
            var texcoords = new List<Vector2>();
            var normals = new List<Vector3>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    string line = reader.ReadLine().Trim();

                    if (line.StartsWith("#")) continue; // obj-style comment

                    GroupCollection groups;
                    if (CheckRegex(line, regexVertex, out groups))
                    {
                        vertices.Add(new Vector3(
                            Single.Parse(groups["v1"].Value),
                            Single.Parse(groups["v2"].Value),
                            Single.Parse(groups["v3"].Value)
                            ));
                    }
                    else if (CheckRegex(line, regexTexcoord, out groups))
                    {
                        texcoords.Add(new Vector2(
                            Single.Parse(groups["vt1"].Value),
                            Single.Parse(groups["vt2"].Value)
                            ));
                    }
                    else if (CheckRegex(line, regexNormal, out groups))
                    {
                        normals.Add(new Vector3(
                            Single.Parse(groups["vn1"].Value),
                            Single.Parse(groups["vn2"].Value),
                            Single.Parse(groups["vn3"].Value)
                            ));
                    }
                    else if (CheckRegex(line, regexFace, out groups))
                    {
                        int v1 = Util.ParseOrDefault(groups["v1"].Value) - 1;
                        int v2 = Util.ParseOrDefault(groups["v2"].Value) - 1;
                        int v3 = Util.ParseOrDefault(groups["v3"].Value) - 1;

                        int vt1 = Util.ParseOrDefault(groups["vt1"].Value) - 1;
                        int vt2 = Util.ParseOrDefault(groups["vt2"].Value) - 1;
                        int vt3 = Util.ParseOrDefault(groups["vt3"].Value) - 1;

                        int vn1 = Util.ParseOrDefault(groups["vn1"].Value) - 1;
                        int vn2 = Util.ParseOrDefault(groups["vn2"].Value) - 1;
                        int vn3 = Util.ParseOrDefault(groups["vn3"].Value) - 1;

                        bool vtExists = (vt1 | vt2 | vt3) > -1;
                        bool vnExists = (vn1 | vn2 | vn3) > -1;

                        data._vertices.AddRange(new[]
                        {
                            vertices[v1],
                            vertices[v2],
                            vertices[v3]
                        });

                        if (vtExists)
                        {
                            data._texcoords.AddRange(new[]
                            {
                                texcoords[vt1],
                                texcoords[vt2],
                                texcoords[vt3]
                            });
                        }

                        if (vnExists)
                        {
                            data._normals.AddRange(new[]
                            {
                                normals[vn1],
                                normals[vn2],
                                normals[vn3]
                            });
                        }
                    }
                }
            }
            return data;
        }
    }
}