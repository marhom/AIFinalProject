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
    class Accessory : Item
    {
        public Accessory(string name, string abbr)
            : base(name, abbr)
        {
            itemTexture = world.xTexture;
            id = "X";
            color = Color.Green;
        }
    }
}
