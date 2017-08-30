using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UnicornEngine
{
    public class Sprite: Object2D
    {
        public Texture2D texture;
        private Color[] collisionMap;
        private Point mapSize = Point.Zero;
        public bool visible = true;
        public Vector2 frameSize;
        public int[] animationsLengths;
        private int _animation;
        public int animation
        {
            get { return _animation; }
            set { if (_animation != value) _frame = 0; _animation = value; }
        }
        private float _frame;
        public float frame
        {
            get { return _frame; }
            set
            {
                if (animationLooped) _frame = value % animationsLengths[animation];
                else _frame = value < 0 ? 0 : value > animationsLengths[animation] - 1 ? animationsLengths[animation] - 1 : value;
            }
        }
        public float speedAnimation = 1f;
        public bool animationLooped = false;
        float _rotation = 0;
        public float rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value % 360;
            }
        }

        public float _scale;
        public float scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                base.size.Y = _scale * frameSize.Y / frameSize.X;
                base.size.X = _scale;
            }
        }
        public Color color = Color.White;

        protected Rectangle frameRect
        {
            get { return new Rectangle((int)((int)frame * frameSize.X), (int)(animation * frameSize.Y), (int)frameSize.X, (int)frameSize.Y); }
        }

        public void SetCollisionMap(string collisionMap)
        {
            Texture2D Map = EngineCore.content.Load<Texture2D>("Sprites/" + collisionMap);
            this.collisionMap = new Color[Map.Width * Map.Height];
            mapSize = new Point(Map.Width, Map.Height);
            Map.GetData<Color>(this.collisionMap);//заполним массив цветов
        }

        protected override bool UnderMouse(Vector2 cursorPos)
        {
            if (base.UnderMouse(cursorPos))
            {
                if (mapSize != Point.Zero)
                {
                    Vector2 relativeCursorPos = cursorPos - absolutePos;
                    Point texturePos = new Point((int)(relativeCursorPos.X * mapSize.X / size.X), 
                                                 (int)(relativeCursorPos.Y * mapSize.Y / size.Y));
                    if (texturePos.X < 0)             texturePos.X = 0;
                    if (texturePos.X > mapSize.X - 1) texturePos.X = mapSize.X - 1;
                    if (texturePos.Y < 0)             texturePos.Y = 0;
                    if (texturePos.Y > mapSize.Y - 1) texturePos.Y = mapSize.Y - 1;
                    return collisionMap[texturePos.Y * mapSize.X + texturePos.X].R > 0xFF / 2;
                }
                else
                    return true;
            }
            return false;
        }

        public void ChangeTexture(string fileName)
        {
            if (fileName != null && fileName != "")
                this.texture = EngineCore.content.Load<Texture2D>("Sprites/" + fileName);
        }

        public Sprite(Object2D parent, Texture2D texture, int[] animationsLengths, float scale, Vector2 pos)
            : base(parent, new Vector2(scale, scale), pos)
        {
            this.texture = texture;
            if (texture != null)
            {
                frameSize = new Vector2(this.texture.Width / animationsLengths.Max(), this.texture.Height / animationsLengths.Length);
            }
            this.animationsLengths = animationsLengths;
            this.scale = scale;
            base.size.Y = scale * this.frameSize.Y / this.frameSize.X;
            base.size.X = scale;
        }

        public Sprite(Object2D parent, string texture, int[] animationsLengths, float scale, Vector2 pos)
            :this(parent, EngineCore.content.Load<Texture2D>("Sprites/" + texture), animationsLengths, scale, pos) { }

        public Sprite(Object2D parent, Texture2D texture, float size, Vector2 pos)
            :this(parent, texture, new int[] { 1 }, size, pos) { }

        public Sprite(Object2D parent, string texture, float size, Vector2 pos)
            : this(parent, texture, new int[] { 1 }, size, pos) { }

        public override void Draw()
        {
            frame += speedAnimation * (float)EngineCore.gameTime.ElapsedGameTime.TotalMilliseconds / 200;
            if (texture != null && visible)
            {
                //if (EngineCore.currentEffect == null)
                    EngineCore.spriteBatch.Draw(texture, posRect, frameRect, color, (float)(Math.PI * rotation / 180.0), Vector2.Zero, SpriteEffects.None, 0);
                /*else
                    EngineCore.spriteBatch.Draw(texture, posRect, frameRect, color, (float)(Math.PI * rotation / 180.0), Vector2.Zero, EngineCore.currentEffect, 0);*/
                base.Draw();
            }
        }
    }
}
