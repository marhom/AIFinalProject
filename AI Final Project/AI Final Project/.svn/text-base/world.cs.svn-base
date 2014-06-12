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

    class world
    {

        public controlHandler controlhandler;
        public Viewport viewport;
        public project_driver gameParent;
        public UI ui;
        public Texture2D mouseTexture, buttonTexture, targetTexture, projectileTexture2, projectileTexture3, projectileTexture4, projectileTexture1;
        public static Texture2D blank, wTexture, aTexture, xTexture, hTexture;
        public SpriteFont consoleFont;
        public static SpriteFont itemFont;
        public List<Texture2D> textureList;
        public GameTime simTime;
        public map worldMap;
        public Player player;
        public Enemy enemy;
        public bool gameOver = false;
        public bool victory = false;
        public bool canWin = false;
        public static int eventStep = 0;
        public int timer;
        public List<Projectile> projectiles, projectilesR;
        public List<Enemy> enemyList, enemyListR, activeEnemies;
        public List<Event> eventList, eventListR;

        /*DEFINES*/
        public int maxEvents = 5;

        public world(project_driver gameParent)
        {
            simTime = new GameTime();

            ui = new UI(this);
            viewport = new Viewport(new Vector2(512, 384), this);
            this.gameParent = gameParent;

        }
        public void initialize()
        {

            this.textureList = new List<Texture2D>();
            //textureList.Add(gameParent.Content.Load<Texture2D>("Art\\Textures\\Surface\\lunarCrater1"));
            //textureList.Add(gameParent.Content.Load<Texture2D>("Art\\Textures\\Surface\\lunarCrater2"));
            eventStep = 0;
            for (int x = 1; x < 10; x++)
                textureList.Add(gameParent.Content.Load<Texture2D>("Art\\Textures\\Surface\\Realtile" + x));
            this.mouseTexture = gameParent.Content.Load<Texture2D>("Art\\Textures\\UI\\crosshair");
            this.consoleFont = gameParent.Content.Load<SpriteFont>("Art\\Fonts\\consoleFont");
            this.buttonTexture = gameParent.Content.Load<Texture2D>("Art\\Textures\\UI\\button");
            projectileTexture1 = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Projectile1");
            projectileTexture2 = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Projectile2");
            projectileTexture3 = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Projectile3");
            projectileTexture4 = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Projectile4");
            hTexture = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Pickups\\crate+");
            aTexture = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Pickups\\crateA");
            wTexture = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Pickups\\cratew");
            xTexture = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\Pickups\\cratex");
            blank = gameParent.Content.Load<Texture2D>("Art\\Textures\\UI\\blank");
            //ui.initialize(cPaneTexture, vPaneTexture, buttonTexture, this);
            Agent.sprite = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\stationaryAgent");
            //Player.sprite = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\rover-1");
            Enemy.sprite = gameParent.Content.Load<Texture2D>("Art\\Textures\\Agents\\stationaryAgent");
            itemFont = consoleFont;
            player = new Player(this);
            enemyList = new List<Enemy>();
            enemyListR = new List<Enemy>();
            activeEnemies = new List<Enemy>();
            worldMap = new map(this);
            eventList = new List<Event>();
            eventListR = new List<Event>();
            projectiles = new List<Projectile>();
            projectilesR = new List<Projectile>();
            worldMap.initialize(2000, 2000, textureList);
            controlhandler = new controlHandler(mouseTexture, targetTexture);
            viewport.initialize();
            worldMap.generateNodeArray();
            timer = 300;

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            worldMap.Draw(spriteBatch, consoleFont, controlhandler);

            foreach (Enemy enemies in activeEnemies)
                enemies.Draw(spriteBatch);
            ui.Draw(spriteBatch);
            foreach (Projectile projectile in projectiles)
                projectile.Draw(spriteBatch);
            foreach (Event events in eventList)
                events.Draw(spriteBatch);
            player.Draw(spriteBatch);
            controlhandler.Draw(spriteBatch);

            if (gameOver)
            {
                spriteBatch.Draw(blank, new Rectangle(0, 0, 1024, 768), new Color(0, 0, 0, (float)(300 - timer) / 300));
                spriteBatch.DrawString(consoleFont, "GAME OVER", new Vector2((1024 - consoleFont.MeasureString("GAME OVER").X) / 2, (768 - consoleFont.MeasureString("GAME OVER").Y) / 2), new Color(255, 255, 255, (float)(300 - timer) / 300));
                timer = (int)MathHelper.Clamp(timer - 1, 0, 300);
            }
            if (victory)
            {
                spriteBatch.Draw(blank, new Rectangle(0, 0, 1024, 768), new Color(255, 255, 255, (float)(300 - timer) / 300));
                spriteBatch.DrawString(consoleFont, "VICTORY", new Vector2((1024 - consoleFont.MeasureString("VICTORY").X) / 2, (768 - consoleFont.MeasureString("VICTORY").Y) / 2), new Color(0, 0, 0, (float)(300 - timer) / 300));
                timer = (int)MathHelper.Clamp(timer - 1, 0, 300);
            }
        }
        public void Update()
        {
            if (!gameOver && !victory)
            {
                UpdateEvents();
                UpdateProjectiles();
                UpdateEnemies();
                player.Control();
            }
            if (canWin && player.collisionRectangle.Intersects(new Rectangle((int)worldMap.goalTile.location.X, (int)worldMap.goalTile.location.Y, 64, 64)))
                victory = true;

            controlhandler.Update();
            viewport.Update();
            if (gameOver && controlhandler.keyState.IsKeyDown(Keys.Escape))
                gameParent.Exit();
            if (canWin)
                worldMap.goalTile.color = Color.Red;
            //worldMap.Update();

        }
        public void UpdateProjectiles()
        {
            foreach (Projectile projectile in projectiles)
                projectile.Update();
            foreach (Projectile projectile in projectilesR)
                projectiles.Remove(projectile);
            projectilesR.Clear();
        }
        public void UpdateEvents()
        {
            if (-(viewport.originVector - worldMap.worldMap[0, 0].location).Y > worldMap.eventLine * eventStep)
            {
                if (eventStep < 5)
                    worldMap.generateEvents();

            }
            foreach (Event events in eventList)
                events.Update();
            foreach (Event events in eventListR)
                eventList.Remove(events);
            eventListR.Clear();
        }
        public void UpdateEnemies()
        {
            activeEnemies.Clear();
            foreach (Enemy enemies in enemyList)
                if (!(enemies.position.X > 1024 || enemies.position.X < -enemies.collisionRectangle.Width || enemies.position.Y > 768 || enemies.position.Y < -enemies.collisionRectangle.Height))
                    activeEnemies.Add(enemies);

            foreach (Enemy enemies in activeEnemies)
                enemies.Update();
            foreach (Enemy enemies in enemyListR)
                enemyList.Remove(enemies);
            enemyListR.Clear();
        }
    }
}
