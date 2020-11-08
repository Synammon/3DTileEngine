using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace First3dDX
{
    public struct TextureMapping
    {
        public Vector2 UpperLeft;
        public Vector2 UpperRight;
        public Vector2 LowerLeft;
        public Vector2 LowerRight;
    }

    public class Tileset
    {
        public Texture2D Texture { get; private set; }
        public List<TextureMapping> Mappings { get; private set; } = new List<TextureMapping>();
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public Tileset(Texture2D texture, int tileWidth, int tileHeight)
        {
            Texture = texture;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            float ratioX = (float)tileWidth / texture.Width;
            float ratioY = (float)tileHeight / texture.Height;

            for (int y = 0; y < texture.Height / tileHeight; y++)
            {
                for (int x = 0; x < texture.Width / tileWidth; x++)
                {
                    TextureMapping tm = new TextureMapping
                    {
                        UpperLeft = new Vector2((float)x * ratioX, (float)y * ratioY)
                    };
                    tm.UpperRight = tm.UpperLeft + new Vector2(ratioX, 0f);
                    tm.LowerLeft = tm.UpperLeft + new Vector2(0f, ratioY);
                    tm.LowerRight = tm.UpperLeft + new Vector2(ratioX, ratioY);

                    Mappings.Add(tm);
                }
            }
        }
    }
}
