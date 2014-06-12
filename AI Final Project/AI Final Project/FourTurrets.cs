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
    class FourTurrets : Event
    {
        public static bool classSpawned = false;

        public FourTurrets(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new Turret(worldParent, this, tileParent.corner));
            enemyList.Add(new Turret(worldParent, this, tileParent.corner - new Vector2(tileParent.groundTexture.Width, 0)));
            enemyList.Add(new Turret(worldParent, this, tileParent.location));
            enemyList.Add(new Turret(worldParent, this, tileParent.location + new Vector2(tileParent.groundTexture.Width, 0)));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            Random random = new Random();
            item = new Health();
        }
        public FourTurrets(Event eventParent) : base (eventParent)
        {
            spawned = classSpawned;
            eventChildren.Add(new Tank3Turrets(this));
            eventChildren.Add(new _4Turret2Rover(this));
            item = new Health();
            chance =  1;
            id = 6;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }
}
