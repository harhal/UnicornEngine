using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnicornEngine
{
    public class Switcher:Sprite
    {
        Sprite switcher;
        public new bool value = false;
        Action onMove;

        public Switcher(Object2D parent, string backTexture, string switcherTexture, float size, Vector2 pos, bool value, Action onMove) :
            base(parent, backTexture, size, pos)
        {
            this.value = value;
            switcher = new Sprite(this, switcherTexture, base.size.Y * 0.95f, Vector2.Zero);
            if (value)
                switcher.pos.X = 0;
            else
                switcher.pos.X = scale - switcher.size.X;
            switcher.pos.Y = base.size.Y * -0.2f;
            elements.Add(switcher);
            this.onMove = onMove;
        }

        public override void Update()
        {
            base.Update();
            if (UnderMouse() &&
                EngineCore.currentMouseState.LeftButton == ButtonState.Pressed &&
                EngineCore.oldMouseState.LeftButton == ButtonState.Released)
            {
                value = !value;
                if (value)
                    switcher.pos.X = 0;
                else
                    switcher.pos.X = scale - switcher.size.X;
                onMove.Invoke();
            }
        }
    }
}
