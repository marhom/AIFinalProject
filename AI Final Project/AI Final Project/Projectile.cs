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
    class Projectile
    {
        public Texture2D projectileTexture;
        public Vector2 position, heading, target;
        public static int velocity = 10;
        public static int range = 400;
        public static int reload = 60;
        
        public float distance;
        public world worldParent;
        private Rectangle _drawRectangle;
        public Rectangle drawRectangle { get { return new Rectangle((int)position.X, (int)position.Y, _drawRectangle.Width, _drawRectangle.Height); } set { _drawRectangle = value; } }
        public Agent parent;
        public int damage;
        public Projectile(Agent parent, world worldParent, Vector2 heading)
        {
            this.parent = parent;
            
            //this.target = target;
            this.worldParent = worldParent;
            this.heading = heading;
                
            this.position = this.parent.center + this.heading * 20;
            distance = 0;
            drawRectangle = new Rectangle((int)(parent.center + Vector2.Normalize(heading) * 20).X, (int)(parent.center + Vector2.Normalize(heading) * 20).Y, 10, 10);
            this.projectileTexture = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Projectile4");
            damage = 5;
            
        }   
        public virtual void Update()
        {
            distance = distance + (heading * velocity).Length();
            if (distance > range)
                terminate();
            if (this.parent is Enemy)
            {
                if(this.drawRectangle.Intersects(worldParent.player.collisionRectangle))
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
                        this.worldParent.projectilesR.Add(this);
                    }

            this.position = position + heading * velocity;

        }
        public void terminate()
        {
            this.worldParent.projectilesR.Add(this);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(projectileTexture, drawRectangle, Color.Red);
        }

        
    }
}
