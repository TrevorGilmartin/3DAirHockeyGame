using Engine;
using Engine.Base;
using Engine.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.AirHockeyGame.Controllers
{
    public class LeftWallController : Component
    {
        private BoxBody LeftWallBody;
        private float rotation = 90;

        public LeftWallController() : base() { }

        public override void Initialize()
        {
            if (Manager.HasComponent<BoxBody>())
            {
                LeftWallBody = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                LeftWallBody.Entity.BecomeKinematic();

                //LeftWallBody.Entity.Orientation = new BEPUutilities.Quaternion(-0, -0, -rotation, -rotation);
            }
            else
            {
                Destroy();
            }
            base.Initialize();
        }

        public override void Update()
        {

            base.Update();
        }
    }
}
