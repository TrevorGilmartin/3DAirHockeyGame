using Engine;
using Engine.Base;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
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

        public override void Update()
        {
            if (OpponentPaddleBody.Entity.Position.X > ball.Location.X)
                OpponentPaddleBody.Entity.WorldTransform *=
                                MathConverter.Convert(Matrix.CreateTranslation(-.1f, 0, 0));

            if (OpponentPaddleBody.Entity.Position.X > ball.Location.X)
                OpponentPaddleBody.Entity.WorldTransform *=
                                MathConverter.Convert(Matrix.CreateTranslation(.1f, 0, 0));
            base.Update();
        }
    }
}
