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
    class Title
    {
        bool stage1 = false;
        bool stage2 = false;
        bool stage3 = false;
        bool done = false;
        int count = 0;
        public project_driver gameParent;
        SpriteFont displayFont;
        KeyboardState kState, pState;
        public Title(project_driver gameParent)
        {
            this.gameParent = gameParent;
            displayFont = gameParent.Content.Load<SpriteFont>("Art\\Fonts\\consoleFont");
            kState = Keyboard.GetState();
        }
        public void Update()
        {
            pState = kState;
            kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Space) && pState.IsKeyUp(Keys.Space))
                if (stage1)
                    if (stage2)
                        if (stage3)
                            gameParent.isTitle = false;
                        else
                        {
                            if (!done)
                            {
                                done = true;
                                count = 150;
                            }
                            else
                            {
                                done = false;
                                count = 0;
                                stage3 = true;
                            }
                        }
                    else
                    {
                        if (!done)
                        {
                            done = true;
                            count = 150;
                        }
                        else
                        {
                            done = false;
                            count = 0;
                            stage2 = true;
                        }
                    }
                else
                {
                    if (!done)
                    {
                        done = true;
                        count = 150;
                    }
                    else
                    {
                        done = false;
                        count = 0;
                        stage1 = true;
                    }
                }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (count % 200 == 0 && count != 0)
                done = true;
            //if (kState != pState)
            //{
            //    done = true;
            //    count = count + (150 - count % 150);
            //}
            if (stage3)
            {
                spriteBatch.DrawString(displayFont, "On the frozen lunar plains, the Cold War ", new Vector2((1024 - displayFont.MeasureString("On the frozen lunar plains, the Cold War just got colder.").X) / 2, 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 150), 0, 1)));

                if (done)
                {
                    spriteBatch.DrawString(displayFont, " just ", new Vector2((1024 - displayFont.MeasureString("On the frozen lunar plains, the Cold War just got colder.").X) / 2 + displayFont.MeasureString("On the dusty lunar plains, the Cold War ").X/* - MathHelper.Clamp(count - 150, 0, 150)*/, 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 500), 0, 1)));
                    spriteBatch.DrawString(displayFont, " got ", new Vector2((1024 - displayFont.MeasureString("On the frozen lunar plains, the Cold War just got colder.").X) / 2 + +displayFont.MeasureString("On the dusty lunar plains, the Cold War just ").X/* - MathHelper.Clamp(count - 150, 0, 150)*/, 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 900), 0, 1)));
                    spriteBatch.DrawString(displayFont, " colder.", new Vector2((1024 - displayFont.MeasureString("On the frozen lunar plains, the Cold War just got colder.").X) / 2 + displayFont.MeasureString("On the dusty lunar plains, the Cold War just got ").X, 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 1250), 0, 1)));

                }
            }
            else if (stage2)
            {
                spriteBatch.DrawString(displayFont, "Now, both nations maintain an uneasy truce on the surface of the once-neutral Moon, \nseparated only by the intimidating mountain range of the Montes Harbinger...", new Vector2(200, 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 150), 0, 1)));
                if (done)
                    spriteBatch.DrawString(displayFont, "...until a routine survey mission crashes on the wrong side of the lunar Iron Curtain.", new Vector2(375, 350), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 500), 0, 1)));

            }
            else if (stage1)
            {
                spriteBatch.DrawString(displayFont, "In space, no one could hear them seize the means of production.", new Vector2((1024 - displayFont.MeasureString("In space, no one could hear them seize the means of production.").X) / 2, 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 150), 0, 1)));


            }
            else
            {
                spriteBatch.DrawString(displayFont, "The year is 1975.", new Vector2(150 + MathHelper.Clamp(count, 0, 150), 300), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 150), 0, 1)));


                if (done)
                    spriteBatch.DrawString(displayFont, "Not to be outdone by the success of the Apollo program, \nthe USSR fast-tracked their lunar ambitions.", new Vector2(525 - MathHelper.Clamp(count - 150, 0, 150), 350), new Color(255, 255, 255, MathHelper.Clamp(((float)count / 500), 0, 1)));
            }
            count++;
        }
    }
}
