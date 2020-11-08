using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace First3dDX
{
    public class Tile
    {
        public VertexPositionColorTexture[] Vertices;
        public int[] Indices;
        public Matrix Transformation { get; set; }
        public int TileIndex;

        public Tile(float x, float y, Tileset tileset, int index)
        {
            Transformation = Matrix.CreateTranslation(x * 2, y * 2, 0f);
            TileIndex = index;
            FillVerticies(tileset);
        }

        private void FillVerticies(Tileset tileset)
        {
            Vertices = new VertexPositionColorTexture[4];
            Vertices[0] = new VertexPositionColorTexture(
                new Vector3(0f, 0f, 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].LowerLeft);
            Vertices[1] = new VertexPositionColorTexture(
                new Vector3(0f, 2f, 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].UpperLeft);
            Vertices[2] = new VertexPositionColorTexture(
                new Vector3(2f, 2f, 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].UpperRight);
            Vertices[3] = new VertexPositionColorTexture(
                new Vector3(2f, 0f, 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].LowerRight);

            Indices = new int[6];

            Indices[0] = 0;
            Indices[1] = 1;
            Indices[2] = 2;
            Indices[3] = 0;
            Indices[4] = 3;
            Indices[5] = 2;
        }
    }
}
