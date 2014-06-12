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
    /*controlhandler:
     * Provides for mouse control and button interaction
     */
    class controlHandler
    {
        private Texture2D cursorTexture, targetTexture;
        public Vector2 position;
        private MouseState mouseState, prevMState;
        public KeyboardState keyState, prevKState;

        //public controlhandler(Vector2 position, Texture2D cursorTexture)
        //{
        //    this.position = position; 
        //    this.cursorTexture = cursorTexture; 
        //}

        public controlHandler(Texture2D cursorTexture, Texture2D targetTexture)
        {
            this.cursorTexture = cursorTexture;
            this.targetTexture = targetTexture;
            mouseState = Mouse.GetState();
            position.X = mouseState.X;
            position.Y = mouseState.Y;
        }
            
        
        public void Update()
        {
            prevMState = mouseState;
            prevKState = keyState;
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            this.position.X = (int) MathHelper.Clamp(mouseState.X,0,1024);
            this.position.Y = (int) MathHelper.Clamp(mouseState.Y,0,768);
        }
        
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(this.cursorTexture, this.position, Color.White); 
        }
        public bool overObject(Button button)
        {
            if (this.position.X >= button.drawRectangle.X && this.position.Y >= button.drawRectangle.Y && this.position.Y < button.drawRectangle.Bottom && this.position.X < button.drawRectangle.Right)
                return true;
            else
                return false;
        }
        public bool overObject(tile Tile)
        {
            if (this.position.X >= Tile.location.X && this.position.Y >= Tile.location.Y && this.position.Y < Tile.corner.Y && this.position.X < Tile.corner.X)
                return true;
            else
                return false;
        }
        public MouseState getState()
        {
            return mouseState;
        }
        public MouseState getPrevState()
        {
            return prevMState;
        }
        public bool atEdge()
        {
            if (position.X < 10 || position.X > 1015 || position.Y < 10 || position.Y > 760)
                return true;
            else
                return false;
        }
        public Vector2 positionVector(Vector2 vector)
        {
            return position - vector;
        }
        public bool wasPressed(Keys key)
        {
            if (prevKState.IsKeyUp(key) && keyState.IsKeyDown(key))
                return true;
            else
                return false;
        }
    }
}
