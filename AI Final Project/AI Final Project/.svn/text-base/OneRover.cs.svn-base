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
    class OneRover : Event
    {
        public static bool classSpawned = false;

        public OneRover(world worldParent, tile tileParent): base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new USSRRover(worldParent, this, new Vector2(tileParent.location.X + tileParent.groundTexture.Width / 2, tileParent.location.Y + tileParent.groundTexture.Height / 2)));
            
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
        }
        public OneRover(Event eventParent) : base(eventParent)
        {
            spawned = classSpawned;
            eventChildren.Add(new TwoRovers(this));
            eventChildren.Add(new twoEnemies(this));
            chance = (world.eventStep > 3) ? .50 : .33;
            item = new Health();
            id = 2;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }

}
