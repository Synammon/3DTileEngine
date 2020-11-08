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
        private readonly List<Tile> _tiles = new List<Tile>();
        private BasicEffect _basicEffect;
        private readonly Camera _camera = new Camera();
        private Matrix _world = Matrix.CreateTranslation(0, 0, 0);
        private Matrix _view = Matrix.CreateLookAt(new Vector3(0, 0, 32), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        private Matrix _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1920f / 1080f, 0.01f, 100f);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

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

            _basicEffect = new BasicEffect(GraphicsDevice);

            for (int y = -50; y < 50; y++)
            {
                for (int x = -50; x < 50; x++)
                {
                    Tile t = new Tile(x, y, _tileset, 0);
                    _tiles.Add(t);
                }
            }
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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _basicEffect.World = _world;
            _basicEffect.View = _view;
            _basicEffect.Projection = _projection;
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.TextureEnabled = true;
            _basicEffect.Texture = _tileset.Texture;

            RasterizerState rasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };

            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            foreach (Tile t in _tiles)
            {
                _basicEffect.World = t.Transformation * _camera.Transformation;

                foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                        PrimitiveType.TriangleList,
                        t.Vertices,
                        0,
                        4,
                        t.Indices,
                        0, 
                        2);
                }
            }

            base.Draw(gameTime);
        }
    }
}
