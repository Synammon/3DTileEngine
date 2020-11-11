using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace First3dDX
{
    public class Tile
    {
        public VertexPositionColorTexture[] Vertices;
        public int[] Indices;
        public VertexBuffer VertexBuffer;
        public IndexBuffer IndexBuffer;
        public int TileIndex;

        public Matrix Transformation { get; set; }
        public Vector3 Position { get; private set; }

        public Tile(float x, float y, Tileset tileset, int index)
        {
            Position = new Vector3(2 * x, 2 * y, 0f);
            Transformation = Matrix.CreateTranslation(Position);
            TileIndex = index;
            FillVerticies(tileset, x, y);
        }

        private void FillVerticies(Tileset tileset, float x, float y)
        {
            Vertices = new VertexPositionColorTexture[4];
            Vertices[0] = new VertexPositionColorTexture(
                new Vector3(0f + x , 0f + y , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].LowerLeft);
            Vertices[1] = new VertexPositionColorTexture(
                new Vector3(0f + x , 3f + y , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].UpperLeft);
            Vertices[2] = new VertexPositionColorTexture(
                new Vector3(3f + x , 3f + y , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].UpperRight);
            Vertices[3] = new VertexPositionColorTexture(
                new Vector3(3f + x , 0f + y , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].LowerRight);
            Indices = new int[6];

            Indices[0] = 0;
            Indices[1] = 1;
            Indices[2] = 2;
            Indices[3] = 2;
            Indices[4] = 3;
            Indices[5] = 0;
        }
    }
}
