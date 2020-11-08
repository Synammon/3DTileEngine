using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Text;

namespace First3dDX
{
    public class Camera
    {
        public Vector3 Position;
        public float Speed { get; set; }
        public float Scale { get; set; }

        public float Rotation { get; set; }

        public Matrix Transformation => Matrix.CreateScale(Scale) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateTranslation(-Position);

        public Camera()
        {
            Scale = 1;
            Rotation = 0;
        }
    }
}
