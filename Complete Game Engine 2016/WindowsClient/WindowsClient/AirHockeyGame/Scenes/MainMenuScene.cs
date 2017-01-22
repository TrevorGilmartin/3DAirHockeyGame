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
        Texture2D txMenu;//the actual texture used to be the context for the menu
        Song sgMusic;//the music that will play as the backing them when the spacebar is pressed
        #endregion

        public MainMenuScene(GameEngine engine):base("menu", engine)
        {

        }

        //create a new instance of SpriteBatch
        //load in the menu texture
        public override void Initialize()
        {
            sgMusic = GameUtilities.Content.Load<Song>("Music\\TavernMusic");//matching the variables to their respected positions.
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            txMenu = GameUtilities.Content.Load<Texture2D>("Textures\\DARTMENU");

            base.Initialize();
        }

        //if the spacebar is pressed
        //Use Engine.LoadScene to load the BowlingScene
        public override void HandleInput()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))//load the level scene when pressed
            {
                Engine.LoadScene(new MainLevelScene(Engine));
                //MediaPlayer.Play(sgMusic);//plays the backing theme , bonus points if you recognise it
            }

            base.HandleInput();
        }

        //Using SpriteBatch draw the menu texture
        //the texture should fill the entire window
        public override void DrawUI()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(txMenu, new Rectangle(0, 0, GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height), Color.White);
            //draws the texture used for the background in its designated position

            spriteBatch.End();

            base.DrawUI();
        }
    }
}
