using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UnicornEngine
{
    public abstract class UnicornGame : Microsoft.Xna.Framework.Game
    {
        protected bool debugMode = false;

        public UnicornGame()
        {
            EngineCore.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            EngineCore.Update(gameTime);
            if (EngineCore.FullScreen != EngineCore.graphics.IsFullScreen)
            {
                if (!EngineCore.graphics.IsFullScreen)
                {
                    EngineCore.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    EngineCore.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                }
                else
                {
                    EngineCore.graphics.PreferredBackBufferWidth = 800;
                    EngineCore.graphics.PreferredBackBufferHeight = 480;
                }
                EngineCore.graphics.ToggleFullScreen();
                EngineCore.graphics.ApplyChanges();
            }
        }
    }
}
