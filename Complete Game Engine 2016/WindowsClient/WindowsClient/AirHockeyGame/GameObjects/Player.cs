using Engine.Base;
using Engine.Components.Cameras;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsClient.AirHockeyGame.GameObjects
{
    public class Player : GameObject
    {
        public string Name { get; set; }

        public static int TargetsHit = 0;

        public Player(Vector3 location) : base(location)
        {
            Name = Environment.UserName;
        }

        //Add only a ThirdPerson camera
        //Find the a useable offset to position the camera
        public override void Initialize()
        {
            Manager.AddComponent(new ThirdPersonCamera(new Vector3(0, 0, -1), new Vector3(0, 2, 10))); // setting the positon of the camera 

            base.Initialize();
        }

        public PlayerSave CreateSave()
        {
            return new PlayerSave()
            {
                Name = this.Name,

            };
        }

        public class PlayerSave
        {
            public int TargetsHit { get; set; }
            public string Name { get; set; }
        }
    }
}
