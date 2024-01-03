using Galactic_Conquest.GeneralScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.SceneManager
{
    public class Defeat : GameScene
    {
        private GraphicsDevice graphicsDevice;
        private SpriteFont gameOverFont;
        private SpriteFont redirectFont;
        private SpriteBatch spriteBatch;

        private MainScene _mainScene;
        private BossFightScene fightScene;
        private KeyboardState os;
        private string gameOverMsg = "Defeated!";
        private string mainMenuMsg = "Press Esc to Main Menu!";
        private string quitMsg = "Press Q to Exit!";
        private List<Texture2D> starTextures;
        private Star Star;
        private Song GameOverSong;

        public Defeat(Game game, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, MainScene mainScene,BossFightScene bossFightScene,PlayScene playScene) : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            this.gameOverFont = game.Content.Load<SpriteFont>("Fonts/GameoverFont");
            this.redirectFont = game.Content.Load<SpriteFont>("Fonts/RedirectFont");
            this.spriteBatch = spriteBatch;
            this._mainScene = mainScene;
            starTextures = InitializeStar(game);
            Star = new Star(starTextures, 0.1f, true);
            Star.Position = new Vector2(320, 332);
            this.fightScene = bossFightScene;
            GameOverSong = game.Content.Load<Song>("Music/GameOver");
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
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.M) && os.IsKeyUp(Keys.M))
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }
            if (ks.IsKeyDown(Keys.Escape) && this.Enabled)
            {
                fightScene.Hide();
                this.Hide();
                _mainScene.Show();
            }
            if(ks.IsKeyDown(Keys.Q))
            {
                Game.Exit();
            }
            os = ks;
            base.Update(gameTime);
        }
        public override void Show()
        {
            base.Show();
            MediaPlayer.Stop();
            MediaPlayer.Play(GameOverSong);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
 
            Vector2 gameoverPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - gameOverFont.MeasureString(gameOverMsg).X / 2, 20);
            Vector2 mainmenuPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - redirectFont.MeasureString(mainMenuMsg).X / 2, 210);
            Vector2 quitposition = new Vector2(graphicsDevice.Viewport.Width / 2 - redirectFont.MeasureString(quitMsg).X / 2, 260);
            spriteBatch.DrawString(gameOverFont, gameOverMsg, gameoverPosition, Color.IndianRed);
            Star.Draw(spriteBatch);
            spriteBatch.DrawString(redirectFont, "x 100", new Vector2(380, 340), Color.Cyan);
            spriteBatch.DrawString(redirectFont, mainMenuMsg, mainmenuPosition, Color.YellowGreen);
            spriteBatch.DrawString(redirectFont,quitMsg, quitposition, Color.YellowGreen);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
