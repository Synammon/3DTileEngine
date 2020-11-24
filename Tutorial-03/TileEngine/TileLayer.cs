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

        private VertexBuffer _vertextBuffer;
        private IndexBuffer _indexBuffer;

        public TileLayer(GraphicsDevice device, Tileset tileset, int width, int height)
        {
            Tiles = new Dictionary<Point, Tile>();
            Tileset = tileset;

            _vertextBuffer = new VertexBuffer(device, typeof(VertexPositionColorTexture), width * (height + 1) * 4, BufferUsage.WriteOnly);
            _indexBuffer = new IndexBuffer(device, typeof(int), width * height * 6, BufferUsage.WriteOnly);
        }

        public void SetBuffers(GraphicsDevice device)
        {
            _vertextBuffer.SetData<VertexPositionColorTexture>(Tile.VertexData);
            _indexBuffer.SetData(Tile.IndexData);

            device.SetVertexBuffer(_vertextBuffer);
            device.Indices = _indexBuffer;
        }

        public void Draw(GraphicsDevice device, BasicEffect effect, Camera camera)
        {
            effect.World = camera.Transformation;

            device.SetVertexBuffer(_vertextBuffer);
            device.Indices = _indexBuffer;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Tiles.Count * 2);
            }
        }
        public void Draw(GraphicsDevice device, Effect effect, Camera camera)
        {
            effect.Parameters["World"].SetValue(camera.Transformation);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Tiles.Count * 2);
            }
        }
    }
}
