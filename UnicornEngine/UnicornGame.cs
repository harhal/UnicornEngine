using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnicornEngine
{
    public class UnicornGame : Microsoft.Xna.Framework.Game
    {
        public Point[] resolutionList;
        public bool debugMode = false;

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
        {
            resolutionList = EngineCore.RefreshResolutionList();
            EngineCore.Initialize(new SpriteBatch(GraphicsDevice), Content);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            EngineCore.Update(gameTime);
            if (EngineCore.currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F) && debugMode)
            {
                EngineCore.graphics.ToggleFullScreen();
                if (EngineCore.graphics.IsFullScreen)
                {
                    EngineCore.graphics.PreferredBackBufferWidth = resolutionList[resolutionList.Length - 1].X;
                    EngineCore.graphics.PreferredBackBufferHeight = resolutionList[resolutionList.Length - 1].Y;
                }
                else
                {
                    EngineCore.graphics.PreferredBackBufferWidth = 800;
                    EngineCore.graphics.PreferredBackBufferHeight = 480;
                }
                EngineCore.graphics.ApplyChanges();

            }
        }
    }
}
