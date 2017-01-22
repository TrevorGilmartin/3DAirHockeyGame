using Engine.Base;
using Engine.Components.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.AirHockeyGame.Controllers
{
    public class OpponentPaddleController: Component
    {
        private BoxBody OpponentPaddleBody;

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
            base.Initialize();
        }

        public override void Update()
        {

            base.Update();
        }
    }
}
