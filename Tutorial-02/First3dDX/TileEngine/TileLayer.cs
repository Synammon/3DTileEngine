using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace First3dDX
{
    public class TileLayer
    {
        public Dictionary<Point, Tile> Tiles { get; private set; }
        public Tileset Tileset { get; private set; }

        public TileLayer(Tileset tileset)
        {
            Tiles = new Dictionary<Point, Tile>();
            Tileset = tileset;
        }

        public void Draw(GraphicsDevice device, BasicEffect effect, Camera camera)
        {
            foreach (Point p in Tiles.Keys)
            {
                effect.World = camera.Transformation * Tiles[p].Transformation;

                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                        PrimitiveType.TriangleList,
                        Tiles[p].Vertices,
                        0,
                        4,
                        Tiles[p].Indices,
                        0,
                        2);
                }
            }
        }
        public void Draw(GraphicsDevice device, Effect effect, Camera camera)
        {
            foreach (Point p in Tiles.Keys)
            {
                effect.Parameters["World"].SetValue(Tiles[p].Transformation * camera.Transformation);

                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                        PrimitiveType.TriangleList,
                        Tiles[p].Vertices,
                        0,
                        4,
                        Tiles[p].Indices,
                        0,
                        2);
                }
            }
        }
    }
}
