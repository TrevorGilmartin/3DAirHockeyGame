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
        Texture2D txMenu;
        Song sgMusic;
        SpriteFont spriteFont;
        int dartsRemaining;//variable to use to add to score for the end

        public VictoryScene(GameEngine engine) : base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            spriteFont = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            sgMusic = GameUtilities.Content.Load<Song>("Music\\TavernMusic");
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            txMenu = GameUtilities.Content.Load<Texture2D>("Textures\\Win");
            dartsRemaining = 10 - MainLevelScene.dartsLeft;//to add to the score so that the player knows what he hit
            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))//resetting of variables again as in the retry screen if the player wishes to play again
            {
                Engine.LoadScene(new MainLevelScene(Engine));

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

            spriteBatch.Draw(txMenu, new Rectangle(0, 0,
                GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height), Color.White);

            //draws the total score for the player to observe ie how many targets he got and how many darts he used to hit them.these are accessed by going into the player for the dart and pulling the targetsHit variable
            spriteBatch.DrawString(spriteFont, "You hit" + " " + Player.TargetsHit + " " + "Targets with " + 
                dartsRemaining + " " + "Darts", new Vector2(350, 250), Color.Black);

            spriteBatch.End();

            base.DrawUI();
        }
    }
}
