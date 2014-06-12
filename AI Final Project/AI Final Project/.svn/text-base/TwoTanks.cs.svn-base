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
    class TwoTanks : Event
    {
        public static bool classSpawned = false;

        public TwoTanks(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            spawned = classSpawned;
            enemyList.Add(new Tank(worldParent, this, new Vector2(tileParent.location.X, tileParent.center.Y)));
            enemyList.Add(new Tank(worldParent, this, new Vector2(tileParent.corner.X, tileParent.center.Y)));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            Random random = new Random();
            switch (random.Next(0, 2))
            {
                case 0:
                    item = new Armor("Ablative", "ABL");
                    break;
                case 1:
                    item = new Armor("Regen", "REG");
                    break;
            }
        }
        public TwoTanks(Event eventParent)
            : base(eventParent)
        {
            spawned = classSpawned;
            item = new Armor("","");
            chance = 1;
            id = 8;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }
    }
}
