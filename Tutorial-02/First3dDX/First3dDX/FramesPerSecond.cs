using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Psilibrary
{
    public class FramesPerSecond : DrawableGameComponent
    {
        private float _fps;
        private readonly float _updateInterval = 1.0f;
        private float _timeSinceLastUpdate = 0.0f;
        private float _frameCount = 0;
        private float _totalSeconds;
        private float _afps;
        private float _totalFrames;
        private float _maxFps;
        private float _minFps = float.MaxValue;

        public FramesPerSecond(Game game)
            : this(game, false, false, game.TargetElapsedTime)
        {
        }

        public FramesPerSecond(Game game, 
            bool synchWithVerticalRetrace, 
            bool isFixedTimeStep, 
            TimeSpan targetElapsedTime)
            : base(game)
        {
            GraphicsDeviceManager graphics = 
                (GraphicsDeviceManager)Game.Services.GetService(
                    typeof(IGraphicsDeviceManager));

            graphics.SynchronizeWithVerticalRetrace = 
                synchWithVerticalRetrace;
            Game.IsFixedTimeStep = isFixedTimeStep;
            Game.TargetElapsedTime = targetElapsedTime;
        }

        public sealed override void Initialize()
        {
            base.Initialize();
        }

        public sealed override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.F1))
            {
                _fps = 0;
                _afps = 0;
                _timeSinceLastUpdate = 0;
                _totalFrames = 0;
                _frameCount = 0;
                _totalSeconds = 0;
                _minFps = float.MaxValue;
                _maxFps = 0;
            }

            base.Update(gameTime);
        }

        public sealed override void Draw(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCount++;
            _timeSinceLastUpdate += elapsed;
            _totalFrames++;

            if (_timeSinceLastUpdate > _updateInterval)
            {
                _totalSeconds++;
                _fps = _frameCount / _timeSinceLastUpdate;
                
                if (_fps < _minFps)
                {
                    _minFps = _fps;
                }

                if (_fps > _maxFps)
                {
                    _maxFps = _fps;
                }

                _afps = _totalFrames / _totalSeconds;

                System.Diagnostics.Debug.WriteLine($"FPS: {_fps:N4}" +
                    $" - AFPS: {_afps:N4} " +
                    $"- MIN FPS: {_minFps:N4} " +
                    $"- MAX FPS: {_maxFps:N4}");

#if !ANDROID
                Game.Window.Title = "FPS: " + _fps.ToString("N4");
#endif

                _frameCount = 0;
                _timeSinceLastUpdate -= _updateInterval;

#if !ANDROID
                Game.Window.Title += $" - AFPS: {_afps:N4} - " + 
                    $"MIN FPS: {_minFps:N4} - MAX FPS: {_maxFps:N4}";
#endif
            }

            base.Draw(gameTime);
        }
    }
}
