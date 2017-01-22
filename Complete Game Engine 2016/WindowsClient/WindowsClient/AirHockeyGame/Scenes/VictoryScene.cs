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
    public class VictoryScene : Scene
    {
        //scene occurs only if the player wins the game
        SpriteBatch spriteBatch;
        Texture2D txBackground;
        Texture2D txCentreImage;
        Song sgMusic;
        SpriteFont spriteFont;
        private bool shouldAlphaGoUp;
        private Color colour;

        public VictoryScene(GameEngine engine) : base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            spriteFont = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            sgMusic = GameUtilities.Content.Load<Song>("Sounds\\winning");
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            txBackground = GameUtilities.Content.Load<Texture2D>("Textures\\winnerBackground");
            txCentreImage = GameUtilities.Content.Load<Texture2D>("Textures\\winner");

            MediaPlayer.Stop();
            MediaPlayer.Play(sgMusic);

            colour.A = 255;
            colour.B = 150;
            colour.G = 220;
            colour.R = 30;

            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
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

            if (InputEngine.IsKeyPressed(Keys.R))//resetting of variables again as in the retry screen if the player wishes to play again
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

            spriteBatch.Draw(txBackground, new Rectangle(0, 0,
                GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height), Color.White);

            spriteBatch.Draw(txCentreImage, new Rectangle(
               GameUtilities.GraphicsDevice.Viewport.Width / 2 - 70,
               GameUtilities.GraphicsDevice.Viewport.Height / 2 - 130,
               GameUtilities.GraphicsDevice.Viewport.Width / 2 - 500,
               GameUtilities.GraphicsDevice.Viewport.Height / 2 - 50), Color.White);

             spriteBatch.DrawString(spriteFont, "Congratulations on the Victory Press R to Retry", new Vector2(
                GameUtilities.GraphicsDevice.Viewport.Width / 2 - 450,
                GameUtilities.GraphicsDevice.Viewport.Height / 2 - 300), colour);

            spriteBatch.End();

            base.DrawUI();
        }
    }
}
