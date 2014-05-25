using System;
using OpenTK;

namespace COA.Core.Entities
{
    [EntityName("camera_static")]
    public class EntityCameraStatic : Entity, ICamera
    {
        public const float ZNear = 0.01f;
        public const float ZFar = 1000.0f;

        private Matrix4 _matView, _matProj, _matViewProj;
        private float _fov = (float)Math.PI / 2;

        public EntityCameraStatic()
        {
            CreateMatrices();
        }

        public EntityCameraStatic(string tag) : base(tag)
        {
            CreateMatrices();
        }

        private void CreateMatrices()
        {
            CreateProjection();
            CreateView();
            CreateCombined();
        }

        private void CreateProjection()
        {
            _matProj = Matrix4.CreatePerspectiveFieldOfView(FOV,
                (float)Convars.ResolutionWidth / Convars.ResolutionHeight, ZNear, ZFar);
        }

        private void CreateView()
        {
            _matView = Matrix4.CreateRotationY(_angles.X) * 
                       Matrix4.CreateRotationX(_angles.Y) *
                       Matrix4.CreateRotationZ(_angles.Z);
        }

        private void CreateCombined()
        {
            Matrix4.Mult(ref _matView, ref _matProj, out _matViewProj);
        }

        public override void Update()
        {
            
        }

        public float FOV 
        {
            get { return _fov; }
            set
            {
                CreateProjection();
                _fov = value;
            } 
        }

        protected override void OnAnglesChanging(Vector3 anglesOld, ref Vector3 anglesNew)
        {
            CreateView();
        }

        protected override void OnPositionChanging(Vector3 posOld, ref Vector3 posNew)
        {
            CreateView();
        }


        public Matrix4 ViewProjectionMatrix
        {
            get
            {
                return _matViewProj;
            }
        }
    }
}