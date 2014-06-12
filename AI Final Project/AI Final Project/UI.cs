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
    class UI
    {
 
        public world worldParent;
        public Texture2D buttonTexture, blank;
        //public SpriteFont consoleFont;
        public UI(world worldParent)
        {
            this.worldParent = worldParent;
        }
        public void initialize()
        {
            buttonTexture = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\UI\\button");
        }
        public void Draw(SpriteBatch spritebatch)
        {
            //spritebatch.DrawString(worldParent.consoleFont, worldParent.player.ToString() + "\n" + worldParent.worldMap.worldMap[0,0].location + "\n" + worldParent.worldMap.nodeArray[0,0].center, new Vector2(5, 5), Color.White);
            float measureString = 1024 - worldParent.consoleFont.MeasureString("HP: " + worldParent.player.hp + " WEP: " + ((worldParent.player.weapon == null) ? "---" : worldParent.player.weapon.abbr) + " ARM: " + ((worldParent.player.armor == null) ? "---" : worldParent.player.armor.abbr) + " ACC: " + ((worldParent.player.accessory == null) ? "---" : worldParent.player.accessory.abbr)).X;
            spritebatch.DrawString(worldParent.consoleFont, "HP: " + worldParent.player.hp + " WPN: " + ((worldParent.player.weapon == null) ? "---" : worldParent.player.weapon.abbr) + " ARM: " + ((worldParent.player.armor == null) ? "---" : worldParent.player.armor.abbr) + " ACC: " + ((worldParent.player.accessory == null) ? "---" : worldParent.player.accessory.abbr), new Vector2(1024 - worldParent.consoleFont.MeasureString("HP: " + worldParent.player.hp + " WEP: " + ((worldParent.player.weapon == null) ? "---" : worldParent.player.weapon.abbr) + " ARM: " + ((worldParent.player.armor == null) ? "---" : worldParent.player.armor.abbr) + " ACC: " + ((worldParent.player.accessory == null) ? "---" : worldParent.player.accessory.abbr)).X, 768 - 50), Color.WhiteSmoke);
            if(worldParent.player.healthGot)
            {
                spritebatch.DrawString(worldParent.consoleFont, "+5", new Vector2(measureString + worldParent.consoleFont.MeasureString("HP: " + worldParent.player.hp).X - worldParent.consoleFont.MeasureString("" + worldParent.player.hp).X / 2, 768 - 60 - 90 + worldParent.player.hdispTimer/2), new Color(0, 128, 0, ((float)worldParent.player.hdispTimer / 180)));
                if (worldParent.player.hdispTimer == 0)
                    worldParent.player.healthGot = false;
                worldParent.player.hdispTimer--;
            }
            if(worldParent.player.armorGot)
            {
                spritebatch.DrawString(worldParent.consoleFont, worldParent.player.armor.name + " Armor Acquired!", new Vector2(1024 / 2 - worldParent.consoleFont.MeasureString(worldParent.player.armor.name + " Armor Acquired!").X / 2, 300), new Color(250, 235, 215, ((float)worldParent.player.dispTimer / 180)));
                if (worldParent.player.dispTimer == 0)
                    worldParent.player.armorGot = false;
                worldParent.player.dispTimer--;
            }
            if (worldParent.player.weaponGot)
            {
                spritebatch.DrawString(worldParent.consoleFont, worldParent.player.weapon.name + " Acquired!", new Vector2(1024 / 2 - worldParent.consoleFont.MeasureString(worldParent.player.weapon.name + " Acquired!").X / 2, 300), new Color(250, 235, 215, ((float)worldParent.player.dispTimer / 180)));
                if (worldParent.player.dispTimer == 0)
                    worldParent.player.weaponGot = false;
                worldParent.player.dispTimer--;
            }
            if (worldParent.player.accGot)
            {
                spritebatch.DrawString(worldParent.consoleFont, worldParent.player.accessory.name + " Acquired!", new Vector2(1024 / 2 - worldParent.consoleFont.MeasureString(worldParent.player.accessory.name + " Acquired!").X / 2, 300), new Color(250, 235, 215, ((float)worldParent.player.dispTimer / 180)));
                if (worldParent.player.dispTimer == 0)
                    worldParent.player.accGot = false;
                worldParent.player.dispTimer--;
            }
        }
        public void Update()
        {
           
        }
    }

}
