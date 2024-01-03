using Galactic_Conquest.GeneralScripts;
using Galactic_Conquest.OtherScripts;
using Galactic_Conquest.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;

namespace Galactic_Conquest.SceneManager
{
    public class ShopScene : GameScene
    {
        private Star Star;
        private SpriteFont TextFont;
        private PlayScene _playScene;
        private Player _player;
        private List<string> shopItems = new List<string>()
        {
            "Skins","Projectiles"
        };
        public ShopScene(Game game, PlayScene playScene,Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) : base(game)
        {

            Star = new Star(InitializeStar(game), 0.2f, true);

            TextFont = game.Content.Load<SpriteFont>("Fonts/HealthFont");
            _playScene = playScene;
            SGFont = game.Content.Load<SpriteFont>("Fonts/HealthFont");
            myFont = game.Content.Load<SpriteFont>("Fonts/myFont");
            this.spriteBatch = spriteBatch;
            this._player = playScene._player;
            _player.LoadOwnedSkinPlayer(_playScene.ownedSkinNumbers);
            _playScene.ownedSkinNumbers.Add(0);
            _playScene.SavePlayerData();
            _playScene.LoadPlayerData();

        }
        private List<Texture2D> InitializeStar(Game game)
        {
            List<Texture2D> starFrames = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("UI/Star1"),
                game.Content.Load<Texture2D>("UI/Star2"),
                game.Content.Load<Texture2D>("UI/Star3"),
                game.Content.Load<Texture2D>("UI/Star4"),
                game.Content.Load<Texture2D>("UI/Star5"),
                game.Content.Load<Texture2D>("UI/Star6"),
                game.Content.Load<Texture2D>("UI/Star7"),

            };
            return starFrames;
        }
        private KeyboardState oldState;
        private int selectedIndex;
        private int selectedIndexItems;
        private SpriteFont SGFont;
        private SpriteFont myFont;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private List<string> skins = new List<string>()
        {
            "Owned","Buy"
        };
        private List<string> projectiles = new List<string>()
        {
            "Owned","Buy"
        };
        private int SelectedSkinIndex = 0;
        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndexItems++;
                if ((selectedIndexItems == skins.Count && selectedIndex == 0) || (selectedIndexItems == projectiles.Count && selectedIndex == 1))
                {
                    selectedIndexItems = 0;
                }
            }
            if (currentState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndexItems--;
                if (selectedIndexItems < 0)
                {
                    selectedIndexItems = (selectedIndex == 0) ? skins.Count - 1 : projectiles.Count - 1;
                }
            }
            if (selectedIndexItems == 1)
            {
                if (currentState.IsKeyDown(Keys.D1))
                {
                    BuySkinByNumber(1);
                }
                if (currentState.IsKeyDown(Keys.D2))
                {
                    BuySkinByNumber(2);
                }
                if (currentState.IsKeyDown(Keys.D3))
                {
                    BuySkinByNumber(3);

                }
                if (currentState.IsKeyDown(Keys.D4))
                {
                    BuySkinByNumber(4);
                }
                if (currentState.IsKeyDown(Keys.D5))
                {
                    BuySkinByNumber(5);
                }
                if (currentState.IsKeyDown(Keys.D6))
                {
                    BuySkinByNumber(6);
                }
                if (currentState.IsKeyDown(Keys.D7))
                {
                    BuySkinByNumber(7);
                }
                if (currentState.IsKeyDown(Keys.D8))
                {
                    BuySkinByNumber(8);
                }
            }
            if (selectedIndex == 0)
            {
                if (currentState.IsKeyDown(Keys.D1) && oldState.IsKeyUp(Keys.D1))
                {
                    SelectedSkinIndex = 1;
                }
                else if (currentState.IsKeyDown(Keys.D2) && oldState.IsKeyUp(Keys.D2))
                {
                    SelectedSkinIndex = 2;
                }
                else if (currentState.IsKeyDown(Keys.D3) && oldState.IsKeyUp(Keys.D3))
                {
                    SelectedSkinIndex = 3;
                }
                else if (currentState.IsKeyDown(Keys.D4) && oldState.IsKeyUp(Keys.D4))
                {
                    SelectedSkinIndex = 4;
                }
                else if (currentState.IsKeyDown(Keys.D5) && oldState.IsKeyUp(Keys.D5))
                {
                    SelectedSkinIndex = 5;
                }
                else if (currentState.IsKeyDown(Keys.D6) && oldState.IsKeyUp(Keys.D6))
                {
                    SelectedSkinIndex = 6;
                }
                else if (currentState.IsKeyDown(Keys.D7) && oldState.IsKeyUp(Keys.D7))
                {
                    SelectedSkinIndex = 7;
                }
                else if (currentState.IsKeyDown(Keys.D8) && oldState.IsKeyUp(Keys.D8))
                {
                    SelectedSkinIndex = 8;
                }
                else if (currentState.IsKeyDown(Keys.D0) && oldState.IsKeyUp(Keys.D0))
                {
                    SelectedSkinIndex = 0;
                }

            }
            _playScene.Update(gameTime);
            _playScene.Hide();
            _playScene.ResetGame();
            oldState = currentState;
            base.Update(gameTime);
            Star.Update(gameTime);
        }
        Color purchaseColor;
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Star.Draw(spriteBatch);
            Star.Position = new Vector2(600, 0);
            spriteBatch.DrawString(TextFont, _playScene.TotalStars.ToString(), new Vector2(680, 15), Color.White);
            spriteBatch.DrawString(TextFont, purchaseMsg, new Vector2(150, 15), purchaseColor);
        }

        private string purchaseMsg = string.Empty;
        public void BuySkinByNumber(int number)
        {
            for (int i = _player.BuySkins.Count - 1; i >= 0; i--)
            {
                var skin = _player.BuySkins[i];
                if (skin.IsOwned == false && skin.SkinNumber == number)
                {
                    if (skin.Cost <= _playScene.TotalStars)
                    {
                        skin.IsOwned = true;
                        _playScene.PlayerStars -= skin.Cost;
                        _playScene.ownedSkinNumbers.Add(skin.SkinNumber);
                        _playScene.SavePlayerData();
                        _playScene.LoadPlayerData();

                        purchaseMsg = $"Bravo !!:) {skin.Name} Bought";
                        purchaseColor = Color.Green;
                    }
                    else
                    {
                        skin.IsOwned = false;
                        purchaseMsg = "Sorry Insuficiant Stars!! :(";
                        purchaseColor = Color.Red;
                    }

                }
            }
        }
        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            for (int i = 0; i < shopItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(SGFont, shopItems[i], new Vector2(10, 15), Color.MediumVioletRed);
                    if (selectedIndex == 0)
                    {
                        DrawShop(skins);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawShop(List<string> items)
        {
            int x = 70;
            for (int i = 0; i < items.Count; i++)
            {
                if (selectedIndexItems == i)
                {
                    spriteBatch.DrawString(SGFont, items[i], new Vector2(10, x), Color.Green);
                    x += 50;
                    if (selectedIndex == 0)
                    {
                        switch (selectedIndexItems)
                        {
                            case 0:
                                DrawOwnedSkins();
                                break;
                            case 1:
                                DrawBuySkins();
                                break;
                        }
                    }

                }
                else
                {
                    spriteBatch.DrawString(SGFont, items[i], new Vector2(10, x), Color.White);
                    x += 50;
                }
            }
        }

        private void DrawBuySkins()
        {
            int x = 170;
            int y = 50;
            int n = 170;
            int i = 1;
            foreach (var BuySkin in _player.BuySkins)
            {
                spriteBatch.Draw(BuySkin.ShipTextures[4], new Vector2(x, y), Color.White);
                spriteBatch.DrawString(myFont, BuySkin.Name.ToString(), new Vector2(x, n - 40), Color.GreenYellow);
                Star.Draw(spriteBatch);
                Star.Position = new Vector2(x, y + 100);
                spriteBatch.DrawString(myFont, BuySkin.Cost.ToString(), new Vector2(x + 60, n), Color.Cyan);
                spriteBatch.DrawString(myFont, $"{BuySkin.SkinNumber} to Buy", new Vector2(x + 10, n + 50), Color.White);
                x += 150;
                i++;
                if (x > 700)
                {
                    y += 200;
                    n += 200;
                    x = 170;
                }
            }
        }

        private Color colorSelect = Color.White;


        private void DrawOwnedSkins()
        {
            int x = 200;
            int y = 50;
            int lastSelectedIndex = Math.Min(SelectedSkinIndex,_player.ownedSkins.Count -1);
            for (int i = 0; i < _player.ownedSkins.Count; i++)
            {
                var OwnedSkins = _player.ownedSkins[i];
                spriteBatch.Draw(OwnedSkins.ShipTextures[4], new Vector2(x, y), Color.White);

                if (i == lastSelectedIndex)
                {
                    spriteBatch.DrawString(myFont, "Selected", new Vector2(x + 10, y + 70), Color.Green);
                    OwnedSkins.ApplyToPlayer(_player);
                }
                else
                {
                    spriteBatch.DrawString(myFont, $"{i} to select", new Vector2(x + 10, y + 70), Color.White);
                }
                x += 150;
                if (x > 700)
                {
                    y += 150;
                    x = 200;
                }
            }

        }
    }
}
