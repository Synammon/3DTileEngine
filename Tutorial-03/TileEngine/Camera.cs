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

        internal void Lock(TileMap map, Matrix view, Matrix projection)
        {
            Vector3 v = Vector3.Transform(Position, projection * view);

            if (v.X < -map.MapWidth * Scale)
            {
                v.X = -map.MapWidth * Scale;
            } 
            else if (v.X > map.MapWidth * Scale)
            {
                v.X = map.MapWidth * Scale;
            }

            if (v.Y > map.MapHeight * Scale)
            {
                v.Y = map.MapHeight * Scale;
            }
            else if (v.Y < -map.MapHeight * Scale)
            {
                v.Y = -map.MapHeight * Scale;
            }

            Position = Vector3.Transform(v, Matrix.Invert(projection * view));
        }
    }
}
