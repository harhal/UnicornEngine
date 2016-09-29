using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
  /*   public enum TextAlignment {   TopLeft,    TopCenter,    TopRight,
                                      Left,       Center,       Right,
                                BottomLeft, BottomCenter, BottomRight};

    class TextSprite
    {
        public SpriteFont font;
        public override bool UnderMouse()
        {

            return posRect.Contains(EngineCore.currentMouseState.X + (int)parent.pos.X,
                                    EngineCore.currentMouseState.Y + (int)parent.pos.Y) && active;
        }
        public string text;
        public Color color;
        public TextAlignment textAlignment;

        public TextSprite(Sprite parent, SpriteFont font, string text, Vector2 pos, Color color, TextAlignment textAlignment)
            :base(parent, EngineCore.LengthToHundred(font.MeasureString(text)), pos)
        {
            this.parent = parent;
            this.font = font;
            this.text = text;
            this.pos = pos;
            this.color = color;
            this.textAlignment = textAlignment;
        }

        public override void Draw()
        {
            size = EngineCore.LengthToHundred(font.MeasureString(text));
            Vector2 offset = Vector2.Zero;
            switch (textAlignment)
            {
                case (TextAlignment.TopLeft):       offset = new Vector2(-size.X    , -size.Y    ); break;
                case (TextAlignment.TopCenter):     offset = new Vector2(-size.X / 2, -size.Y    ); break;
                case (TextAlignment.TopRight):      offset = new Vector2(      0    , -size.Y    ); break; 
                case (TextAlignment.Left):          offset = new Vector2(-size.X    , -size.Y / 2); break;
                case (TextAlignment.Center):        offset = new Vector2(-size.X / 2, -size.Y / 2); break;
                case (TextAlignment.Right):         offset = new Vector2(      0    , -size.Y / 2); break;
                case (TextAlignment.BottomLeft):    offset = new Vector2(-size.X    ,       0    ); break;
                case (TextAlignment.BottomCenter):  offset = new Vector2(-size.X / 2,       0    ); break;
                case (TextAlignment.BottomRight):   offset = new Vector2(      0    ,       0    ); break;
            } 
            //offset += new Vector2(parent.pos.X, parent.pos.Y);
            EngineCore.spriteBatch.DrawString(font, text, EngineCore.HundredToLength(offset) + new Vector2(posRect.Location.X, posRect.Location.Y), color);
            base.Draw();
        }
    }*/
}
