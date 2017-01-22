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
        Texture2D txMenu;
        Song sgMusic;

        public RetryScene(GameEngine engine) : base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            sgMusic = GameUtilities.Content.Load<Song>("Music\\TavernMusic");
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            txMenu = GameUtilities.Content.Load<Texture2D>("Textures\\FailScreen");

            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))//starts the game again if pressed aswell as restarting the music and resetting all the core variables of the game
            {
                Engine.LoadScene(new MainLevelScene(Engine));
                MediaPlayer.Play(sgMusic);
                Player.TargetsHit = 0;
                //TargetController3.win = false;
                MainLevelScene.dartsLeft = 10;
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

            spriteBatch.End();

            base.DrawUI();
        }
    }
}
