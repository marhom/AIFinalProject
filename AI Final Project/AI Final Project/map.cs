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
    class map
    {
        //public LinkedList<LinkedList<tile>> worldMap;
        public tile[,] worldMap;
        public node[,] nodeArray;
        public world worldParent;
        private int tileWidth, tileHeight, totalNodes, count;
        public tile goalTile;
        public Point gridMax;
        public int eventLine;
        public map(world worldParent)
        {
            this.worldParent = worldParent;
        }

        public void initialize(int width, int height, List<Texture2D> textureList)
        {
            tileWidth = width / textureList[0].Width;
            tileHeight = height / textureList[0].Height;

            gridMax = new Point(2*tileWidth, 3 * tileHeight);

            GameTime gameTime = new GameTime();
            Random random = new Random();
            worldMap = new tile[ gridMax.X, gridMax.Y];
            nodeArray = new node[3 * gridMax.X, 3 * gridMax.Y];
            count = 0;
            for (int x = 0; x < (gridMax.X); x++)
                for (int y = 0; y < (gridMax.Y); y++)
                {
                    worldMap[x, y] = new tile(new Vector2(x * textureList[0].Width, y * textureList[0].Height - gridMax.Y * textureList[0].Height + 768),
                        textureList[random.Next(textureList.Count)], y * tileWidth + x + 1, x, y, worldParent);

                }
            for (int x = 0; x < 3; x++)
                for (int y = gridMax.Y - 1; y < gridMax.Y; y++)
                    worldMap[x, y].groundTexture = worldParent.gameParent.Content.Load<Texture2D>("Art\\Textures\\Surface\\Crash\\crash" + (x + y + 2 - gridMax.Y));
                    goalTile = worldMap[gridMax.X/2 ,0];
            //goalTile.color = Color.Red;
            eventLine = ((int)gridMax.Y * textureList[0].Height - 2 * 768) / 5;
            generateEvents();
            worldParent.eventList.Add(new KremlinEvent(worldParent, goalTile));


        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont consoleFont, controlHandler mouse)
        {
            foreach (tile gridTile in worldMap)
            {
                if ((gridTile.location.X > -60 && gridTile.location.X < 1200) && (gridTile.location.Y > -60 && gridTile.location.Y < 900) || (gridTile.location.X + gridTile.groundTexture.Width > -60 && gridTile.location.X + gridTile.groundTexture.Width < 1200) && (gridTile.location.Y + gridTile.groundTexture.Height > -60 && gridTile.location.Y + gridTile.groundTexture.Height < 900))
                    gridTile.Draw(spriteBatch, consoleFont, Color.White);
            }
            if (false) //###DEBUG: Change to true for node drawing
                foreach (node Node in nodeArray)
                    if (Node != null && (Node.center.X > -25 && Node.center.X < 1060) && (Node.center.Y > -25 && Node.center.Y < 800)) //|| (Node.center.X + gridTile.groundTexture.Width > -25 && Node.center.X + gridTile.groundTexture.Width < 1060) && (Node.center.Y + gridTile.groundTexture.Height > -25 && Node.center.Y + gridTile.groundTexture.Height < 800))
                        Node.Draw(spriteBatch);
        }
        public void generateNodeArray()
        {
            tile parentTemp = worldMap[0, 0];
            for (int j = 0; j < 3 * gridMax.Y; j++)
            {
                for (int i = 0; i < 3 * gridMax.X; i++)
                {
                    if (i % 3 == 0 && i != 0)
                        parentTemp = worldMap[parentTemp.gridPosition.X + 1, parentTemp.gridPosition.Y];
                    nodeArray[i, j] = new node(new Vector2(i * worldParent.textureList[0].Width / 3, j * worldParent.textureList[0].Height / 3) + worldParent.viewport.originMax, Agent.sprite, totalNodes++, worldParent.player.collisionRectangle, i, j, worldMap[0, 0], worldParent);
                    nodeArray[i, j].tileParent = parentTemp;
                    parentTemp.tileNodes[i % 3, j % 3] = new Point(i, j);
                    //foreach (Wall wall in wallList)
                    //    if (nodeArray[i, j].collisionRectangle.Intersects(wall.boundingRectangle))
                    //        nodeArray[i, j].activeNode = false;
                }
                if (j % 3 == 0 && j != 0)
                    parentTemp = worldMap[0, parentTemp.gridPosition.Y + 1];
                else
                    parentTemp = worldMap[0, parentTemp.gridPosition.Y];
            }
            for (int i = 0; i < 3 * gridMax.X; i++)
                for (int j = 0; j < 3 * gridMax.Y; j++)
                {
                    if (i != 0 && j != 0)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i - 1, j - 1));
                    if (i != 0)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i - 1, j));
                    if (i != 0 && j != 60)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i - 1, j + 1));
                    if (i != 80 && j != 0)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i + 1, j - 1));
                    if (i != 80)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i + 1, j));
                    if (i != 80 && j != 60)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i + 1, j + 1));
                    if (j != 0)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i, j - 1));
                    if (j != 60)
                        nodeArray[i, j].adjacentNodes.AddFirst(new Point(i, j + 1));
                }
        }//generateNodeArray
        public void generateEvents()
        {
            eventTree eventtree = new eventTree(this.worldParent);
            eventtree.findEvents();
            Random random = new Random();
            tile eventTile;
            foreach (Event events in eventtree.pushList)
            {
                do
                {
                    eventTile = worldMap[(random.Next(1, gridMax.X - 1)),
                    (int)Math.Floor(((double)(random.Next((gridMax.Y * worldMap[0, 0].groundTexture.Height + (int)worldMap[0, 0].location.Y - 768 - (eventLine * (world.eventStep)) - 400),
                    (gridMax.Y * worldMap[0, 0].groundTexture.Height + (int)worldMap[0, 0].location.Y - 768 - eventLine * world.eventStep - 200)) - worldMap[0, 0].location.Y) / worldMap[0, 0].groundTexture.Height))];
                } while (eventTile.eventChild != null || eventTile.eventOverlap());
                if (events is OneRover)
                    worldParent.eventList.Add(new OneRover(worldParent, eventTile));
                if (events is OneTurret)
                    worldParent.eventList.Add(new OneTurret(worldParent, eventTile));
                if (events is TwoTurrets)
                    worldParent.eventList.Add(new TwoTurrets(worldParent, eventTile));
                if (events is TwoRovers)
                    worldParent.eventList.Add(new TwoRovers(worldParent, eventTile));
                if (events is twoEnemies)
                    worldParent.eventList.Add(new twoEnemies(worldParent, eventTile));
                if (events is FourTurrets)
                    worldParent.eventList.Add(new FourTurrets(worldParent, eventTile));
                if (events is OneTank)
                    worldParent.eventList.Add(new OneTank(worldParent, eventTile));
                if (events is TwoTanks)
                    worldParent.eventList.Add(new TwoTanks(worldParent, eventTile));
                if (events is ThreeRovers)
                    worldParent.eventList.Add(new ThreeRovers(worldParent, eventTile));
                if (events is FourRovers)
                    worldParent.eventList.Add(new FourRovers(worldParent, eventTile));
                if (events is Tank3Turrets)
                    worldParent.eventList.Add(new Tank3Turrets(worldParent, eventTile));

                //if (events is OneTank)
                //    worldParent.eventList.Add(new OneTank(worldParent, eventTile));
                //if (events is TwoTanks)
                //    worldParent.eventList.Add(new TwoTanks(worldParent, eventTile));


            }



            //for
            //switch (random.Next(0, 6))
            //{

            //    case 0:
            //        worldParent.eventList.Add(new OneRover(worldParent, eventTile));
            //        break;
            //    case 1:
            //        worldParent.eventList.Add(new OneTurret(worldParent, eventTile));
            //        break;
            //    case 2:
            //        worldParent.eventList.Add(new TwoTurrets(worldParent, eventTile));
            //        break;
            //    case 3:
            //        worldParent.eventList.Add(new twoEnemies(worldParent, eventTile));
            //        break;
            //    case 4:
            //        worldParent.eventList.Add(new TwoRovers(worldParent, eventTile));
            //        break;
            //    case 5:
            //        worldParent.eventList.Add(new FourTurrets(worldParent, eventTile));
            //        break;
            //        OneRover.classSpawned = true;
            //        OneTurret.classSpawned = true;
            //}

            //        }
            //        else
            //        {
            //            eventtree.findEvents();
            //            switch (random.Next(0, 6))
            //            {
            //                //case 0:
            //                //    worldParent.eventList.Add(new Event(worldParent, worldMap[(random.Next(0, gridMax.X)), (int)Math.Floor((double)(random.Next((gridMax.Y * textureList[0].Height - eventLine - 400), (gridMax.Y * textureList[0].Height - eventLine - 200)) / textureList[0].Height))]));
            //                //    break;
            //                case 0:
            //                    worldParent.eventList.Add(new OneRover(worldParent, eventTile));
            //                    break;
            //                case 1:
            //                    worldParent.eventList.Add(new OneTurret(worldParent, eventTile));
            //                    break;
            //                case 2:
            //                    worldParent.eventList.Add(new TwoTurrets(worldParent, eventTile));
            //                    break;
            //                case 3:
            //                    worldParent.eventList.Add(new twoEnemies(worldParent, eventTile));
            //                    break;
            //                case 4:
            //                    worldParent.eventList.Add(new TwoRovers(worldParent, eventTile));
            //                    break;
            //                case 5:
            //                    worldParent.eventList.Add(new FourTurrets(worldParent, eventTile));
            //                    break;
            //            }
            //        }
            //    }
            //    else
            //        x--;

            //}
            world.eventStep++;
        }

    }
}
