using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnicornEngine
{
    public abstract class Scene
    {
        DebugOuput debug;
        public bool DebugMode = false;
        public Color colorBG = Color.Black;
        public Action<int> exit;
        public Effect effect = null;

        public Object2D Draggable;
        public Object2D Canvas;
        protected Object2D Effects;
        protected List<DEvent> dEvents;

        public void AddEffect(EffectSprite effect)
        {
            Effects.elements.Add(effect);
        }

        public void CleareEffects()
        {
            Effects.elements.Clear();
        }

        public void AddEvent(DEvent dEvent)
        {
            dEvents.Add(dEvent);
        }

        public void AddEvent(float lifeTime, Action onFinish)
        {
            dEvents.Add(new DEvent(lifeTime, onFinish));
        }

        public void CleareEvents()
        {
            dEvents.Clear();
        }

        public Scene(Action<int> exit)
        {
            this.exit = exit;
            Canvas = new Object2D(null, Vector2.Zero, Vector2.Zero);
            Effects = new Object2D(null, Vector2.Zero, Vector2.Zero);
            dEvents = new List<DEvent>();
            debug = new DebugOuput();
        }

        public abstract Scene GetReloadedScene();

        public virtual void Update()
        {
            if (DebugMode)
                debug.Update();
            if (Draggable != null)
                Draggable.absolutePos = EngineCore.GetCurrentCursorPos() - Draggable.size / 2;
            Canvas.Update();
            Effects.Update();
            Effects.elements.RemoveAll(delegate (Object2D a) { return ((EffectSprite)a).died; });
            int startCount = dEvents.Count;
            for (int i = 0; i < startCount; i++)
                dEvents[i].Update();
            dEvents.RemoveAll(delegate (DEvent a) { return ((DEvent)a).died; });
        }

        public virtual void Draw()
        {
            EngineCore.graphics.GraphicsDevice.Clear(Color.Black);
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            if (effect != null)
                EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS, effect);
            else
                EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            EngineCore.graphics.GraphicsDevice.Clear(colorBG);
            Canvas.Draw();
            Effects.Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
            EngineCore.spriteBatch.Begin();
            if (DebugMode)
                debug.Draw();
            EngineCore.spriteBatch.End();
        }
    }
}
