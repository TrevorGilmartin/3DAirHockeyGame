using Engine.Base;
using Engine.Components.Physics;
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

        public LeftWallController() : base() { }

        public override void Initialize()
        {
            if (Manager.HasComponent<BoxBody>())
            {
                LeftWallBody = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                LeftWallBody.Entity.BecomeKinematic();
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
