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
    class Event
    {
        public List<Enemy> enemyList;
        public tile tileParent;
        public world worldParent;
        public Item item;
        public int id;
        public bool itemTaken, itemSpawn;
        public Vector2 shift;
        public Event eventParent;
        public List<Event> eventChildren;
        public List<double> eventChance;
        public static Texture2D eventTexture;
        private static bool classSpawned;
        public bool spawned;
        public double chance;
        public static string name;
        public Rectangle _drawRectangle;
        public Rectangle drawRectangle { get { return new Rectangle((int)(tileParent.location.X + /*shift.X*/ + tileParent.groundTexture.Width / 2 - _drawRectangle.Width / 2), (int)(tileParent.location.Y + /*shift.Y*/ +tileParent.groundTexture.Height / 2 - _drawRectangle.Height / 2), _drawRectangle.Width, _drawRectangle.Height); } set { _drawRectangle = value; } }
        public Event(world worldParent, tile tileParent)
        {
            this.tileParent = tileParent;
            this.worldParent = worldParent;
            eventTexture = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\UI\\blank");
            this.enemyList = new List<Enemy>();
            itemTaken = false;
            itemSpawn = false;
            drawRectangle = new Rectangle((int)(tileParent.location.X + tileParent.groundTexture.Width/2 - _drawRectangle.Width/2), (int)(tileParent.location.Y + tileParent.groundTexture.Height/2 - _drawRectangle.Height/2), 20, 15);
            item = new Health();
            spawned = classSpawned;
            id = 0;
            shift = Vector2.Zero;
        }
        public Event(Event eventParent)
        {
            
            this.eventParent = eventParent;
            eventChildren = new List<Event>();
            eventChance = new List<double>();
        }
        public void Update()
        {
            if (enemyList.Count == 0)
            {
                itemSpawn = true;
                if (drawRectangle.Intersects(worldParent.player.collisionRectangle))
                {
                    worldParent.player.getItem(item);
                    itemTaken = true;
                }
            }
            if (itemTaken)
                Completed();

        }
        public virtual void Completed()
        {
            this.worldParent.eventListR.Add(this);
            classSpawned = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (itemSpawn)
            {
                spriteBatch.Draw(item.itemTexture, this.drawRectangle, Color.White);
                //spriteBatch.Draw(eventTexture, new Rectangle(drawRectangle.X + 1, drawRectangle.Y + 1, drawRectangle.Width - 2, drawRectangle.Width - 2), Color.White);
                //spriteBatch.DrawString(worldParent.consoleFont, item.id, new Vector2(drawRectangle.X + drawRectangle.Width/2 - worldParent.consoleFont.MeasureString(item.id).X / 2, drawRectangle.Y + drawRectangle.Height/ 2 - worldParent.consoleFont.MeasureString(item.id).Y / 2 + 1), item.color);
            }
            //spriteBatch.DrawString(worldParent.consoleFont, "EVENT", tileParent.location + new Vector2(tileParent.groundTexture.Width / 2, tileParent.groundTexture.Height / 2), Color.Red);
        }
    }
}
