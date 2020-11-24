using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace First3dDX
{
    public class Tile
    {
        private static int index = 0;
        public VertexPositionColorTexture[] Vertices;
        public int[] Indices;

        private static List<VertexPositionColorTexture> _vertices = new List<VertexPositionColorTexture>();
        private static List<int> _indices = new List<int>();

        public static VertexPositionColorTexture[] VertexData { get { return _vertices.ToArray(); } }
        public static int[] IndexData { get { return _indices.ToArray(); } }

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
                new Vector3(0f + x * 3 , 0f + y * 3 , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].LowerLeft);
            Vertices[1] = new VertexPositionColorTexture(
                new Vector3(0f + x * 3 , 3f + y * 3 , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].UpperLeft);
            Vertices[2] = new VertexPositionColorTexture(
                new Vector3(3f + x * 3 , 3f + y * 3 , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].UpperRight);
            Vertices[3] = new VertexPositionColorTexture(
                new Vector3(3f + x * 3 , 0f + y * 3 , 0f), 
                Color.White, 
                tileset.Mappings[TileIndex].LowerRight);
            Indices = new int[6];

            Indices[0] = index;
            Indices[1] = (int)(index + 1);
            Indices[2] = (int)(index + 2);
            Indices[3] = (int)(index + 2);
            Indices[4] = (int)(index + 3);
            Indices[5] = index;

            index += 4;

            _vertices.AddRange(Vertices.ToList());
            _indices.AddRange(Indices.ToList());
        }
    }
}
