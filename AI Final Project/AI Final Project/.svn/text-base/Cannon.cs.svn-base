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
    class Cannon : Projectile
    {
        public Cannon(Agent parent, world worldParent, Vector2 heading) : base(parent, worldParent, heading)
        {
            if (parent is Player)
            {
                range = (int)heading.Length();
                this.heading = Vector2.Normalize(heading);
            }
            else
                range = 600;
            this.position = this.parent.center + this.heading * 20;
            this.drawRectangle = new Rectangle((int)(parent.center + this.heading * 20).X, (int)(parent.center + this.heading * 20).Y, 20, 20);
            this.projectileTexture = worldParent.projectileTexture2;
            
            if (parent is Enemy)
                this.damage = 35;
            else
                this.damage = 10;
        }
        public override void Update()
        {
            distance = distance + (heading * velocity).Length();
            if (distance > range)
                terminate();
            if (this.parent is Enemy)
            {
                if (this.drawRectangle.Intersects(worldParent.player.collisionRectangle))
                {
                    worldParent.player.Hit(this);
                    this.terminate();
                }

            }
            else

                foreach (Enemy enemy in worldParent.enemyList)
                    if (this.drawRectangle.Intersects(enemy.collisionRectangle))
                    {
                        enemy.isHit(damage);
                    }

            this.position = position + heading * velocity;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(projectileTexture, drawRectangle, Color.Goldenrod);
        }

    }
}
