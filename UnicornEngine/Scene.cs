using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnicornEngine
{
    public class Scene
    {
        DebugOuput debug;
        public bool DebugMode = false;
        Sprite menuPanel;
        public Color colorBG = Color.Black;

        public Object2D Draggable;
        public Object2D Canvas;
        protected Object2D Effects;
        //Object2D Menu;

        public void AddEffect(EffectSprite effect)
        {
            Effects.elements.Add(effect);
        }

        public Scene()
        {
            menuPanel = new Sprite(null, "MenuPanel", 25, new Vector2(74, 1));
            menuPanel.elements.Add(new Button(menuPanel, "HelpButton", 5, new Vector2(1, 0.8f), new int[] { 1, 1, 1 }, "Help", delegate { }));
            menuPanel.elements.Add(new Button(menuPanel, "MenuButton", 5, new Vector2(7, 0.8f), new int[] { 1, 1, 1 }, "Menu", delegate { }));
            menuPanel.elements.Add(new Button(menuPanel, "SettingButton", 5, new Vector2(13, 0.8f), new int[] { 1, 1, 1 }, "Setting", delegate { }));
            menuPanel.elements.Add(new Button(menuPanel, "SoundOn", 5, new Vector2(19, 0.8f), new int[] { 1, 1, 1 }, "Sound", delegate { }));
            Canvas = new Object2D(null, Vector2.Zero, Vector2.Zero);
            Effects = new Object2D(null, Vector2.Zero, Vector2.Zero);
            debug = new DebugOuput();
        }

        public virtual void Update()
        {
            if (DebugMode)
                debug.Update();
            if (Draggable != null)
                Draggable.absolutePos = EngineCore.GetCurrentCursorPos() - Draggable.size / 2;
            menuPanel.Update();
            Canvas.Update();
            Effects.Update();
            Effects.elements.RemoveAll(delegate(Object2D a) { return ((EffectSprite)a).died; });
        }

        public virtual void Draw()
        {
            EngineCore.graphics.GraphicsDevice.Clear(colorBG);
            Rectangle ORR = EngineCore.graphics.GraphicsDevice.ScissorRectangle;
            RasterizerState RS = new RasterizerState();
            RS.ScissorTestEnable = true;
            EngineCore.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RS);
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = EngineCore.RenderedRectangle;
            Canvas.Draw();
            Effects.Draw();
            EngineCore.spriteBatch.End();
            EngineCore.graphics.GraphicsDevice.ScissorRectangle = ORR;
            EngineCore.spriteBatch.Begin();
            if (DebugMode)
                debug.Draw();
            menuPanel.Draw();
            EngineCore.spriteBatch.End();
        }
    }
}
