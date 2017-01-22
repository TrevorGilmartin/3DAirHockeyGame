using Engine;
using Engine.Base;
using Engine.Components.Physics;
using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClient.AirHockeyGame.GameObjects;
using WindowsClient.AirHockeyGame.Scenes;

namespace WindowsClient.AirHockeyGame.Controllers
{
    public class OpponentPaddleController: Component
    {
        private BoxBody OpponentPaddleBody;
        private Ball ball;
        private Vector3 startLocation;
        private Quaternion startRotation;
        private float distance = 6;

        public OpponentPaddleController() : base() { }

        public override void Initialize()
        {
            if (Manager.HasComponent<BoxBody>())
            {
                OpponentPaddleBody = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                OpponentPaddleBody.Entity.BecomeKinematic();
            }
            else
            {
                Destroy();
            }

            ball = MainLevelScene.ball;
            base.Initialize();
        }

        private void Reset()
        {
            //resets its velocity and makes it kinetimatic
            OpponentPaddleBody.Entity.LinearVelocity = BEPUutilities.Vector3.Zero;
            OpponentPaddleBody.Entity.AngularVelocity = BEPUutilities.Vector3.Zero;
            OpponentPaddleBody.Entity.BecomeKinematic();

            OpponentPaddleBody.Entity.Position = MathConverter.Convert(startLocation);//resets the position
            OpponentPaddleBody.Entity.Orientation = MathConverter.Convert(startRotation);//resets the orientation
        }

        public override void Update()
        {
            if (InputEngine.IsKeyPressed(Keys.R))
                Reset();

            if (OpponentPaddleBody.Entity.Position.X > ball.Location.X)
                if (Manager.Owner.Location.X > startLocation.X - distance)
                    OpponentPaddleBody.Entity.WorldTransform *=
                                MathConverter.Convert(Matrix.CreateTranslation(-.1f, 0, 0));

            if (-OpponentPaddleBody.Entity.Position.X > -ball.Location.X)
                if (-Manager.Owner.Location.X > -startLocation.X - distance)
                    OpponentPaddleBody.Entity.WorldTransform *=
                                MathConverter.Convert(Matrix.CreateTranslation(.1f, 0, 0));
            base.Update();
        }
    }
}
