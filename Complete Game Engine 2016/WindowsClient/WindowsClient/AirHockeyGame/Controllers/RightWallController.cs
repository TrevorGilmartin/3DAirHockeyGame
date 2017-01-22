using Engine.Base;
using Engine.Components.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.AirHockeyGame.Controllers
{
    public class RightWallController : Component
    {
        private BoxBody RightWallBody;

        public RightWallController() : base() { }

        public override void Initialize()
        {
            if (Manager.HasComponent<BoxBody>())
            {
                RightWallBody = Manager.GetComponent(typeof(BoxBody)) as BoxBody;
                RightWallBody.Entity.BecomeKinematic();
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
