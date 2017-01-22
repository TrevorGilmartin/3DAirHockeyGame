using Engine.Base;
using Engine.Components.Graphics;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClient.AirHockeyGame.Controllers;

namespace WindowsClient.AirHockeyGame.GameObjects
{
    public class Ball : GameObject
    {
        public Ball(Vector3 location) : base(location)
        {

        }

        //add a BasicEffect model using the bowlingball model
        //add a SphereBody with a mass of 15
        //add a BowlingBallController
        public override void Initialize()
        {
            Manager.AddComponent(new BasicEffectModel("bowlingball"));
            Manager.AddComponent(new SphereBody(15));
            Manager.AddComponent(new BallController());

            base.Initialize();
        }
    }
}
