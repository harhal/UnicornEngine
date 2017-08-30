using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnicornEngine
{
    public class SettingMenu : Menu
    {
        Switcher fullScreenSwitcher;
        Slider sounds;
        Slider music;
        Button confirm;

        public SettingMenu(Action close) :
            base("SettingMenu/Window", 50, 0)
        {
            fullScreenSwitcher = new Switcher(this, "SettingMenu/helpSwitcher/BackGround", "SettingMenu/helpSwitcher/Switcher", 20,
                new Vector2(27, 12), EngineCore.FullScreen, delegate 
                {
                    EngineCore.FullScreen = fullScreenSwitcher.value;
                });
            sounds = new Slider(this, "SettingMenu/Slider/BackGround", "SettingMenu/Slider/Slider", 25,
                new Vector2(22, 22), EngineCore.soundVolume, delegate
                {
                    EngineCore.soundVolume = sounds.value;
                });
            music = new Slider(this, "SettingMenu/Slider/BackGround", "SettingMenu/Slider/Slider", 25,
                new Vector2(22, 32), EngineCore.musicVolume, delegate
                {
                    EngineCore.musicVolume = music.value;
                });
            confirm = new Button(this, "SettingMenu/Button", 30, new Vector2(10, 45), new int[] { 1, 1, 2 }, "Confirm", close);
            confirm.elements.Add(new Sprite(confirm, "SettingMenu/Save", 20, new Vector2(6, 0)));
            elements.Add(fullScreenSwitcher);
            elements.Add(new Sprite(this, "SettingMenu/helpOn", 20, new Vector2(3, 12)));
            elements.Add(sounds);
            elements.Add(new Sprite(this, "SettingMenu/Sounds", 10, new Vector2(3, 24)));
            elements.Add(music);
            elements.Add(new Sprite(this, "SettingMenu/Music", 15, new Vector2(3, 34)));
            elements.Add(confirm);
        }
        
    }
}
