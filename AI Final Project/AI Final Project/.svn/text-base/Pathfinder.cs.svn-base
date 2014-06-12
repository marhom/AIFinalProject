//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

//namespace AI_Final_Project
//{
//    static class Pathfinder
//    {
//        static world worldParent;


//        public static void findPath(Agent activeAgent, node curNode, node destNode)
//        {
//            impossible = false;
//            visited = new List<node>();
//            expanded = new List<node>();
//            adjacent = new List<node>();

//            /* %%% CHANGE TO START NODE ALG %%% */

//                expanded.Add(activeAgent.findClosest(worldParent.worldMap.nodeArray));
//                setOpened(activeAgent.findClosest(worldParent.worldMap.nodeArray));

//            while (!visited.Contains(destNode) && expanded.Count > 0 && !impossible)
//            {
//                curNode = expanded[0];
//                /* loop through opened nodes looking for lowest scoring one */
//                for (int i = 1; i < expanded.Count; i++)
//                    if (worldParent.worldMap.nodeArray[expanded[i].gridPosition.X, expanded[i].gridPosition.Y].getScore()
//                        <= worldParent.worldMap.nodeArray[curNode.gridPosition.X, curNode.gridPosition.Y].getScore())
//                        curNode = expanded[i];
//                /*once lowest scorer is found, go to it*/
//                expanded.Remove(curNode);
//                visited.Add(curNode);
//                /*look at neighboring nodes to find the lowest scoring node off of that node*/
//                adjacent = checkAdjacent(curNode, destNode);
//                /*update parents for lower-cost paths*/
//                for (int i = 0; i < adjacent.Count; i++)

//                    if (worldParent.worldMap.nodeArray[adjacent[i].gridPosition.X, adjacent[i].gridPosition.Y].getScore() > worldParent.worldMap.nodeArray[curNode.gridPosition.X, curNode.gridPosition.Y].getScore())
//                    {
//                        worldParent.worldMap.nodeArray[adjacent[i].gridPosition.X, adjacent[i].gridPosition.Y].parent = curNode;
//                        worldParent.worldMap.nodeArray[adjacent[i].gridPosition.X, adjacent[i].gridPosition.Y].setScore(curNode, destNode);
//                    }
//            }
//            if (visited.Contains(destNode))
//            {
//                makePath(curNode);
//                pathFound = true;
//            }
//            else
//            {
//                impossible = true;
//                path = new LinkedList<node>();
//                destNode = null;
//            }

//        }
//        public static void makePath(node currentNode)
//        {
//            path = new LinkedList<node>();
//            path.AddFirst(new node(destPosition, nodeTexture, -1, activeAgent.collisionRectangle, -1, -1));
//            do
//            {
//                path.AddFirst(currentNode);
//                setSelected(currentNode);
//                currentNode = currentNode.parent;
//            }
//            while (currentNode.parent != null);
//            path.AddFirst(new node(activeAgent.center, nodeTexture, -1, activeAgent.collisionRectangle, -1, -1));
//            donePath = false;

//        }
//        public static List<node> checkAdjacent(node curNode, node destNode)
//        {
//            List<node> temp = new List<node>();
//            foreach (Point adjNode in curNode.adjacentNodes)
//                if (visited.Contains(worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y]) == false && worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y].activeNode == true)
//                {
//                    if (expanded.Contains(worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y]))
//                        temp.Add(worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y]);
//                    else
//                    {
//                        worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y].parent = curNode;
//                        worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y].setScore(curNode, destNode);
//                        expanded.Add(worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y]);
//                        setOpened(worldParent.worldMap.nodeArray[adjNode.X, adjNode.Y]);
//                    }
//                }
//            return temp;
//        }
//        public static void movePath()
//        {
//            if (path.Count == 0)
//            {
//                donePath = true;
//                activeAgent.clearTarget();
//            }
//            else if (donePath == false && activeAgent.targetNode == null)
//            {
//                //activeAgent.center = path.First().center;
//                //path.RemoveFirst();
//                activeAgent.targetNode = path.First();
//                path.RemoveFirst();
//                pauseMove = true;
//            }
//            else if (activeAgent.agentArrived())
//            {
//                activeAgent.targetNode = path.First();
//                path.RemoveFirst();
//                pauseMove = true;
//            }

//        }
//        public static void clearSelected()
//        {
//            foreach (node Node in worldParent.worldMap.nodeArray)
//            {
//                Node.isSelected = false;
//                Node.parent = null;
//                Node.isOpened = false;

//            }
//        }
//    }
//}
