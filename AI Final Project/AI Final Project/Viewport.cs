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
    class Viewport
    {
        public Vector2 centerOfWindow;
        public world worldParent;
        public Vector2 originVector, cornerVector, centerOfDrawable, originMax, cornerMax;

        public Viewport(Vector2 centerOfWindow, world worldParent)
        {
            this.worldParent = worldParent;
            this.centerOfWindow = centerOfWindow;
            
        }
        public void initialize()
        {
            centerOfDrawable = new Vector2((1024) / 2, (768) / 2);
            originVector = worldParent.worldMap.worldMap[0, 0].location; //- centerOfDrawable;
            cornerVector = worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location + new Vector2(worldParent.worldMap.worldMap[0,0].groundTexture.Width,worldParent.worldMap.worldMap[0,0].groundTexture.Height); //- centerOfDrawable;
            originMax = originVector - Vector2.Zero;
            cornerMax = cornerVector - centerOfDrawable * 2;
        }
        public void Update()
        {
            checkScroll();

        }
        //public bool checkScroll()
        //{
        //    Vector2 directionVector = Vector2.Normalize(worldParent.player.directionVector * worldParent.player.curVel);
        //    if (worldParent.player.atEdge() == 0)
        //        return false;
        //    if (worldParent.player.atEdge() < 5)
        //    {
        //        Vector2 pointingVector = -worldParent.player.curVel * Vector2.Normalize(directionVector);
        //        if (directionVector.Y > 0 && directionVector.X > 0)
        //        pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).X,
        //            (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).X),
        //            MathHelper.Clamp(pointingVector.Y, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).Y,
        //            (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).Y));
        //        else if (directionVector.Y > 0)
        //            pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X,0  , -(worldParent.worldMap.worldMap[0, 0].location.X - originMax.X)),
        //            MathHelper.Clamp(pointingVector.Y, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).Y,
        //            (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).Y));
        //        else if (directionVector.X > 0)
        //            pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).X,
        //            (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).X),
        //            MathHelper.Clamp(pointingVector.Y, 0, -(worldParent.worldMap.worldMap[0, 0].location.Y + -cornerMax.Y)));
        //        else
        //            pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, 0, -(worldParent.worldMap.worldMap[0, 0].location.X - originMax.X)),
        //            MathHelper.Clamp(pointingVector.Y, 0, -(worldParent.worldMap.worldMap[0, 0].location.Y + - cornerMax.Y)));
        //        scrollShift(pointingVector);
        //        return true;
        //    }
        //    else if (worldParent.player.atEdge() < 10)
        //    {
        //        Vector2 pointingVector = -worldParent.player.curVel * Vector2.Normalize(directionVector);
        //        if (directionVector.Y > 0)
        //        pointingVector = new Vector2(0, MathHelper.Clamp(pointingVector.Y, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).Y,
        //            (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).Y));
        //        else
        //            pointingVector = new Vector2(0, MathHelper.Clamp(pointingVector.Y, 0, -(worldParent.worldMap.worldMap[0, 0].location.Y + - cornerMax.Y)));
        //        scrollShift(pointingVector);
        //        return true;
        //    }
        //    else if (worldParent.player.atEdge() < 15)
        //    {
        //        Vector2 pointingVector = -worldParent.player.curVel * Vector2.Normalize(directionVector);
        //        if (directionVector.X > 0)
        //            pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).X,
        //                (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).X), 0);
        //        else
        //            pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X,0  , -(worldParent.worldMap.worldMap[0, 0].location.X - originMax.X)),0);
        //        scrollShift(pointingVector);
        //        return true;
        //    }
        //    return false;
        //}
        public bool checkScroll()
        {
            if (worldParent.player.atEdge() == 0)
                return false;
            if (worldParent.player.atEdge() < 5)
            {
                Vector2 pointingVector = -worldParent.player.velocity * Vector2.Normalize(worldParent.player.directionVector);
                if (worldParent.player.directionVector.Y > 0 && worldParent.player.directionVector.X > 0)
                    pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).X,
                        (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).X),
                        MathHelper.Clamp(pointingVector.Y, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).Y,
                        (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).Y));
                else if (worldParent.player.directionVector.Y > 0)
                    pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, 0, -(worldParent.worldMap.worldMap[0, 0].location.X - originMax.X)),
                    MathHelper.Clamp(pointingVector.Y, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).Y,
                    (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).Y));
                else if (worldParent.player.directionVector.X > 0)
                    pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).X,
                    (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).X),
                    MathHelper.Clamp(pointingVector.Y, 0, -(worldParent.worldMap.worldMap[0, 0].location.Y + -cornerMax.Y)));
                else
                    pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, 0, -(worldParent.worldMap.worldMap[0, 0].location.X - originMax.X)),
                    MathHelper.Clamp(pointingVector.Y, 0, -(worldParent.worldMap.worldMap[0, 0].location.Y + -cornerMax.Y)));
                scrollShift(pointingVector);
                return true;
            }
            else if (worldParent.player.atEdge() < 10)
            {
                Vector2 pointingVector = -worldParent.player.velocity * Vector2.Normalize(worldParent.player.directionVector);
                if (worldParent.player.directionVector.Y > 0)
                    pointingVector = new Vector2(0, MathHelper.Clamp(pointingVector.Y, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).Y,
                        (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).Y));
                else
                    pointingVector = new Vector2(0, MathHelper.Clamp(pointingVector.Y, 0, -(worldParent.worldMap.worldMap[0, 0].location.Y + -cornerMax.Y)));
                scrollShift(pointingVector);
                return true;
            }
            else if (worldParent.player.atEdge() < 15)
            {
                Vector2 pointingVector = -worldParent.player.velocity * Vector2.Normalize(worldParent.player.directionVector);
                if (worldParent.player.directionVector.X > 0)
                    pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, (originMax + -cornerMax - worldParent.worldMap.worldMap[0, 0].location).X,
                        (-originMax + cornerMax + worldParent.worldMap.worldMap[worldParent.worldMap.worldMap.GetUpperBound(0), worldParent.worldMap.worldMap.GetUpperBound(1)].location).X), 0);
                else
                    pointingVector = new Vector2(MathHelper.Clamp(pointingVector.X, 0, -(worldParent.worldMap.worldMap[0, 0].location.X - originMax.X)), 0);
                scrollShift(pointingVector);
                return true;
            }
            return false;
        }

        public void scrollShift(Vector2 pointingVector)
        {
            foreach (tile Tile in worldParent.worldMap.worldMap)
            {
                Tile.location = Tile.location + pointingVector;
                Tile.Update();
            }
            foreach (Enemy enemy in worldParent.enemyList)
                enemy.position += pointingVector;
            foreach (Projectile projectile in worldParent.projectiles)
                projectile.position += pointingVector;
            worldParent.player.position = worldParent.player.position + pointingVector;
            worldParent.player.Update();

        }
        //public bool checkScroll()
        //{
        //    if (worldParent.player.atEdge() < 15 && worldParent.player.atEdge() != 0)
        //    {
        //        Vector2 pointingVector = -worldParent.player.baseVelocity * Vector2.Normalize(worldParent.player.directionVector);

        //        foreach (tile Tile in worldParent.worldMap.worldMap)
        //        {
        //            Tile.location = Tile.location + pointingVector;
        //            Tile.Update();
        //        }
        //        worldParent.player.position = worldParent.player.position + pointingVector;
        //        worldParent.player.Update();
        //        worldParent.enemy.position = worldParent.enemy.position + pointingVector;
        //        worldParent.enemy.Update();
        //        return true;
        //    }
        //    else
        //        return false;
        //}
    }

}
