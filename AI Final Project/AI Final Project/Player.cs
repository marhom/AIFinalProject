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
    class Player : Agent
    {
        public world worldParent;
        public double curHeading;
        public int hp, hpMax;
        public Armor armor;
        public Accessory accessory;
        public Weapon weapon;
        public Texture2D sprite;
        public int timer, reloadTime, faccelTimer, raccelTimer, velRamp;
        public bool armorGot, healthGot, weaponGot, accGot;
        public int dispTimer, hdispTimer, cannonFireTimer;
        public Vector2 tVector;
        public Texture2D arrow;
        public float aRotation;

        public Player(world worldParent)
            : base()
        {
            this.worldParent = worldParent;
            hp = 100;
            position = new Vector2(75, 700);
            velocity = 6;
            hpMax = 100;
            sprite = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\rover-1");
            arrow = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\UI\\arrow");
            tint = Color.White;
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            centerPoint = new Vector2(sprite.Width / 2, sprite.Height / 2);
            timer = 0;
            reloadTime = 30;
            faccelTimer = 0;
            raccelTimer = 0;
            armorGot = false;
            healthGot = false;
            weaponGot = false;
            accGot = false;
            tVector = Vector2.Zero;

            velRamp = 1;
        }
        public void Control()
        {
            cannonFireTimer = (int)MathHelper.Clamp(cannonFireTimer - 1, 0, 300);
            tVector = Vector2.Zero; 
            if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Down))
            {
                this.MoveAgent(-1);
                curVel = -velocity;
                directionVector = headingVector * curVel;
            }
            else if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Up))
            {
                this.MoveAgent(1);
                curVel = velocity;
                directionVector = headingVector * curVel;
            }
            else
            {
                curVel = 0;
                directionVector = headingVector * curVel;
            }
            if(worldParent.controlhandler.keyState.IsKeyDown(Keys.PageUp))
            {
                weapon = new Weapon("Cannon", "CAN");
                accessory = new Accessory("Triangulator", "TRG");
                armor = new Armor("Regen", "REG");
            }

            if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Right))
                this.rotateAgent(1);
            if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Left))
                this.rotateAgent(-1);
            if (worldParent.controlhandler.getState().LeftButton == ButtonState.Pressed && worldParent.controlhandler.getPrevState().LeftButton == ButtonState.Released)
            {
                fireWeapon();
            }
            if (weapon != null && weapon.name == "Cannon" && worldParent.controlhandler.getState().RightButton == ButtonState.Pressed && worldParent.controlhandler.getPrevState().RightButton == ButtonState.Released)
                fireCannon();
            if (armor != null && armor.name == "Regen")
                if (timer % 30 == 0)
                    hp = (int)MathHelper.Clamp(hp + 1, 0, hpMax);
            if (accessory != null && accessory.name == "Triangulator")
            {
                if (worldParent.eventList.Count == 0)
                    tVector = this.center - worldParent.worldMap.goalTile.center;
                else
                    foreach (Event events in worldParent.eventList)
                    {
                        if (tVector.Length() < (events.tileParent.center - (this.center)).Length()) //- worldParent.worldMap.worldMap[0, 0].location + worldParent.viewport.originMax)).Length())
                            tVector = -(events.tileParent.center - (this.center));// - worldParent.worldMap.worldMap[0,0].location + worldParent.viewport.originMax);
                        //aRotation = (float)Math.Acos(Vector2.Dot(Vector2.Normalize(tVector), right));
                        aRotation = (float)Math.Atan2(tVector.X, tVector.Y) - (float)Math.PI / 2;
                        //if (events.tileParent.center.Y > this.center.Y)
                        //    aRotation = aRotation - (float)Math.PI/2;
                    }
            }
            timer++;
            Update();
        }

        public void fireCannon()
        {
            if (cannonFireTimer == 0)
            {
                cannonFireTimer = 300;
                worldParent.projectiles.Add(new Cannon(this, worldParent, worldParent.controlhandler.position - this.center));
            }
        }

        public int atEdge()
        {
            int xEdge = 5;
            int yEdge = 10;
            if (directionVector.Length() == 0)
                return 0;

            if ((this.position.X < this.velocity * 30 && Vector2.Normalize(this.directionVector).X < 0))
                xEdge = 2;

            else if ((this.position.X > (1024 - this.velocity * 30) && Vector2.Normalize(this.directionVector).X > 0))
                xEdge = 1;

            if ((this.position.Y < this.velocity * 30 && Vector2.Normalize(this.directionVector).Y < 0))
                yEdge = 2;
            else if (this.position.Y > (768 - this.velocity * 30) && Vector2.Normalize(this.directionVector).Y > 0)
                yEdge = 1;
            return yEdge + xEdge;
        }
        public void fireWeapon()
        {
            worldParent.projectiles.Add(new Projectile(this, worldParent, Vector2.Normalize(worldParent.controlhandler.position - this.center)));

        }
        public void Hit(Projectile projectile)
        {
            hp = (int)MathHelper.Clamp(hp - projectile.damage, 0, hpMax);
            if (hp == 0)
                worldParent.gameOver = true;
        }
        public new void MoveAgent(int direction)
        {
            this.position = new Vector2(MathHelper.Clamp(direction * velocity * headingVector.X + position.X, 0, 1024 - sprite.Width), MathHelper.Clamp(direction * velocity * headingVector.Y + position.Y, 0, 768 - sprite.Height));
        }

        public void getItem(Item item)
        {
            if (item is Armor)
            {
                armor = (Armor)item;
                if (armor.name == "Ablative" && hpMax != 300)
                {
                    hpMax = 300;
                    hp = hp + 200;
                }
                else
                {
                    hpMax = 100;
                    hp = (int)MathHelper.Clamp(hp, 0, hpMax);
                }
                weaponGot = false;
                armorGot = true;
                accGot = false;
                dispTimer = 180;
            }

            if (item is Accessory)
            {
                accessory = (Accessory)item;
                weaponGot = false;
                armorGot = false;
                accGot = true;
                dispTimer = 180;
            
            }
            if (item is Weapon)
            {
                weapon = (Weapon)item;
                weaponGot = true;
                armorGot = false;
                accGot = false;
                dispTimer = 180;
            }
            if (item is Health)
            {
                hp = (int)MathHelper.Clamp(hp + 10, 0, hpMax);
                healthGot = true;
                hdispTimer = 180;
            }

        }
        public Item getState()
        {
            if (world.eventStep <= 1 && accessory == null)
                return new Accessory("", "");
            else if (weapon == null)
                return new Weapon("", "");
            else if (armor == null)
                return new Armor("", "");
            else
                return new Health();
            if (world.eventStep <= 3 && weapon == null)
                return new Weapon("", "");
            else if (armor == null)
                return new Armor("", "");
            else if (accessory == null)
                return new Accessory("", "");
            else
                return new Health();
            if (world.eventStep <= 5 && armor == null)
                return new Armor("", "");
            else if (accessory == null)
                return new Accessory("", "");
            else if (weapon == null)
                return new Weapon("", "");
            else
                return new Health();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, center, null, tint, rotation, centerPoint, 1, SpriteEffects.None, 0);
            if (weapon != null && weapon.name == "Cannon")
            {
                spriteBatch.Draw(world.blank, new Rectangle(1024 - 110, 766-24, 100, 24), Color.Gray);
                spriteBatch.Draw(world.blank, new Rectangle(1026 - 110, 768 - 24, (int)(((float)300 - (float)cannonFireTimer) / 300 * 100)-4, 20), Color.LightYellow);
            }
            //if (accessory != null && accessory.name == "Triangulator")
                //spriteBatch.Draw(arrow, new Rectangle(800 + arrow.Width / 2, 550 + arrow.Height / 2, arrow.Width, arrow.Height), null, Color.Black, aRotation, new Vector2(arrow.Width / 2, arrow.Height / 2), SpriteEffects.FlipHorizontally, 0);
            //spriteBatch.Draw(arrow, new Rectangle(924 + arrow.Width / 2, 668 + arrow.Height / 2, arrow.Width, arrow.Height), null, Color.Black, (float)Math.Acos(Vector2.Dot(Vector2.Normalize(tVector), right)), new Vector2(arrow.Width / 2, arrow.Height / 2), SpriteEffects.None, 0);
        }
    }
}
//public void Control()
//{

