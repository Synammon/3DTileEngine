using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Psilibrary
{
    public class FramesPerSecond : DrawableGameComponent //could you explain DrawableGameComponent?
    {
        private float _fps;
        private float _updateInterval = 1.0f; //is this the standard or you're changing it?
        private float _timeSinceLastUpdate = 0.0f;
        private float _frameCount = 0;

        public FramesPerSecond(Game game)
            : this(game, false, false, game.TargetElapsedTime)
        {
        }

        public FramesPerSecond(Game game, bool synchWithVerticalRetrace, bool isFixedTimeStep, TimeSpan targetElapsedTime)
            : base(game)
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));

            graphics.SynchronizeWithVerticalRetrace = synchWithVerticalRetrace;
            Game.IsFixedTimeStep = isFixedTimeStep;
            Game.TargetElapsedTime = targetElapsedTime;
        }

        public sealed override void Initialize()
        {
            base.Initialize();
        }

        public sealed override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public sealed override void Draw(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _frameCount++;
            _timeSinceLastUpdate += elapsed;

            if (_timeSinceLastUpdate > _updateInterval)
            {
                _fps = _frameCount / _timeSinceLastUpdate;
                System.Diagnostics.Debug.WriteLine("FPS: " + _fps.ToString());
#if !ANDROID
                Game.Window.Title = "FPS: " + ((int)_fps).ToString();
#endif
                _frameCount = 0;
                _timeSinceLastUpdate -= _updateInterval;
            }

            base.Draw(gameTime);
        }
    }
}
