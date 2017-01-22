using Engine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Engine.Engines;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WindowsClient.AirHockeyGame.Scenes
{
    public class MainMenuScene : Scene
    {
        #region scene that contains the intial menu.
        SpriteBatch spriteBatch;//used to draw
        Texture2D txBackground;//the actual texture used to be the context for the menu
        SpriteFont spriteFontBig;
        SpriteFont spriteFontBiggest;
        Song sgMusic;//the music that will play as the backing them when the spacebar is pressed
        private string buttonMessage;
        private string gameMessage;
        Color colour;
        bool shouldAlphaGoUp;
        #endregion

        public MainMenuScene(GameEngine engine):base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            sgMusic = GameUtilities.Content.Load<Song>("Sounds\\thegame");//matching the variables to their respected positions.
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            txBackground = GameUtilities.Content.Load<Texture2D>("Textures\\background");
            spriteFontBig = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            spriteFontBiggest = GameUtilities.Content.Load<SpriteFont>("Fonts\\biggestFont");

            buttonMessage = "Press Enter to Play";
            gameMessage = "Air-Hockey";

            colour.A = 255;
            colour.B = 150;
            colour.G = 220;
            colour.R = 30;

            MediaPlayer.Play(sgMusic);
            base.Initialize();
        }

        public override void HandleInput()
        {
            if (shouldAlphaGoUp)
            {
                colour.A++;
                colour.B++;
                colour.G++;
                colour.R++;
            }
            else
            {
                colour.A--;
                colour.B--;
                colour.G--;
                colour.R--;
            }

            if (colour.A == 0 || colour.B == 0 || colour.G == 0 || colour.R == 0)
                shouldAlphaGoUp = true;
            if (colour.A == 255 || colour.B == 255 || colour.G == 255 || colour.R == 255)
                base.Update();

            if (InputEngine.IsKeyPressed(Keys.Enter))//load the level scene when pressed
            {
                Engine.LoadScene(new MainLevelScene(Engine));
            }
            base.HandleInput();
        }

        //Using SpriteBatch draw the menu texture
        //the texture should fill the entire window
        public override void DrawUI()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(txBackground, new Rectangle(0, 0, GameUtilities.GraphicsDevice.Viewport.Width,
                              GameUtilities.GraphicsDevice.Viewport.Height),Color.White);
            //draws the texture used for the background in its designated position

            spriteBatch.DrawString(spriteFontBig, buttonMessage, new Vector2(
                GameUtilities.GraphicsDevice.Viewport.Width / 2 - 200,
                GameUtilities.GraphicsDevice.Viewport.Height / 2 - 150), Color.White);

            spriteBatch.DrawString(spriteFontBiggest, gameMessage, new Vector2(
              GameUtilities.GraphicsDevice.Viewport.Width / 2 - 200,
              GameUtilities.GraphicsDevice.Viewport.Height / 2 - 50), colour);

            spriteBatch.End();

            base.DrawUI();
        }
    }
}
