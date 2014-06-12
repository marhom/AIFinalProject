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
    class Enemy : Agent
    {
        public enum state { seek, patrol, avoid, inactive, stationary, guard, retToGuard, fight };
        private bool _targDistance;
        public bool targDistance { get { return (this.center - playerTarget.center).Length() < _pursueDistance; } set { _targDistance = value; } }
        public bool gunRange { get { return (this.center - playerTarget.center).Length() < Projectile.range; } set { _targDistance = value; } }
        public Player playerTarget;
        public world worldParent;
        public state State, pState;
        protected int reloadTime;
        public Event eventParent;
        public Projectile projectile;
        public const int detectionRange = 500;
        private int _pursueDistance = 150;
        //public bool pursueDistance { get { return (this.center - playerTarget.center).Length() < _pursueDistance;  } set { _pursueDistance = value; } }
        private int _avoidDistance = 600;
        public int hp;
        public bool avoidCheck { get { return (this.center - playerTarget.center).Length() > _avoidDistance; } set { avoidCheck =value; } }
        
        public Enemy(world worldParent, Event eventParent, Vector2 position) : base ()
        {
            this.worldParent = worldParent;
            State = state.stationary;
            this.position = position;
            center = position + centerPoint;
            reloadTime = 0;
            this.eventParent = eventParent;
            velocity = 4;
            hp = 5;
        }
        public new void Update()
        {
            switch(State)
            {
                case state.stationary:
                    if (scanForPlayer())
                        playerTarget = worldParent.player;
                    if (playerTarget != null )
                    {
                        if ((this is USSRRover) && !(this.eventParent is OneRover) && this.eventParent.enemyList.Count == 1 && !avoidCheck)
                            State = state.avoid;
                        if (!targDistance && pState != state.avoid)
                            State = state.seek;
                        if (gunRange)
                            this.fireWeapon();
                       
                    }
   

                    break;
                case state.patrol:
                    break;
                case state.avoid:
                    if(playerTarget!= null)
                    {
                        pState = state.avoid;
                        setHeading();
                        MoveAgent(-1);
                        if (avoidCheck)
                            State = state.stationary;
                    }
 
                    break;
                case state.inactive:
                    break;
                case state.guard:
                    pState = state.guard;
                    if (scanForPlayer())
                        playerTarget = worldParent.player;
                    State = state.seek;
                    break;

                case state.seek:
                    if (playerTarget == null)
                        if(scanForPlayer())
                            playerTarget = worldParent.player;
                        else
                        break;
                    if ((this is USSRRover) && !(this.eventParent is OneRover) && this.eventParent.enemyList.Count == 1)
                        State = state.avoid;
                        setHeading();
                        MoveAgent(1);
                        if (targDistance)
                            State = state.stationary;
                        if (gunRange)
                            this.fireWeapon();
                        if (pState == state.guard  && (this.center - this.eventParent.tileParent.location + new Vector2(32, 32)).Length() > 550)
                            State = state.retToGuard;
                        break;
                   
                case state.retToGuard:
                    targetNode = worldParent.worldMap.nodeArray[eventParent.tileParent.tileNodes[1, 1].X, eventParent.tileParent.tileNodes[1, 1].Y];
                    base.setHeading();
                    MoveAgent(1);
                    if ((this.center - targetNode.center).Length() < 75)
                        State = state.guard;
                    break;


            }
            base.Update();
            
        }
        public new void setHeading()
        {
            Vector2 positionVector = playerTarget.center - this.center;
            headingVector = Vector2.Normalize(positionVector);
            rotation = (float)Math.Acos(Vector2.Dot(headingVector, right));
        }
        public void isHit(int damage)
        {
            hp = (int)MathHelper.Clamp(hp - damage, 0, 1000);
            if (hp == 0)
            {
                this.die();
            }
        }
        public void die()
        {
            if (eventParent.enemyList.Count == 1)
                eventParent.shift = eventParent.tileParent.center - this.center;
                //eventParent.drawRectangle = new Rectangle((int)(this.center.X - sprite.Width / 2), (int)(this.center.Y - sprite.Height / 2), eventParent.drawRectangle.Width, eventParent.drawRectangle.Height);
            this.worldParent.enemyListR.Add(this);
            eventParent.enemyList.Remove(this);
        }
        public virtual void fireWeapon()
        {
            if(reloadTime % Projectile.reload == 0)
                worldParent.projectiles.Add(new Projectile(this, worldParent, Vector2.Normalize(playerTarget.center - this.center)));
            reloadTime++;
        }
        public new void MoveAgent(int direction)
        {
            base.MoveAgent(direction);
            foreach (Enemy enemy in worldParent.activeEnemies)
                if (enemy.collisionRectangle.Intersects(this.collisionRectangle) && enemy != this)
                {
                    base.MoveAgent(-direction);
                    break;
                }
        }
        public bool scanForPlayer()
        {
            if ((this.position - worldParent.player.position).Length() < detectionRange)
                return true;
            else
                return false;
        }
        public void setNodeHeading(node Node)
        {
            headingVector = Vector2.Normalize(Node.center - this.center);
        }
        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(agentSprite, collisionRectangle, Color.White);
        //}
    }
}
