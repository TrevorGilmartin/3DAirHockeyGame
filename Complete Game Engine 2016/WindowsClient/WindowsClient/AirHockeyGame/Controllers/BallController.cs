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
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using WindowsClient.AirHockeyGame.GameObjects;

namespace WindowsClient.AirHockeyGame.Controllers
{
    public class BallController : Component
    {
        private float MovementSpeed;
        private int isNegativeStart;

        private SphereBody ballSphere;
        private BEPUutilities.Vector3 impulse = new BEPUutilities.Vector3();
        private BEPUutilities.Vector3 currentImpulse = new BEPUutilities.Vector3();

        //where is the original location of the ball
        private Vector3 startLocation;

        //has the ball collided with things
        private bool wasLeftWallHit;
        private bool wasRightWallHit;
        private bool wasOpponentPaddleHit;
        private bool wasPaddleHit;

        public bool hasFailed { get; set; }

        public BallController() : base() { }

        //get the starting location of the game object
        //check if the owner has a SphereBody component
        //if it does then get it and set the Entity to be Kinematic
        public override void Initialize()
        {
            if (Manager.HasComponent<SphereBody>())
            {
                ballSphere = Manager.GetComponent(typeof(SphereBody)) as SphereBody;
                ballSphere.Entity.CollisionInformation.Events.DetectingInitialCollision += CollidedWith;
                ballSphere.Entity.BecomeKinematic();
            }
            else
            {
                Destroy();
            }

            startLocation = Manager.Owner.Location;

            base.Initialize();
        }

        private void CollidedWith(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
        {
            if (other.Tag is PhysicsComponent.GameObjectInfo)
            {
                var tag = (other.Tag as PhysicsComponent.GameObjectInfo);

                if (tag.ObjectType == typeof(LeftWall))
                {
                    wasLeftWallHit = true;
                }

                else if (tag.ObjectType == typeof(RightWall))
                {
                    wasRightWallHit = true;
                }

                else if (tag.ObjectType == typeof(OpponentPaddle))
                {
                    wasOpponentPaddleHit = true;
                }

                else if (tag.ObjectType == typeof(Paddle))
                {
                    wasPaddleHit = true;
                }
            }
        }

        //reset the hasThrown and ThrowPower back to their defaul values
        //set the ballSpeher to be back at teh starting position
        //set the ballSphere Entity LinearVelocity and AngularVelocity to be zero
        //set the ballSphere Entity to be Kinematic
        private void Reset()
        {

            ballSphere.Entity.LinearVelocity = BEPUutilities.Vector3.Zero;
            ballSphere.Entity.AngularVelocity = BEPUutilities.Vector3.Zero;
            ballSphere.Entity.BecomeKinematic();

            ballSphere.Entity.Position = MathConverter.Convert(startLocation);
        }

        public override void Update()
        {
            if (InputEngine.IsKeyPressed(Keys.R))
                Reset();

            ballSphere.Entity.CollisionInformation.Events.DetectingInitialCollision += CollidedWith;

            Random rand = new Random();
            MovementSpeed = rand.Next(3, 10);
            isNegativeStart = 1;/*rand.Next(0, 1);*/

            StartTheGame();

            if (wasOpponentPaddleHit)
            {
                impulse.Z = (MovementSpeed * .1f);
                ballSphere.Entity.LinearVelocity += impulse;

                wasOpponentPaddleHit = false;
            }

            if (wasPaddleHit)
            {
                impulse.Z = -(MovementSpeed * .1f);
                ballSphere.Entity.LinearVelocity += impulse;

                wasPaddleHit = false;
            }

            if (wasLeftWallHit)
            {
                impulse.X = (MovementSpeed * .1f);
                ballSphere.Entity.LinearVelocity += impulse;

                wasLeftWallHit = false;
            }

            
            if (wasRightWallHit)
            {
                impulse.X = -(MovementSpeed * .1f);
                ballSphere.Entity.LinearVelocity += impulse;

                wasRightWallHit = false;
            }

            if (ballSphere.Entity.Position.Z == 51)
                hasFailed = true;

            ballSphere.Entity.LinearVelocity += impulse;
            base.Update();
        }

        private void StartTheGame()
        {
            if (InputEngine.IsKeyPressed(Keys.Space))
            {
                if (ballSphere != null)
                {

                    ballSphere.Entity.BecomeDynamic(ballSphere.Mass);

                    if (isNegativeStart == 0)
                    {
                        impulse.Z = (MovementSpeed * .1f);
                        ballSphere.Entity.LinearVelocity += impulse;
                    }
                    else if (isNegativeStart == 1)
                    {
                        impulse.Z = -(MovementSpeed * .1f);
                        //impulse.X = -(MovementSpeed * 5);
                        ballSphere.Entity.LinearVelocity += impulse;
                        //angularImpulse.Y = -(MovementSpeed * 5);
                        //ballSphere.Entity.LinearVelocity += impulse;
                        //ballSphere.Entity.AngularVelocity += angularImpulse;
                    }
                }
            }
        }
    }
}
