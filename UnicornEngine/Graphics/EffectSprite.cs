using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnicornEngine
{
    public class EffectSprite:Sprite
    {
        public float lifeTime;
        float? life;
        public bool died = false;
        public Func<float, float, Vector2> move;

        public EffectSprite(Object2D parent, string texture, int animationsLengths, float scale, Vector2 pos, float lifeTime) :
            base(parent, texture, new int[1]{ animationsLengths }, scale, pos)
        {
            this.lifeTime = lifeTime;
            life = lifeTime;
        }

        public EffectSprite(Object2D parent, Texture2D texture, int animationsLengths, float scale, Vector2 pos, float lifeTime) :
            base(parent, texture, new int[1] { animationsLengths }, scale, pos)
        {
            this.lifeTime = lifeTime;
            life = lifeTime;
        }

        public override void Update()
        {
            if (life > 0)
            {
                life -= EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
                if (move != null)
                    pos += move(life.Value, EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            else if (life != null)
            {
                life = 0;
                died = true;
            }
            base.Update();
        }
    }
}