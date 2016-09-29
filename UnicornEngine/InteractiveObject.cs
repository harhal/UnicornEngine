using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UnicornEngine
{
    public class InteractiveObject :Sprite
    {
        public Action OnPress;

        public InteractiveObject(Sprite parent, string texture, int[] animationsLengths, float size, Vector2 pos, Action OnPress)
            :base(parent, texture, new int[] { 1, 1, 1 }, size, pos)
        {
            if (texture != null && animationsLengths.Length > 0)
            {
                this.frameSize = new Vector2(this.texture.Width, this.texture.Height / animationsLengths.Length);
            }
            this.scale = scale;
            this.OnPress = OnPress;
        }

        public override void Update()
        {
            if (UnderMouse())
                if (EngineCore.currentMouseState.LeftButton == ButtonState.Pressed)
                    base.animation = 2;
                else
                {
                    if (EngineCore.oldMouseState.LeftButton == ButtonState.Pressed) OnPress.Invoke();
                    base.animation = 1;
                }
            else
                base.animation = 0;
        }
    }
}
