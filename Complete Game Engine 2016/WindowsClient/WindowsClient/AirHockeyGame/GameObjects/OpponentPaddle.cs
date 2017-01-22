using Engine.Base;
using Engine.Components.Graphics;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClient.AirHockeyGame.Controllers;

namespace WindowsClient.AirHockeyGame.GameObjects
{
    public class OpponentPaddle : GameObject
    {

        public OpponentPaddle(Vector3 location) : base(location)
        {

        }

        //add a BasicEffect model using the bowlingball model
        //add a SphereBody with a mass of 15
        //add a BowlingBallController
        public override void Initialize()
        {
            //holds the model and associates this with the class
            Manager.AddComponent(new BasicEffectModel("board"));
            Manager.AddComponent(new BoxBody(15));//add the body and setting the mass
            Manager.AddComponent(new OpponentPaddleController());//associating the controller with the class

            base.Initialize();
        }

    }
}