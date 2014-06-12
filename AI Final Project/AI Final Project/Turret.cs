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
    class Turret : Enemy
    {
        public Turret(world worldParent, Event eventParent, Vector2 position) : base(worldParent, eventParent, position) 
        { 
            velocity = 0;
            tint = Color.DarkSlateGray;
        }
        public override void fireWeapon()
        {
            if (reloadTime % Projectile.reload == 0)
            {
                worldParent.projectiles.Add(new Projectile(this, worldParent, Vector2.Normalize(playerTarget.center - this.center)));
                Matrix rotMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(25));
                worldParent.projectiles.Add(new Projectile(this, worldParent, Vector2.Transform(Vector2.Normalize(playerTarget.center - this.center), rotMatrix)));
                rotMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(-25));
                worldParent.projectiles.Add(new Projectile(this, worldParent, Vector2.Transform(Vector2.Normalize(playerTarget.center - this.center), rotMatrix)));
            }
            reloadTime++;
        }
    }
}
