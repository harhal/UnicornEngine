using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace UnicornEngine
{
    public class EndLevelMenu:Menu
    {
        public EndLevelMenu(Action exit, Action retry, Action nextLevel) : base("EndLevelMenu/Window", 40)
        {
            elements.Add(new Sprite(this, "EndLevelMenu/Congratulation", 30, new Vector2(5, 15)));
            elements.Add(new Button(this, "EndLevelMenu/Exit", 9, new Vector2(5, 28), new int[] { 1, 1, 1 }, "Exit", exit));
            elements.Add(new Button(this, "EndLevelMenu/Retry", 9, new Vector2(16, 28), new int[] { 1, 1, 1 }, "Retry", retry));
            elements.Add(new Button(this, "EndLevelMenu/NextLevel", 9, new Vector2(27, 28), new int[] { 1, 1, 1 }, "NextLevel", nextLevel));
        }
    }
}
