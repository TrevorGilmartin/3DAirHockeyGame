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
using WindowsClient.AirHockeyGame.GameObjects;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;

namespace WindowsClient.AirHockeyGame.Controllers
{
    public class PaddleController : Component
    {
        public float ThrowPower { get; set; }

        private float MovementSpeed = 0.1f;
        private BoxBody PaddleBody;
        private BEPUutilities.Vector3 ForwardMomentum = new BEPUutilities.Vector3();
        private Vector3 startLocation;
        private Quaternion startRotation;
        private float distance = 4;
        private Player data;

        //has the ball being thrown
        public static bool hasCollision = false;

        public PaddleController() : base() { }

        public PaddleController(Player data)
        {
            this.data = data;
        }

        public override void Initialize()
        {

            if (Manager.HasComponent<BoxBody>())
            {
                PaddleBody = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                PaddleBody.Entity.BecomeKinematic();
            }
            else
            {
                Destroy();
            }

            startLocation = Manager.Owner.Location;
            startRotation = Manager.Owner.Rotation + new Quaternion();

            base.Initialize();
        }

        private void Reset()
        {
            hasCollision = false;//this method puts the dart back in its original position and orientation so that the player will be ready to launch it again.
            ThrowPower = 0;
            //resets its velocity and makes it kinetimatic
            PaddleBody.Entity.LinearVelocity = BEPUutilities.Vector3.Zero;
            PaddleBody.Entity.AngularVelocity = BEPUutilities.Vector3.Zero;
            PaddleBody.Entity.BecomeKinematic();

            PaddleBody.Entity.Position = MathConverter.Convert(startLocation);//resets the position
            PaddleBody.Entity.Orientation = MathConverter.Convert(startRotation);//resets the orientation
        }


        public override void Update()
        {
            //If the R key is pressed then call Reset
            //As long as the spacebar is held, increase ThrowPower by 1(max 10)
            #region Reset and Charge Shot
            if (InputEngine.IsKeyPressed(Keys.R))
                Reset();

            if (InputEngine.IsKeyHeld(Keys.Space))
            {
                if (ThrowPower < 10)
                    ThrowPower++;//increments the power the longer the spacebar is held
            }
            #endregion

            //move the ball left and right
            if (!hasCollision)//contains the code that moves the dart up down left and right
            {
                if (InputEngine.IsKeyHeld(Keys.A))
                {
                    if (Manager.Owner.Location.X > startLocation.X - distance)
                        PaddleBody.Entity.WorldTransform *= MathConverter.Convert(Matrix.CreateTranslation(-MovementSpeed, 0, 0));
                }

                else if (InputEngine.IsKeyHeld(Keys.D))
                {
                    if (Manager.Owner.Location.X < startLocation.X + distance)
                        PaddleBody.Entity.WorldTransform *=
                            MathConverter.Convert(Matrix.CreateTranslation(MovementSpeed, 0, 0));
                }

                base.Update();
            }
        }
    }
}
