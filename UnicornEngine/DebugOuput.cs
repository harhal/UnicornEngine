using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace UnicornEngine
{
    public class DebugOuput
    {
        class GraphicRect : Object2D
        {
            Texture2D pixel;

            public GraphicRect() : base(null, Vector2.Zero, Vector2.Zero)
            {
                pixel = EngineCore.content.Load<Texture2D>("Pixel");
            }

            public override void Draw()
            {
                EngineCore.spriteBatch.Draw(pixel, posRect, new Color(1, 0.5f, 0.5f, 0.6f));
            }
        }

        SpriteFont font;
        GraphicRect choose;
        GraphicRect vline;
        GraphicRect hline;
        static string otherInfo = "";
        string text;

        public DebugOuput()
        {
            font = EngineCore.content.Load<SpriteFont>("DebugFont");
            choose = new GraphicRect();
            choose.size = Vector2.Zero;
            vline = new GraphicRect();
            vline.pos.Y = -10;
            vline.size.X = EngineCore.LengthToHundred(1);
            hline = new GraphicRect();
            hline.size.Y = EngineCore.LengthToHundred(1);
        }

        public void Update()
        {
            vline.size.Y = EngineCore.HundredToLength(100);
            vline.pos.X = EngineCore.GetCurrentCursorPos().X;
            hline.size.X = EngineCore.HundredToLength(100);
            hline.pos.Y = EngineCore.GetCurrentCursorPos().Y;

            if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed && EngineCore.oldMouseState.LeftButton == ButtonState.Released)
                choose.pos = EngineCore.GetCurrentCursorPos();
            if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed)
                choose.size +=  EngineCore.GetCurrentCursorPos() - EngineCore.GetOldCursorPos();
            else
                choose.size = Vector2.Zero;
            text = ("Cursor: {" + EngineCore.GetCurrentCursorPos().X + ", " + EngineCore.GetCurrentCursorPos().Y + "}\n" + otherInfo + 
                ((choose.size.Length() > 1) ? ("Size: {" + choose.size.X.ToString() + ", " + choose.size.Y.ToString() + "}\n") : ""));
        }

        public void Draw()
        {
            choose.Draw();
            vline.Draw();
            hline.Draw();
            EngineCore.spriteBatch.DrawString(font, text, 
                new Vector2(EngineCore.RenderedRectangle.Location.X, EngineCore.RenderedRectangle.Location.Y), 
                Color.Black);
        }
    }
}
