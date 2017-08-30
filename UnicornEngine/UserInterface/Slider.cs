using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnicornEngine
{
    public class Slider:Sprite
    {
        Sprite slider;
        float _value = 0;
        public new float value
        {
            get { return _value; }
            set
            {
                _value = value < 100 ? value > 0 ? value : 0 : 100;
            }
        }
        Action onMove;
        bool pressed = false;

        public Slider(Object2D parent, string backTexture, string sliderTexture, float size, Vector2 pos, float value, Action onMove) :
            base(parent, backTexture, size, pos)
        {
            this.value = value;
            slider = new Sprite(this, sliderTexture, base.size.Y * 0.9f, Vector2.Zero);
            slider.pos.X = (scale - slider.size.X) * (this.value / 100);
            slider.pos.Y = base.size.Y * 0.2f;
            elements.Add(slider);
            this.onMove = onMove;
        }

        public override void Update()
        {
            base.Update();
            if (UnderMouse() && 
                EngineCore.currentMouseState.LeftButton == ButtonState.Pressed && 
                EngineCore.oldMouseState.    LeftButton == ButtonState.Released)
            {
                pressed = true;
            }
            if (pressed)
            {
                slider.pos.X = EngineCore.GetCurrentCursorPos().X - absolutePos.X - slider.scale / 2;
                if (slider.pos.X < 0)
                    slider.pos.X = 0;
                if (slider.pos.X > scale - slider.size.X)
                    slider.pos.X = scale - slider.size.X;
                value = slider.pos.X / (scale - slider.size.X) * 100;
                onMove.Invoke();
            }
            if (EngineCore.currentMouseState.LeftButton == ButtonState.Released &&
                EngineCore.oldMouseState.LeftButton == ButtonState.Pressed)
            {
                pressed = false;
            }
        }
    }
}
