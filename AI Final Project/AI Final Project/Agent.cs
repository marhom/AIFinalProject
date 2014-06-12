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
    class Agent
    {
        public static Texture2D sprite;
        public Vector2 position;
        public Vector2 centerPoint;
        public Vector2 center;
        public Vector2 headingVector, directionVector;
        public float rotation;
        public Color tint;
        public double heading;
        public bool isDetected;
        public int velocity, curVel, baseVelocity;
        public float rotationScalar;
        public node targetNode;
        public Agent targetPlayer;
        private Rectangle _collisionRectangle;
        public Rectangle collisionRectangle { get { return new Rectangle((int)position.X, (int)position.Y, _collisionRectangle.Width, _collisionRectangle.Height); } set { _collisionRectangle = value; } }
        protected Vector2 up = new Vector2(0, -1);
        protected Vector2 right = new Vector2(1, 0);

        public Agent()
        {
            //sprite = agentSprite;
            position = new Vector2(200, 200);
            rotation = 0.0f;
            rotationScalar = .1f;
            baseVelocity = 5;
            velocity = baseVelocity;
            tint = Color.Black;
            centerPoint = new Vector2(sprite.Width / 2, sprite.Height / 2);
            center = position + centerPoint;
            heading = rotation * 180 / Math.PI;
            isDetected = false;
            targetNode = null;
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            curVel = 0;
            
        }
        

        public Vector2 Position
        {
            get { return position; }
        }
        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Color Tint
        {
            get { return tint; }
            set { tint = value; }
        }
        public void Update()
        {
            //if(!agentArrived())
            //    move();
            //this.collisionRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height); 
            this.center = position + centerPoint;
            //this.position = center - centerPoint;
            this.center.X = (int)center.X;
            this.center.Y = (int)center.Y;
            this.rotation = (float)(rotation % (2 * Math.PI));
            this.heading = rotation * 180 / Math.PI + 90;
            if (this.heading < 0)
                heading = 360 + heading;
            Matrix rotMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians((float)this.heading));
            headingVector = Vector2.Transform(up, rotMatrix);
        }
        public void MoveAgent(int direction)
        {

            this.position = direction * velocity * headingVector + position;
        }
        public void rotateAgent(int direction)
        {
            this.rotation = direction * rotationScalar + rotation;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, center, null, tint, rotation, centerPoint, 1, SpriteEffects.None, 0);
            //spriteBatch.Draw()
            //spriteBatch.Draw(sprite, this.collisionRectangle,null, tint, this.rotation, Vector2.Zero, SpriteEffects.None, 0);
            //spriteBatch.Draw(this.Sprite, this.collisionRectangle, Color.White);
        }
        public void radialCheck(sensorData sensors, LinkedList<Agent> stationaryAgents, int detectionRadius)
        {
            sensors.Initialize();
            foreach (Agent obj in stationaryAgents)
            {
                obj.isDetected = false;
                Vector2 posVec = obj.center - this.center;
                float relAngle = (float)Math.Atan2(posVec.Y, posVec.X);
                float delAngle = MathHelper.ToDegrees((float)MathHelper.WrapAngle(relAngle - this.rotation)) + 180;
                if ((posVec).Length() <= detectionRadius)
                {
                    obj.isDetected = true;
                    sensors.distanceReport++;
                    posVec = Vector2.Normalize(posVec);
                    double posDir = MathHelper.ToDegrees((float)Math.Acos(posVec.X));
                    if (delAngle >= 315 || delAngle <= 45)
                        sensors.sCount++;
                    if (delAngle > 225 && delAngle < 315)
                        sensors.eCount++;
                    if (delAngle > 45 && delAngle < 135)
                        sensors.wCount++;
                    if (delAngle >= 135 && delAngle <= 225)
                        sensors.nCount++;

                }
            }
        }
        public void setHeading()
        {
            Vector2 positionVector = targetNode.center - this.center;
            headingVector = Vector2.Normalize(positionVector);
            rotation = (float)Math.Acos(Vector2.Dot(headingVector, right));
            //if (position.X > targetNode.center.X && position.Y > targetNode.center.Y)
            //{
            //    rotation = MathHelper.WrapAngle(rotation + MathHelper.Pi/2);
            //}

        }
        public void setTarget(node Node)
        {
            this.targetNode = Node;
        }
        public void clearTarget()
        {
            this.targetNode = null;
        }
        public void move()
        {
            if(targetNode != null)
            {
                    setHeading();
                    MoveAgent(1);
            }
        }
        public bool agentArrived()
        {
           if (targetNode != null && Math.Abs((center - targetNode.center).Length()) <= velocity * 3.5)
            //if(targetNode != null && collisionRectangle.Intersects(targetNode.drawRectangle))
            //if(targetNode != null && collisionRectangle.Location == targetNode.drawRectangle.Location)
            {
                position = targetNode.center - centerPoint;
                return true;
            }
            else return false;
            //if(targetNode != null)
            //    if (collisionRectangle.Intersects(targetNode.drawRectangle))
            ////if (collisionRectangle.Bottom >= targetNode.position.Y || collisionRectangle.Right >= targetNode.position.X)
            //        return true;
            //return false;
        }
        public node findClosest(node[,] nodeArray)
        {
            node temp = null;
            float distance = 0;
            foreach (node Node in nodeArray)
            {
                if (Math.Abs((center - Node.center).Length()) == 0)
                    return Node;
                if (temp == null)
                {
                    temp = Node;
                    distance = Math.Abs((center - Node.center/*position - new Vector2(Node.drawRectangle.X, Node.drawRectangle.Y*/).Length());
                }
                else
                {
                    if (distance > Math.Abs((center - Node.center/*position - new Vector2(Node.drawRectangle.X, Node.drawRectangle.Y*/).Length()))
                    {
                        temp = Node;
                        distance = Math.Abs((center - Node.center)/*(position - new Vector2(Node.drawRectangle.X, Node.drawRectangle.Y)*/.Length());
                    }
                }
            }
            return temp;
        }
        public override string ToString()
        {
            return "Center: " + center + "\nHeading: " + headingVector + "\nCurVel: " + curVel + "\ndirVec: " + directionVector; 
        }

    }
}
namespace AI_Final_Project
{
    class sensorData
    {
        public int nCount;
        public int wCount;
        public int eCount;
        public int sCount;
        public int distanceReport;

        public void Initialize()
        {
            nCount = 0;
            wCount = 0;
            eCount = 0;
            sCount = 0;
            distanceReport = 0;
        }


    }
}
