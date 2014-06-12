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
    class tile
    {
        public bool isBuildable { get; set; } 
        public Vector2 location { get; set; }
        public Texture2D groundTexture { get; set; }
        public Vector2 corner { get; set; }
        public Point gridPosition;
        public Point[,] tileNodes;
        public Event eventChild;
        public Color color;
        public world worldParent;
        public Vector2 _center;
        public Vector2 center { get { return new Vector2(location.X + groundTexture.Width / 2, location.Y + groundTexture.Height / 2); } set { _center = value; } }
        
        #region DEBUG
        public int debugNum;
        #endregion

        public tile()
        {
            location = new Vector2(0, 0);
            groundTexture = null;
            corner = new Vector2(0, 0);
            gridPosition = new Point(0, 0);
            tileNodes = new Point[3, 3];
            color = Color.White;
            eventChild = null;
        }
        
        public tile(Vector2 location, Texture2D groundTexture, int debugNum, int x, int y, world worldParent)
        {
           
            this.location = location;
            this.groundTexture = groundTexture;
            this.debugNum = debugNum;
            this.corner = new Vector2(location.X + groundTexture.Width, location.Y + groundTexture.Height);
            this.gridPosition = new Point(x, y);
            this.worldParent = worldParent;
            this.color = Color.White;
            this.center = new Vector2(location.X + groundTexture.Width / 2, location.Y + groundTexture.Height / 2);
            tileNodes = new Point[3, 3];

        }
        public void Update()
        {
            corner = location + new Vector2(groundTexture.Width, groundTexture.Height);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont consoleFont, Color color)
        {
                    spriteBatch.Draw(this.groundTexture, this.location, this.color);
                    //Vector2 stringSize = consoleFont.MeasureString("" + this.debugNum);
                    //spriteBatch.DrawString(consoleFont, "" + this.debugNum, new Vector2(this.location.X + this.groundTexture.Width / 2 - stringSize.X / 2, this.location.Y + this.groundTexture.Height / 2 - stringSize.Y / 2), Color.Gold);

        }
        public bool eventOverlap()
        {
            for(int x = 0; x < 2; x++)
                for (int y = 0; y < 2; y++)
                {
                    if (!(this.worldParent.worldMap.worldMap[(int)MathHelper.Clamp(gridPosition.X + x, 0, worldParent.worldMap.gridMax.X - 1), (int)(int)MathHelper.Clamp(gridPosition.Y + y, 0, worldParent.worldMap.gridMax.Y - 1)].eventChild == null && this.worldParent.worldMap.worldMap[(int)(int)MathHelper.Clamp(gridPosition.X - x, 0, worldParent.worldMap.gridMax.X - 1), (int)MathHelper.Clamp(gridPosition.Y - y, 0, worldParent.worldMap.gridMax.Y - 1)].eventChild == null
                        && this.worldParent.worldMap.worldMap[(int)MathHelper.Clamp(gridPosition.X - x, 0, worldParent.worldMap.gridMax.X - 1), (int)MathHelper.Clamp(gridPosition.Y + y, 0, worldParent.worldMap.gridMax.Y - 1)].eventChild == null && this.worldParent.worldMap.worldMap[(int)MathHelper.Clamp(gridPosition.X + x, 0, worldParent.worldMap.gridMax.X - 1), (int)MathHelper.Clamp(gridPosition.Y - y, 0, worldParent.worldMap.gridMax.Y - 1)].eventChild == null))
                        return true;
                }
            return false;
        }
    }
}
