using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace AI_Final_Project
{
    class FourRovers : Event
    {
        public static bool classSpawned = false;

        public FourRovers(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new USSRRover(worldParent, this, tileParent.corner));
            enemyList.Add(new USSRRover(worldParent, this, tileParent.corner - new Vector2(tileParent.groundTexture.Width, 0)));
            enemyList.Add(new USSRRover(worldParent, this, tileParent.location));
            enemyList.Add(new USSRRover(worldParent, this, tileParent.location + new Vector2(tileParent.groundTexture.Width,0)));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            item = new Weapon("Cannon","CAN");
        }
        public FourRovers(Event eventParent)
            : base(eventParent)
        {
            spawned = classSpawned;
            item = new Weapon("Cannon", "CAN");
            chance = .5;
            id = 12;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }
}
