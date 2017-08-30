using System;

namespace UnicornEngine
{
    public abstract class Screen
    {

        public Action<int> exit;
        public Screen(Action<int> exit)
        {
            this.exit = exit;
        }

        abstract public void Update();

        abstract public void Draw();
    }
}
