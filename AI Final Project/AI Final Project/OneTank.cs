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
    class OneTank : Event
    {
        public static bool classSpawned = false;

        public OneTank(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new Tank(worldParent, this, tileParent.center));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            Random random = new Random();
            item = new Health();
        }
        public OneTank(Event eventParent)
            : base(eventParent)
        {
            spawned = classSpawned;
            eventChildren.Add(new TwoTanks(this));
            item = new Health();
            chance = .50;
            id = 7;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }
}