//    if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Down))
//    {

//        {
//            raccelTimer = (int)MathHelper.Clamp(raccelTimer - 1, -180, 0);
//            faccelTimer = (int)MathHelper.Clamp(faccelTimer - 1, 0, 180);
//        }
//        curVel = (int)MathHelper.Clamp(curVel - (float)Math.Floor((double)(faccelTimer - raccelTimer) / 60) * velRamp, -baseVelocity, 0);
//        directionVector = headingVector * curVel;
//    }
//    else if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Up))
//    { 

//        raccelTimer = (int)MathHelper.Clamp(raccelTimer + 1, -180, 0);
//        faccelTimer = (int)MathHelper.Clamp(faccelTimer + 1, 0, 180);

//        curVel = (int)MathHelper.Clamp(curVel + (float)Math.Floor((double)(faccelTimer - raccelTimer) / 60) * velRamp, 0, baseVelocity);
//        directionVector = headingVector * curVel;
//    }
//    else
//    {


//        raccelTimer = (int)MathHelper.Clamp(raccelTimer + 1, -180, 0);
//        faccelTimer = (int)MathHelper.Clamp(faccelTimer - 1, 0, 180);
//        if(curVel < 0)
//        curVel = (int)MathHelper.Clamp(curVel + (float)Math.Floor((double)(faccelTimer - raccelTimer) / 60) * velRamp,-baseVelocity, 0 );
//        else
//            curVel = (int)MathHelper.Clamp(curVel - (float)Math.Floor((double)(faccelTimer - raccelTimer) / 60) * velRamp, 0, baseVelocity);

//        directionVector = headingVector * curVel;
//    }
//    MoveAgent();
//    if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Right))
//        this.rotateAgent(1);
//    if (worldParent.controlhandler.keyState.IsKeyDown(Keys.Left))
//        this.rotateAgent(-1);
//    if (worldParent.controlhandler.getState().LeftButton == ButtonState.Pressed && worldParent.controlhandler.getPrevState().LeftButton == ButtonState.Released)
//    {
//        fireWeapon();
//    }
//    if (armor != null && armor.name == "Regen")
//        if ( timer % 60 == 0)
//            hp = (int)MathHelper.Clamp(hp + 1, 0, hpMax);
//    timer++;
//    Update();
//}