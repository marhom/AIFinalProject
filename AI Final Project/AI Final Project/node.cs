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
    class node
    {
        public Point gridPosition;
        private Vector2 _center;
        public Vector2 center { get { return new Vector2(worldParent.worldMap.worldMap[0, 0].location.X + tileParent.groundTexture.Width / 6 + 
            (nodeNumber % (3 * worldParent.worldMap.gridMax.X)) * worldParent.worldMap.worldMap[0, 0].groundTexture.Width / 3, 
            (float)(worldParent.worldMap.worldMap[0, 0].location.Y + tileParent.groundTexture.Height / 6 
            + Math.Floor((double)(nodeNumber / (3 * worldParent.worldMap.gridMax.X))) * worldParent.worldMap.worldMap[0, 0].groundTexture.Width / 3)); } set { _center = value; } }
        public Texture2D texture;
        private Rectangle _drawRectangle;
        public Rectangle drawRectangle { get { return new Rectangle((int)(center.X - _drawRectangle.Width / 2), (int)(center.Y - _drawRectangle.Height / 2), _drawRectangle.Width, _drawRectangle.Height);} set { _drawRectangle = value;}}
        public bool isSelected;
        private Rectangle _collisionRectangle;
        public Rectangle collisionRectangle { get { return new Rectangle((int)center.X - _collisionRectangle.Width / 2, (int)center.Y - _collisionRectangle.Height / 2, _collisionRectangle.Width, _collisionRectangle.Height); } set { _collisionRectangle = value; } }
        public bool activeNode;
        public node parent;
        public LinkedList<Point> adjacentNodes;
        public int nodeNumber;
        public Color color;
        public int score;
        public int mDistance;
        public bool isOpened;
        public tile tileParent;
        public world worldParent;
        public node(Vector2 center, Texture2D texture, int nodeNumber, Rectangle collisionRectangle, int x, int y, tile tileParent, world worldParent)
        {
            this.nodeNumber = nodeNumber;
            this.worldParent = worldParent;
            this.center = center;
            this.texture = texture;
            this.collisionRectangle = new Rectangle((int)center.X - collisionRectangle.Width/2, (int)center.Y - collisionRectangle.Height/2, collisionRectangle.Width, collisionRectangle.Height);
            this.activeNode = true;
            this.adjacentNodes = new LinkedList<Point>();
            this.gridPosition = new Point(x, y);
            this.score = 10;
            this.mDistance = 0;
            this.isOpened = false;
            color = Color.Black;
            this.tileParent = tileParent;
            this.worldParent = worldParent;
            //position = new Vector2(center.X - texture.Width / 2, center.Y - texture.Height / 2);
            //drawRectangle = new Rectangle((int)(center.X - 3), (int) (center.Y - 3), 6, 6);
            drawRectangle = new Rectangle((int)(center.X - tileParent.groundTexture.Width / 6), (int)(center.Y - tileParent.groundTexture.Height / 6), tileParent.groundTexture.Width / 3, tileParent.groundTexture.Height / 3);
            
        }
        //public node(Vector2 center, Texture2D texture, int nodeNumber, Rectangle collisionRectangle, int x, int y, Color color)
        //{
        //    this.nodeNumber = nodeNumber;
        //    this.center = center;
        //    this.texture = texture;
        //    this.collisionRectangle = new Rectangle((int)center.X - collisionRectangle.Width / 2, (int)center.Y - collisionRectangle.Height / 2, collisionRectangle.Width, collisionRectangle.Height);
        //    this.activeNode = true;
        //    this.adjacentNodes = new LinkedList<Point>();
        //    this.gridPosition = new Point(x, y);
        //    this.color = color;
        //    //position = new Vector2(center.X - texture.Width / 2, center.Y - texture.Height / 2);
        //    //drawRectangle = new Rectangle((int)(center.X - 3), (int) (center.Y - 3), 6, 6);
        //    drawRectangle = new Rectangle((int)(center.X - texture.Width / 8), (int)(center.Y - texture.Height / 8), texture.Width / 4, texture.Height / 4);

        //}
        public int getScore()
        {
            return mDistance +  score;
 
        }
        public void setScore(node startingNode, node targetNode)
        {
            mDistance = (int)manhattanDistance(targetNode);
            //score = (parent == null ? 0 : ((targetNode.center.X != center.X && targetNode.center.Y != center.Y) ?  4 + parent.score : 0 + parent.score));
            score = parent.score + 10;
        }
        public void heuristicCalc(node targetNode)
        {
            score = /*(int)manhattanDistance(targetNode) +*/ (parent == null ? 10 : 10 + parent.score);
        }
        public float manhattanDistance(node targetNode)
        {
            return Math.Abs((center.X - targetNode.center.X)) + Math.Abs((center.Y - targetNode.center.Y));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (activeNode)
                if (isOpened)
                {
                    //if(World.drawOpened)
                    spriteBatch.Draw(texture, drawRectangle, Color.Black /*(World.drawOpened ? Color.Blue : Color.Black)*/);

                    if (isSelected)
                    {
                        spriteBatch.Draw(texture, drawRectangle, Color.Red);

                    }
                    //else if (isSelected)
                    //    spriteBatch.Draw(texture, drawRectangle, color);
                }
                else
                    spriteBatch.Draw(texture, drawRectangle, color);
        }
        public override string ToString()
        {
            return nodeNumber + " Node Center: " + center.ToString() + " Node Score: " + getScore();
        }
        public void Update()
        {
            drawRectangle = new Rectangle((int)(center.X - drawRectangle.Width / 2), (int)(center.Y - drawRectangle.Height / 2), drawRectangle.Width, drawRectangle.Height);
            collisionRectangle = new Rectangle((int)center.X - collisionRectangle.Width / 2, (int)center.Y - collisionRectangle.Height / 2, collisionRectangle.Width, collisionRectangle.Height);
        }
    }
}
