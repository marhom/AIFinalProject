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
    class Button
    {
        public Texture2D normal, hover, clicked;
        public SpriteFont font;
        public Rectangle drawRectangle;
        public controlHandler controlhandler;
        public String text;
        public bool toggle;
        public int id;

        public Button(Rectangle drawRectangle, Texture2D normal, SpriteFont font, String text, int id, ref controlHandler controlhandler)
        {
            this.normal = normal;
            this.controlhandler = controlhandler;
            this.drawRectangle = drawRectangle;
            toggle = false;
            this.text = text;
            this.font = font;
            this.id = id;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(normal, drawRectangle, Color.White);
            spriteBatch.DrawString(font, text, new Vector2(drawRectangle.X + (drawRectangle.Width - font.MeasureString(text).X) / 2, drawRectangle.Y + (drawRectangle.Height - font.MeasureString(text).Y) / 2), Color.White);

        }
    }
}
