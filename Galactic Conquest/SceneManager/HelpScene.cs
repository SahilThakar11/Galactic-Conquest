using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Galactic_Conquest.SceneManager
{
    public class HelpScene : GameScene
    {
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private SpriteFont SGFont;
        private SpriteFont myFont;
        private KeyboardState oldState;
        private int selectedIndex;
        private int selectedIndexItems;
        private List<string> helpItems = new List<string>
        {
            "Controls",
            "Game-Details"
        };
        private List<string> controlItems = new List<string>()
        {
            "Keyboard",
            "Mouse"
        };
        private List<string> gameDetailsItems = new List<string>()
        {
            "Game Modes","Player","Enemies","Projectiles"
        };
        private List<Texture2D> shipImages;
        private List<Texture2D> enemyImages;
        private List<Texture2D> projectileImages;
        public HelpScene(Game game) : base(game) 
        {
            Game1 game1 = game as Game1;
            this._spriteBatch = game1._spriteBatch;
            SGFont = game.Content.Load<SpriteFont>("Fonts/HealthFont");
            myFont = game.Content.Load<SpriteFont>("Fonts/myFont");
            shipImages = new List<Texture2D>()
            {
                game.Content.Load<Texture2D>("Assests/Player/blue_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/green_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/yellow_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/cyan_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/red_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/grey_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/emerald_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/purple_ship_3"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Idle")
            };
            projectileImages = new List<Texture2D>()
            {
                game.Content.Load<Texture2D>("Assests/Projectiles/green_1"),
                game.Content.Load<Texture2D>("Assests/Projectiles/red_2"),
                game.Content.Load<Texture2D>("Assests/Projectiles/purple_3"),
                game.Content.Load<Texture2D>("Assests/Projectiles/blue_4"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball5"),
                game.Content.Load<Texture2D>("Assests/Projectiles/green_6")
            };
            enemyImages = new List<Texture2D>()
            {
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_flash"),
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_track"),
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_rage"),
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_cyclic")
            };
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            if(currentState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                selectedIndex++;
                selectedIndexItems = 0;
                if (selectedIndex == helpItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (currentState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                selectedIndex--;
                selectedIndexItems = 0;
                if (selectedIndex == -1)
                {
                    selectedIndex = helpItems.Count - 1;
                }
            }
            if(currentState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndexItems++;
                if((selectedIndexItems == controlItems.Count && selectedIndex == 0 )|| (selectedIndexItems == gameDetailsItems.Count && selectedIndex == 1))
                {
                    selectedIndexItems = 0;
                }
            }
            if (currentState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndexItems--;
                if (selectedIndexItems < 0)
                { 
                   selectedIndexItems = (selectedIndex == 0)? controlItems.Count - 1 : gameDetailsItems.Count - 1;
                }
            }
            oldState = currentState;
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            for (int i = 0; i < helpItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    _spriteBatch.DrawString(SGFont, helpItems[i], new Vector2(240, 0), Color.DarkCyan);
                    if(selectedIndex == 0)
                    {
                        DrawMenu(controlItems);
                    }
                    else if(selectedIndex == 1)
                    {
                        DrawMenu(gameDetailsItems);
                    }
                }
                else
                {
                    _spriteBatch.DrawString(SGFont, helpItems[i], new Vector2(470, 0), Color.White);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawMenu(List<string> Items)
        {
            int x = 70;
            for(int i = 0;i < Items.Count;i++)
            {
                if(selectedIndexItems == i)
                {
                    _spriteBatch.DrawString(SGFont, Items[i], new Vector2(10,x),Color.IndianRed);
                    x += 50;
                    
                    if(selectedIndex == 0)
                    {
                        
                        switch (selectedIndexItems)
                        {
                            case 0:
                                DrawKeyBoardControls();
                                break;
                            case 1:
                                DrawMouseControls();
                                break;

                        }
                    }
                    if(selectedIndex == 1)
                    {
                        switch(selectedIndexItems)
                        {
                            case 0:
                                DrawGameModes();
                                break;
                            case 1:
                                DrawPlayers();
                                break;
                            case 2:
                                DrawEnemies();
                                break;
                            case 3:
                                DrawProjectiles();
                                break;

                        }
                    }
                }
                else
                {
                    _spriteBatch.DrawString(SGFont, Items[i], new Vector2(10,x), Color.White);
                    x += 50;
                }
            }
        }



        private void DrawProjectiles()
        {
            _spriteBatch.Draw(projectileImages[0], new Vector2(300, 50),null, Color.White,0f,Vector2.Zero,2f,SpriteEffects.None,0f);
            _spriteBatch.DrawString(myFont, "level-1", new Vector2(300, 130), Color.White);
            _spriteBatch.Draw(projectileImages[1], new Vector2(450, 50),null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(myFont, "level-2", new Vector2(450, 130), Color.White);
            _spriteBatch.Draw(projectileImages[2], new Vector2(600, 50), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(myFont, "level-3", new Vector2(600, 130), Color.White);
            _spriteBatch.Draw(projectileImages[3], new Vector2(300, 250), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(myFont, "level-4", new Vector2(300, 330), Color.White);
            _spriteBatch.Draw(projectileImages[4], new Vector2(450, 250), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(myFont, "level-5", new Vector2(450, 330), Color.White);
            _spriteBatch.Draw(projectileImages[5], new Vector2(600, 250), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(myFont, "level-6", new Vector2(600, 330), Color.White);

        }
        private void DrawEnemies()
        {
            _spriteBatch.Draw(enemyImages[0], new Vector2(170, 50),Color.White);
            _spriteBatch.DrawString(myFont, "Flash", new Vector2(230, 180), Color.MediumVioletRed);
            _spriteBatch.DrawString(myFont, "Fast Movement \nTeleportation \nDamage-3", new Vector2(200, 230), Color.MediumVioletRed);
            _spriteBatch.Draw(enemyImages[1], new Vector2(310, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Crimson", new Vector2(365, 180), Color.DarkRed);
            _spriteBatch.DrawString(myFont, "Deadly Laser Beam \nRegeneration \nDamage-3", new Vector2(350, 230), Color.DarkRed);
            _spriteBatch.Draw(enemyImages[2], new Vector2(470, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Frostbite", new Vector2(520, 180), Color.OrangeRed);
            _spriteBatch.DrawString(myFont, "Slowest Movement \nMissile Shooting \nDamage-5", new Vector2(500, 230), Color.OrangeRed);
            _spriteBatch.Draw(enemyImages[3], new Vector2(620, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Vortex", new Vector2(675, 180), Color.Gray);
            _spriteBatch.DrawString(myFont, "Multiple Projectile \nSpin Shooting \nDamage-4", new Vector2(650, 230), Color.Gray);
        }

        private void DrawPlayers()
        {
            _spriteBatch.Draw(shipImages[8], new Vector2(200, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Celestial Breeze", new Vector2(180, 130), Color.White);
            _spriteBatch.Draw(shipImages[7], new Vector2(350, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Nebula Dreamer", new Vector2(340, 130), Color.White);
            _spriteBatch.Draw(shipImages[6], new Vector2(500, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Galactic Explorer", new Vector2(490, 130), Color.White);
            _spriteBatch.Draw(shipImages[5], new Vector2(650, 50), Color.White);
            _spriteBatch.DrawString(myFont, "Starlight Voyager", new Vector2(640, 130), Color.White);
            _spriteBatch.Draw(shipImages[0], new Vector2(650, 170), Color.White);
            _spriteBatch.DrawString(myFont, "Titan's Fury", new Vector2(180, 240), Color.White);
            _spriteBatch.Draw(shipImages[4], new Vector2(200, 170), Color.White);
            _spriteBatch.DrawString(myFont, "Solar Leviathan", new Vector2(340, 240), Color.White);
            _spriteBatch.Draw(shipImages[2], new Vector2(350, 170), Color.White);
            _spriteBatch.DrawString(myFont, "Infinite Behemoth", new Vector2(490, 240), Color.White);
            _spriteBatch.Draw(shipImages[1], new Vector2(500, 170), Color.White);
            _spriteBatch.DrawString(myFont, "Radiant Serenity", new Vector2(640, 240), Color.White);
            _spriteBatch.Draw(shipImages[3], new Vector2(200, 290), Color.White);
            _spriteBatch.DrawString(myFont, "Luminous Whisper", new Vector2(180, 355), Color.White);

        }
        private void DrawGameModes()
        {
            _spriteBatch.DrawString(SGFont, "Endless Survival", new Vector2(200, 50), Color.Purple);
            DrawStringText("Enemies will spawn your task \n is to  kill enemies or dodge \nprojectiles to collect stars. \nLonger time you survive a lot \nstars you wil get.", new Vector2(200, 100));
            _spriteBatch.DrawString(SGFont, "Boss Fight", new Vector2(550, 50), Color.Purple);
            DrawStringText("You have to fight against \ndifferent Bosses. If you\nWin the Game you will get lot \nof stars.But You will just have \n1 life with 6 health points.", new Vector2(500, 100));
        }

        private void DrawMouseControls()
        {
            _spriteBatch.DrawString(myFont, "Left/Right Click : Move Up/Down", new Vector2(270, 100), Color.Yellow);
        }

        private void DrawKeyBoardControls()
        {
            _spriteBatch.DrawString(SGFont, "Movement Controls", new Vector2(170, 50), Color.OrangeRed);
            DrawStringText("W/Up : To Move Upwards",new Vector2(170,90));
            DrawStringText("A/Left : To Move Left/Behind", new Vector2(170,120));
            DrawStringText("S/Down : To Move Down", new Vector2(170,150));
            DrawStringText("D/Right : To Move Right/Foreward", new Vector2(170, 180));

            _spriteBatch.DrawString(SGFont, "Shooting Controls", new Vector2(170, 210), Color.OrangeRed);
            DrawStringText("E : To Shoot Projectile", new Vector2(170, 250));

            _spriteBatch.DrawString(SGFont, "Basic Controls", new Vector2(170, 280), Color.OrangeRed);
            DrawStringText("M : Music toggle", new Vector2(170, 320));
           
            DrawStringText("Esc : Return to Main Menu", new Vector2(170, 350));
            

        }
        private void DrawStringText(string text,Vector2 position)
        {
            _spriteBatch.DrawString(myFont,text,position,Color.Yellow);
        }
    }
}
