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
    class Tank3Turrets : Event
    {
        public static bool classSpawned = false;

        public Tank3Turrets(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new Tank(worldParent, this, tileParent.center));
            enemyList.Add(new Turret(worldParent, this, tileParent.corner - new Vector2(0, tileParent.groundTexture.Height)));
            enemyList.Add(new Turret(worldParent, this, tileParent.center - new Vector2(0, tileParent.groundTexture.Height)));
            enemyList.Add(new Turret(worldParent, this, tileParent.location));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            item = new Weapon("Cannon", "CAN");
        }
        public Tank3Turrets(Event eventParent)
            : base(eventParent)
        {
            spawned = classSpawned;
            eventChildren.Add(new TwoTanks(this));
            item = new Weapon("Cannon", "CAN");
            chance = .50;
            id = 10;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }
}
