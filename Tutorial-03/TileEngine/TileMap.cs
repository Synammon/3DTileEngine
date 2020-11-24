using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace First3dDX
{
    public class TileMap
    {
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public List<TileLayer> Layers { get; private set; }
        
        public TileMap(int mapWidth, int mapHeight)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;

            Layers = new List<TileLayer>();
        }

        public void Draw(GraphicsDevice device, BasicEffect effect, Camera camera)
        {
            foreach (TileLayer layer in Layers)
            {
                layer.Draw(device, effect, camera);
            }
        }

        public void Draw(GraphicsDevice device, Effect effect, Camera camera)
        {
            foreach (TileLayer layer in Layers)
            {
                layer.Draw(device, effect, camera);
            }
        }
    }
}
