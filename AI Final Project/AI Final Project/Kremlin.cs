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
    class Kremlin : Enemy
    {
        private new Texture2D sprite;
        public Kremlin(world worldParent, Event eventParent, Vector2 position)
            : base(worldParent, eventParent, position)
        {
            this.sprite = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\kremlin");
            centerPoint = new Vector2(sprite.Width / 2, sprite.Height / 2);
            center = position + centerPoint;
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, 3 * 64, 3 * 64);
            velocity = 0;
            State = state.guard;
            tint = Color.Red;
            reloadTime = 30;
            hp = 100;
        }
        public override void fireWeapon()
        {
            if (reloadTime % Cannon.reload == 0)
            {
                worldParent.projectiles.Add(new Cannon(this, worldParent, Vector2.Normalize(playerTarget.center - this.center)));
            }
            reloadTime++;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (hp > 35 * 2 / 3)
                color = Color.DarkGreen;
            else if (hp > 35 / 3)
                color = Color.Gold;
            else
                color = Color.DarkRed;
            spriteBatch.Draw(world.blank, new Rectangle((int)this.center.X - 50, (int)this.position.Y - 30, 100, 16), Color.Gray);
            spriteBatch.Draw(world.blank, new Rectangle((int)this.center.X - 48, (int)this.position.Y - 28, (int)(((float)hp) / 100 * 100) - 4, 12), color);
            base.Draw(spriteBatch);
        }
    }
}
