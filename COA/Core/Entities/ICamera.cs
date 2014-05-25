using OpenTK;

namespace COA.Core.Entities
{
    public interface ICamera
    {
        float FOV { get; set; }
        Matrix4 ViewProjectionMatrix { get; }
    }
}