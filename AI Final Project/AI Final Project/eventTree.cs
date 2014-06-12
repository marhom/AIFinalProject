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
    class eventTree
    {
        public List<Event> goalCriteria, masterList, spawnList, pushList, negList, altList, negspawnList;
        public List<double> chanceList, nchanceList;
        public world worldParent;
        public double chance;

        public eventTree(world worldParent)
        {
            this.worldParent = worldParent;
            masterList = new List<Event>();
            altList = new List<Event>();
            goalCriteria = new List<Event>();
            negList = new List<Event>();
            pushList = new List<Event>();
            spawnList = new List<Event>();
            negspawnList = new List<Event>();
            chanceList = new List<double>();
            nchanceList = new List<double>();
            masterList.Add(new OneRover(null));
            masterList.Add(new OneTurret(null));

        }

        public void findEvents()
        {
            Item goal = worldParent.player.getState();
            foreach (Event events in masterList)
            {
                generateGoalList(events, goal);
                generateNegList(events, goal);
                generateAltList(events);
            }
            
            makeList();
            makeNegList();
            findZeroPaths();
            determineSpawn();
        }
        public void generateNegList(Event events, Item item)
        {
            if (events.eventChildren.Count != 0)
                foreach (Event eventChild in events.eventChildren)
                    generateNegList(eventChild, item);
            if (events.item.id != item.id)
                negList.Add(events);
        }
        public void findZeroPaths()
        {
            bool isGoal;
            List<Event> temp = new List<Event>();
            foreach (Event negEvent in negspawnList)
            {
                isGoal = false;
                foreach (Event goal in spawnList)
                    if (negEvent.id == goal.id)
                    {
                        isGoal = true;
                        break;
                    }
                if (!isGoal)
                    temp.Add(negEvent);
            }
            negspawnList = new List<Event>(temp);

            
        }
        public void generateGoalList(Event events, Item item)
        {
            if(events.eventChildren.Count != 0)
                foreach(Event eventChild in events.eventChildren)
                    generateGoalList(eventChild, item);
            if (events.item.id == item.id)
                goalCriteria.Add(events);
        }
        public void makeList()
        {
            foreach(Event events in goalCriteria)
            {
                chance = events.chance;
                checkSpawned(events);

            }
        }
        public void makeNegList()
        {
            foreach (Event events in negList)
            {
                chance = events.chance;
                checkNSpawned(events);

            }
        }
        public void checkSpawned(Event events)
        {
            if (events.eventParent == null)
            {
                spawnList.Add(events);
                chanceList.Add(chance);
            }
            else if (events.eventParent.spawned == true)
            {
                spawnList.Add(events);
                chanceList.Add(chance);
            }
            else
            {
                chance = chance * events.eventParent.chance;
                checkSpawned(events.eventParent);
            }

        }
        public void checkNSpawned(Event events)
        {
            if (events.eventParent == null)
            {
                negspawnList.Add(events);
                nchanceList.Add(chance);
            }
            else if (events.eventParent.spawned == true)
            {
                negspawnList.Add(events);
                nchanceList.Add(chance);
            }
            else
            {
                chance = chance * events.eventParent.chance;
                checkNSpawned(events.eventParent);
            }

        }
        public void generateAltList(Event events)
        {
            bool inList = false;
            if (events.eventParent == null || events.eventParent.spawned)
            {
                if (altList.Count == 0)
                    altList.Add(events);
                foreach (Event events2 in altList)
                    if ((events2.id == events.id))
                        inList = true;
                if(!inList)
                    altList.Add(events);
                foreach (Event child in events.eventChildren)
                    generateAltList(child);
            }
                    
       }
        public void determineSpawn()
        {
            int sum;
            int randVal;
            double totalChance = 0;
            Random random = new Random();
            foreach (double chance in chanceList)
                totalChance = totalChance + chance;
            totalChance = totalChance * 1000;
            if (worldParent.maxEvents <= spawnList.Count)
                for (int x = 0; x < worldParent.maxEvents; x++)
                {
                    sum = 0;
                    randVal = random.Next(0, (int)totalChance + 1);
                    for (int j = 0; j < chanceList.Count; j++)
                        if (randVal < sum + chanceList[j] * 1000)
                        {
                            pushList.Add(spawnList[j]);
                            break;
                        }
                        else
                        {
                            sum = sum + (int)(chanceList[j] * 1000);
                        }
                }
            else
            {
                for (int x = 0; x < spawnList.Count; x++)
                {
                    sum = 0;
                    randVal = random.Next(0, (int)totalChance + 1);
                    for (int j = 0; j < chanceList.Count; j++)
                        if (randVal < sum + chanceList[j] * 1000)
                        {
                            pushList.Add(spawnList[j]);
                            break;
                        }
                        else
                        {
                            sum = sum + (int)(chanceList[j] * 1000);
                        }
                }
                for (int x = spawnList.Count; x < worldParent.maxEvents; x++)
                {
                    sum = 0;
                    randVal = random.Next(0, altList.Count);
                    pushList.Add(altList[randVal]);
                }
            }
        }
        
    }
}
