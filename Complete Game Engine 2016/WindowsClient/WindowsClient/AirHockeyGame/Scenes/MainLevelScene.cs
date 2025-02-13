﻿using Engine;
using Engine.Base;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClient.AirHockeyGame.Controllers;
using WindowsClient.AirHockeyGame.GameObjects;
using WindowsClient.GameObjects;

namespace WindowsClient.AirHockeyGame.Scenes
{
    public class MainLevelScene : Scene
    {
        //variables used
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D HUD;
        Song sgMusic;
        public static int dartsLeft = 10;

        public static Player player;
        public static Paddle paddle;
        public static OpponentPaddle opponentPaddle;
        public static Ball ball;
        public static LeftWall leftWall;
        public static RightWall rightWall;

        PaddleController paddleController;

        public MainLevelScene(GameEngine engine) : base("AirHockey", engine)
        {

        }

        public override void Initialize()
        {
            //Assignment of Variables
            spriteBatch = new SpriteBatch(GameUtilities.GraphicsDevice);
            spriteFont = GameUtilities.Content.Load<SpriteFont>("Fonts\\bigFont");
            sgMusic = GameUtilities.Content.Load<Song>("Sounds\\mainSong");

            MediaPlayer.Stop();
            MediaPlayer.Play(sgMusic);
            //seFail = GameUtilities.Content.Load<SoundEffect>("SoundEffects\\Fail");

            AddObject(new StaticModelObject("floor", Vector3.Zero));

            paddle = new Paddle(new Vector3(0, 2, 50));//placement of Dart
            AddObject(paddle);

            leftWall = new LeftWall(new Vector3(-9, 2, 25));
            AddObject(leftWall);

            opponentPaddle = new OpponentPaddle(new Vector3(0, 2, 0));//placement of Player
            AddObject(opponentPaddle);

            rightWall = new RightWall(new Vector3(9, 2, 25));
            AddObject(rightWall);

            ball = new Ball(new Vector3(0, 2, 25));
            AddObject(ball);

            player = new Player(new Vector3(0, 5, 50));//placement of player/camera
            AddObject(player);

            HUD = GameUtilities.Content.Load<Texture2D>("Textures\\hud");

            base.Initialize();

            paddleController = paddle.Manager.GetComponent(typeof(PaddleController)) as PaddleController;
        }

        public override void DrawUI()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(HUD, new Rectangle(0, 0, GameUtilities.GraphicsDevice.Viewport.Width,
                GameUtilities.GraphicsDevice.Viewport.Height), Color.White);

            //drawing controls

            spriteBatch.DrawString(spriteFont, "Space = Charge Shot", new Vector2(20, 90), Color.White);
            spriteBatch.DrawString(spriteFont, "Enter =  Shoot", new Vector2(20, 140), Color.White);

            float xLength = spriteFont.MeasureString("Power: " + paddleController.ThrowPower).X;
            //to record the numerical value of the strength of throwing the dart from its controller.

            //drawing the actual result of the power being recorded
            spriteBatch.DrawString(spriteFont, "Power: " + paddleController.ThrowPower, new Vector2(
                    GameUtilities.GraphicsDevice.Viewport.Width / 2 - xLength / 2,
                    GameUtilities.GraphicsDevice.Viewport.Height - 80), Color.Black);

            spriteBatch.End();

            base.DrawUI();
        }
        public override void HandleInput()
        {
            //handles conditions to do with the game i.e when the last target is hit,if certain buttons are pressed and whether or not the last dart has been fired
            //if (TargetController3.win == true)
            //{
            //    Engine.LoadScene(new VictoryScene(Engine));
            //}
            if (ball.Location.Z > 60)
            {
                MediaPlayer.Stop();
                Engine.LoadScene(new RetryScene(Engine));
            }

            if (ball.Location.Z < -10)
            {
                MediaPlayer.Stop();
                Engine.LoadScene(new VictoryScene(Engine));
            }

            if (InputEngine.IsKeyPressed(Keys.M))
            {
                MediaPlayer.Stop();
                Engine.LoadScene(new MainMenuScene(Engine));
            }
            base.HandleInput();
        }
    }
}
