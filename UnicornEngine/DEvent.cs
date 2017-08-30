using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnicornEngine
{
    public class DEvent
    {
        public float lifeTime;
        float? life;
        public bool died = false;
        public Action onFinish;

        public DEvent(float lifeTime, Action onFinish)
        {
            this.lifeTime = lifeTime;
            life = lifeTime;
            this.onFinish = onFinish;
        }

        public void Update()
        {
            if (life > 0)
                life -= EngineCore.gameTime.ElapsedGameTime.Milliseconds / 1000f;
            else if (life != null)
            {
                life = 0;
                died = true;
                onFinish();
            }
        }
    }
}
