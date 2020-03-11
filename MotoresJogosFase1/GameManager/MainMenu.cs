using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Content;

namespace MotoresJogosFase1
{
    public class MenuManager
    {
        private static Texture2D backgroundMainMenu;

        public static Texture2D BackgroundMainMenu
        {
            get { return backgroundMainMenu; }
            set { backgroundMainMenu = value; }
        }

        private static Texture2D backgroundPauseMenu;

        public static Texture2D BackgroundPauseMenu
        {
            get { return backgroundPauseMenu; }
            set { backgroundPauseMenu = value; }
        }

        private static Texture2D backgroundLostMenu;

        public static Texture2D BackgroundLostMenu
        {
            get { return backgroundLostMenu; }
            set { backgroundLostMenu = value; }
        }

        public static void LoadContent(ContentManager content)
        {
            backgroundMainMenu = content.Load<Texture2D>("images/MainMenu");
            backgroundPauseMenu = content.Load<Texture2D>("images/PauseMenu");
            backgroundLostMenu = content.Load<Texture2D>("images/LostMenu");
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            //scale to screen size and draw
            if (GameManager.GameState == GameState.MainMenu)
            {
                spriteBatch.Draw(backgroundMainMenu, destinationRectangle: new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            }
            else if (GameManager.GameState == GameState.Pause)
            {
                spriteBatch.Draw(backgroundPauseMenu, destinationRectangle: new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            }
            else if (GameManager.GameState == GameState.Lost)
            {
                spriteBatch.Draw(backgroundLostMenu, destinationRectangle: new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            }

            spriteBatch.End();
        }
    }
}
