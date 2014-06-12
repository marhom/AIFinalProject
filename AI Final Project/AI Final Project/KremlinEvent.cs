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
    class KremlinEvent : Event
    {
        public KremlinEvent(world worldParent, tile tileParent)
            : base(worldParent, tileParent)
        {
            
            enemyList.Add(new Tank(worldParent, this,  worldParent.worldMap.worldMap[tileParent.gridPosition.X - 4, 5].center));
            enemyList.Add(new Tank(worldParent, this, worldParent.worldMap.worldMap[tileParent.gridPosition.X + 4, 5].center));
            enemyList.Add(new Tank(worldParent, this, worldParent.worldMap.worldMap[tileParent.gridPosition.X, 5].center));
            //enemyList.Add(new Kremlin(worldParent, this, worldParent.worldMap.worldMap[worldParent.worldMap.goalTile.gridPosition.X-1,1].location));
            worldParent.enemyList = new List<Enemy>(worldParent.enemyList.Concat(this.enemyList));
            item = new Health();
          
        }
        public KremlinEvent(Event eventParent)
            : base(eventParent)
        {
            
            item = new Health();
            chance = .5;
            id = 13;
        }
        public override void Completed()
        {
            this.worldParent.eventListR.Add(this);
            worldParent.canWin = true;
            
        }
    }
}
