using First3dDX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary;
using System;
using System.Collections.Generic;

namespace First3D
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Tileset _tileset;
        private readonly TileMap map = new TileMap(100, 100);
        private BasicEffect _basicEffect;
        private Effect _effect;
        private readonly Camera _camera = new Camera();
        private Matrix _world = Matrix.CreateTranslation(0, 0, 0);
        private Matrix _view = Matrix.CreateLookAt(new Vector3(0, 0, 32), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        private Matrix _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1920f / 1080f, 0.01f, 100f);
        //private Matrix _projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, 0, 1)
        Random Random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Components.Add(new FramesPerSecond(this));

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileset = new Tileset(Content.Load<Texture2D>("tiny-16"), 16, 16);
            _effect = Content.Load<Effect>("Effect");

            SetUpEffect();

            _basicEffect = new BasicEffect(GraphicsDevice)
            {
                World = _world,
                View = _view,
                Projection = _projection,
                VertexColorEnabled = true,
                TextureEnabled = true,
                Texture = _tileset.Texture
            };

            int tiles = 100;
            TileLayer layer = new TileLayer(GraphicsDevice, _tileset, tiles * 2, tiles * 2);
            for (int y = tiles; y >= -tiles; y--)
            {
                for (int x = -tiles; x < tiles; x++)
                {
                    Tile t = new Tile(x, y, _tileset, Random.Next(10));
                    layer.Tiles.Add(new Point(x, y), t);
                }
            }

            layer.SetBuffers(GraphicsDevice);

            map.Layers.Add(layer);
        }

        private void SetUpEffect()
        {
            _effect.CurrentTechnique = _effect.Techniques["QuadDraw"];//["BasicDrawing"];

            _effect.Parameters["World"].SetValue(_world);
            _effect.Parameters["View"].SetValue(_view);
            _effect.Parameters["Projection"].SetValue(_projection);
            _effect.Parameters["TextureA"].SetValue(_tileset.Texture);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.W))
            {
                _camera.Position.Y += 0.25f;
            }

            if (ks.IsKeyDown(Keys.S))
            {
                _camera.Position.Y -= 0.25f;
            }

            if (ks.IsKeyDown(Keys.A))
            {
                _camera.Position.X -= 0.25f;
            }

            if (ks.IsKeyDown(Keys.D))
            {
                _camera.Position.X += 0.25f;
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                _camera.Scale += 0.01f;
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                _camera.Scale -= 0.01f;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                _camera.Rotation -= 0.01f;
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                _camera.Rotation += 0.01f;
            }

            _camera.Lock(map, _view, _projection);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            //map.Draw(GraphicsDevice, _basicEffect, _camera);
            map.Draw(GraphicsDevice, _effect, _camera);

            base.Draw(gameTime);
        }
    }
}
