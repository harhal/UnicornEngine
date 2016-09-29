using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Runtime.InteropServices;

namespace UnicornEngine
{
    public static class EngineCore
    {
        #region Графическое ядро
        public static SpriteBatch spriteBatch;
        public static GraphicsDeviceManager graphics;
        public static ContentManager content;
        public static void Initialize(SpriteBatch spriteBatch, ContentManager content)
        {
            EngineCore.spriteBatch = spriteBatch;
            EngineCore.content     = content;
            EngineCore.Cursor = new Sprite(null, "Cursor", 5, Vector2.Zero);
        }
        public static Rectangle RenderedRectangle
        {
            get { return new Rectangle(0, 
                                      (int)((spriteBatch.GraphicsDevice.Viewport.Height - spriteBatch.GraphicsDevice.Viewport.Width * 9 / 16) / 2), 
                                             spriteBatch.GraphicsDevice.Viewport.Width, 
                                      (int)( spriteBatch.GraphicsDevice.Viewport.Width * 9 / 16)); }
        }
        public static float HundredToLength(float Hundred)
        {
            return spriteBatch.GraphicsDevice.Viewport.Width * Hundred / 100;
        }
        public static Vector2 HundredToLength(Vector2 Hundred)
        {
            return new Vector2(spriteBatch.GraphicsDevice.Viewport.Width * Hundred.X / 100,
                               spriteBatch.GraphicsDevice.Viewport.Width * Hundred.Y / 100);
        }
        public static float LengthToHundred(float Length)
        {
            return Length / spriteBatch.GraphicsDevice.Viewport.Width * 100;
        }
        public static Vector2 LengthToHundred(Vector2 Length)
        {
            return new Vector2(Length.X / spriteBatch.GraphicsDevice.Viewport.Width * 100,
                               Length.Y / spriteBatch.GraphicsDevice.Viewport.Width * 100);
        }
        public static Rectangle V2ToRect(Vector2 pos, Vector2 size)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
        }

        #region Курсор
        static Sprite Cursor;
        public static bool ShowCursor = true;

        public static void ChangeCursor(string fileName)
        {
            Cursor.ChangeTexture(fileName);
        }

        public static void DrawCursor()
        {
            Cursor.pos = EngineCore.GetCurrentCursorPos();
            EngineCore.spriteBatch.Begin();
            if (ShowCursor)
                Cursor.Draw();
            EngineCore.spriteBatch.End();
        }

        #endregion
        #endregion


        #region Аудио ядро
        public static float soundVolume;
        private static float _musicVolume;
        public static float musicVolume
        {
            get { return _musicVolume; }
            set { _musicVolume = value % 100; MediaPlayer.Volume = _musicVolume / 100; }
        }
        public static void PlaySound(SoundEffect sound)
        {
            sound.Play(soundVolume / 100, 0, 0);
        }
        public static void PlaySound(SoundEffect sound, float offset)
        {
            sound.Play(soundVolume / 100, 0, offset);
        }
        #endregion

        #region Ядро синхронизации времени
        public static GameTime gameTime;
        #endregion

        #region Ядро ввода
        public static MouseState currentMouseState;
        public static MouseState oldMouseState;
        public static KeyboardState currentKeyboardState;
        public static KeyboardState oldKeyboardState;
        public static Vector2 GetCurrentCursorPos()
        {
            return new Vector2(EngineCore.LengthToHundred(EngineCore.currentMouseState.X), EngineCore.LengthToHundred(EngineCore.currentMouseState.Y - EngineCore.RenderedRectangle.Y));
        }
        public static Vector2 GetOldCursorPos()
        {
            return new Vector2(EngineCore.LengthToHundred(EngineCore.oldMouseState.X), EngineCore.LengthToHundred(EngineCore.oldMouseState.Y - EngineCore.RenderedRectangle.Y));
        }
        public static void Update(GameTime gameTime)
        {
            EngineCore.gameTime = gameTime;
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        #endregion

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        const int ENUM_CURRENT_SETTINGS = -1;

        const int ENUM_REGISTRY_SETTINGS = -2;

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;

        }

        public static Point[] RefreshResolutionList()
        {
            System.Collections.Generic.List<Point> res = new System.Collections.Generic.List<Point>();
            DEVMODE vDevMode = new DEVMODE();
            int i = 0;
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                res.Add(new Point(vDevMode.dmPelsWidth,
                                  vDevMode.dmPelsHeight));
                 i+=3;
            }
            return res.ToArray();
        }
    }
}
