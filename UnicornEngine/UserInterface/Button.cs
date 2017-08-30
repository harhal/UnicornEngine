using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UnicornEngine
{
    public class Button :Sprite
    {
        public Action OnPress;

        public Button(Object2D parent, string texture, float scale, Vector2 pos, int[] AnimationLengths, string text, Action OnPress)
            :base(parent, texture, AnimationLengths, scale, pos)
        {
            this.scale = scale;
            name = text;
            this.OnPress = OnPress;
        }

        public override void Update()
        {
            if (UnderMouse())
                if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (base.frame >= animationsLengths[animation] - 1)
                        base.animation = 2;
                }
                else
                {
                    if (EngineCore.oldMouseState.LeftButton == ButtonState.Pressed && OnPress != null) OnPress.Invoke();
                    if (base.frame >= animationsLengths[animation] - 1)
                        base.animation = 1;
                }
            else
                if (base.frame >= animationsLengths[animation] - 1)
                    base.animation = 0;
            if (animationsLengths.Length > 3)
            {
                if (base.animation == 0 && base.frame >= animationsLengths[animation] - 1 && (new Random()).Next(1000) < 1)
                    animation = 2 + (new Random()).Next(animationsLengths.Length - 3);
                if (base.animation > 2 && base.frame > animationsLengths[animation] - 1)
                    base.animation = 0;
            }
            base.Update();
        }
    }
}
