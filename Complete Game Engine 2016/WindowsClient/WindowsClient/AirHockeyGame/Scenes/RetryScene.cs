using Engine;
using Engine.Base;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClient.AirHockeyGame.GameObjects;

namespace WindowsClient.AirHockeyGame.Scenes
{
    public class RetryScene : Scene
    {
        //scene similar to the intial menu scene just with different texture to be the context.
        SpriteBatch spriteBatch;
        Texture2D txCentreImage;
        Song sgMusic;
        private Texture2D txMenu;
        private SpriteFont spriteFont;

        public RetryScene(GameEngine engine) : base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            sgMusic = GameUtilities.Content.Load<Song>("Sounds\\failure");
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            txCentreImage = GameUtilities.Content.Load<Texture2D>("Textures\\failure");
            spriteFont = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            txMenu = GameUtilities.Content.Load<Texture2D>("Textures\\failureBackground");

            MediaPlayer.Stop();
            MediaPlayer.Play(sgMusic);
            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.R))//starts the game again if pressed aswell as restarting the music and resetting all the core variables of the game
            {
                MediaPlayer.Stop();
                Engine.LoadScene(new MainLevelScene(Engine));
            }

            base.HandleInput();
        }

        //Using SpriteBatch draw the menu texture
        //the texture should fill the entire window
        public override void DrawUI()
        {
            spriteBatch.Begin();

            //draws the texture for the menu context
            spriteBatch.Draw(txMenu, new Rectangle(0, 0,
                GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height), Color.White);

            spriteBatch.Draw(txCentreImage, new Rectangle(
               GameUtilities.GraphicsDevice.Viewport.Width / 2 - 300,
               GameUtilities.GraphicsDevice.Viewport.Height / 2 - 150,
               GameUtilities.GraphicsDevice.Viewport.Width / 2,
               GameUtilities.GraphicsDevice.Viewport.Height / 2), Color.White);

            spriteBatch.DrawString(spriteFont, "Press R to Retry", new Vector2(
                GameUtilities.GraphicsDevice.Viewport.Width / 2 - 200,
                GameUtilities.GraphicsDevice.Viewport.Height / 2 - 150), Color.Red);

            spriteBatch.End();

            base.DrawUI();
        }
    }
}
