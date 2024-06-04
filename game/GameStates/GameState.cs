using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseProject.GameObjects;
using Blok3Game.Engine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blok3Game.GameStates
{
    public class GameState : GameObjectList
    {
        // public const bool DEBUG = true;
        public const DebugLevel DEBUG_LEVEL = DebugLevel.INFO;
        
        private TimeSpan now;
        private Queue<TimeSpan> TimeSpanQueue = new();
        private TimeSpan updateText;
        
        private World world;
        private TextGameObject fps;
        
        public GameState()
        {
            world = new World();
            Add(world);

            #region DEBUG
            if (DEBUG_LEVEL == DebugLevel.NONE) return;
            fps = new TextGameObject("Fonts/SpriteFont");
            fps.Text = "FPS";
            Add(fps);
            #endregion
        }
        
        public override void Update(GameTime gameTime)
        {
            // Update the world
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            
            #region DEBUG
            if (DEBUG_LEVEL == DebugLevel.NONE) return;
            
            // Calculate time between frames
            TimeSpanQueue.Enqueue(gameTime.TotalGameTime - now);
            now = gameTime.TotalGameTime;
            
            if (TimeSpanQueue.Count > 100)
            {
                TimeSpanQueue.Dequeue();
            }
            
            // Get average time between frames
            TimeSpan average = new TimeSpan();
            foreach (TimeSpan timeSpan in TimeSpanQueue)
            {
                average += timeSpan;
            }
            average /= TimeSpanQueue.Count;
            
            // Update text every second
            if (gameTime.TotalGameTime - updateText > TimeSpan.FromSeconds(1))
            {
                updateText = gameTime.TotalGameTime;
                fps.Text = $"FPS: {1000 / average.Milliseconds}";
            }
            #endregion
        }
        
        public enum DebugLevel
        {
            NONE,
            INFO,
            DEBUG
        }
    }
}