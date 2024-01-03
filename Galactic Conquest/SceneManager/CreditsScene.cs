using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.SceneManager
{
    public class CreditsScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private SpriteFont myFont;
        private Texture2D logoTexture;
        private List<Texture2D> shipImages;
        private List<Texture2D> enemyImages;
        public CreditsScene(Game game): base(game) 
        {
            Game1 game1 = game as Game1;
            this.spriteBatch = game1._spriteBatch;
            this.myFont = game1.Content.Load<SpriteFont>("Fonts/creditFont");
            logoTexture = game.Content.Load<Texture2D>("UI/Logo");
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
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(logoTexture, new Vector2(130, -30), Color.MediumPurple);
            DrawText("Created By : Sahil Thakar", new Vector2(290,230),Color.GreenYellow);
            DrawText("Powered By : Monogame",new Vector2(297,270), Color.Orange);
            
            DrawText("Thank you for playing and supporting indie development", new Vector2(150, 350),Color.DarkGreen);
            DrawText("Galactic Conquest (c) 2023 Sahil Thakar. All rights Reserved.", new Vector2(130, 400), Color.White);
            spriteBatch.End();
        }

        private void DrawText(string text,Vector2 position,Color color)
        {
            Vector2 pPosition = new Vector2(0, -10);
            Vector2 ePosition = new Vector2(650, -10);
            spriteBatch.DrawString(myFont, text, position, color);
            foreach(Texture2D ship in shipImages)
            {
                spriteBatch.Draw(ship, pPosition,Color.White);
                pPosition.Y += 50;
            }
            foreach (Texture2D ship in enemyImages)
            {
                spriteBatch.Draw(ship, ePosition, Color.White);
                ePosition.Y += 120;
            }
        }
    }
}
