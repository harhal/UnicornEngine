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
        public Action onFinish;

        public EffectSprite(Object2D parent, string texture, int animationsLengths, float scale, Vector2 pos, int lifeTime, Action onFinish) :
            base(parent, texture, new int[1]{ animationsLengths }, scale, pos)
        {
            this.lifeTime = lifeTime;
            life = lifeTime;
            this.onFinish = onFinish;
        }

        public override void Update()
        {
            if (life > 0)
                life -= EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
            else if (life != null)
            {
                life = 0;
                died = true;
                onFinish();
            }
            base.Update();
        }
    }
}