using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnicornEngine
{
    public class Object2D : ICloneable
    {
        public Object2D parent;
        public string name;
        public List<Object2D> elements;

        public bool active = true;
        public Vector2 size;
        public Vector2 pos;

        public Vector2 absolutePos
        {
            get
            {
                Vector2 offset = Vector2.Zero;
                if (parent != null) offset = parent.absolutePos;
                return new Vector2(pos.X + offset.X, pos.Y + offset.Y);
            }
            set
            {
                Vector2 offset = Vector2.Zero;
                if (parent != null) offset = parent.absolutePos;
                pos = new Vector2(value.X - offset.X, value.Y - offset.Y);
            }
        }

        public BoundingBox box
        {
            get { return new BoundingBox(new Vector3(absolutePos, 0), new Vector3(absolutePos + size, 0)); }
        }

        public Rectangle posRect
        {
            get
            {
                return new Rectangle((int) EngineCore.HundredToLength(absolutePos.X),
                                     (int)(EngineCore.HundredToLength(absolutePos.Y) + EngineCore.RenderedRectangle.Y),
                                     (int) EngineCore.HundredToLength(size.X),
                                     (int)(EngineCore.HundredToLength(size.Y)));
            }
        }

        public object value = null;

        public Object2D(Object2D parent, Vector2 size, Vector2 pos)
        {
            this.parent = parent;
            this.size = size;
            this.pos = pos;
            elements = new List<Object2D>();
        }

        public bool UnderMouse()
        {
            return UnderMouse(EngineCore.GetCurrentCursorPos());
        }

        public bool UnderMouseBefore()
        {
            return UnderMouse(EngineCore.GetOldCursorPos());
        }

        protected virtual bool UnderMouse(Vector2 cursorPos)
        {
            return box.Contains(new Vector3(cursorPos, 0)) == ContainmentType.Contains && active;
        }

        public Object2D ObjUnderMouse()
        {
            for (int i = elements.Count - 1; i >= 0; i--)
            {
                Object2D buf = elements[i].ObjUnderMouse();
                if (buf != null) return buf;
            }
            if (UnderMouse())
                return this;
             return null;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual void Update()
        {
            if (active)
                foreach (Object2D element in elements) element.Update();
        }

        public virtual void Draw()
        {
            foreach (Object2D element in elements) element.Draw();
        }
    }
}
