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
    class TwoTurrets :Event 
    {
        public static bool classSpawned = false;

        public TwoTurrets(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new Turret(worldParent, this, tileParent.corner));
            enemyList.Add(new Turret(worldParent, this, tileParent.corner - new Vector2(tileParent.groundTexture.Width, 0)));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            item = new Accessory("Triangulator", "TRG");
        }
        public TwoTurrets(Event eventParent) : base(eventParent)
        {
            spawned = classSpawned;
            eventChildren.Add(new FourTurrets(this));
            item = new Accessory("Triangulator","TRG");
            chance = 1;
            id = 5;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }

    }
}
