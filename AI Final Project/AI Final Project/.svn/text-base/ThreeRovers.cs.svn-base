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
    class ThreeRovers : Event
    {
        public static bool classSpawned = false;

        public ThreeRovers(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new USSRRover(worldParent, this, tileParent.corner));
            enemyList.Add(new USSRRover(worldParent, this, tileParent.corner - new Vector2(tileParent.groundTexture.Width, 0)));
            enemyList.Add(new USSRRover(worldParent, this, tileParent.center));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            item = new Health();    
        }
        public ThreeRovers(Event eventParent)
            : base(eventParent)
        {
            spawned = classSpawned;
            eventChildren.Add(new OneTank(this));

            eventChildren.Add(new FourRovers(this));
            item = new Health();
            chance = 1;
            id =11;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }
}
