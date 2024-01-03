using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galactic_Conquest.GeneralScripts;

namespace Galactic_Conquest.SceneManager
{
    public class Victory : GameScene
    {
        private GraphicsDevice graphicsDevice;
        private SpriteFont gameOverFont;
        private SpriteFont redirectFont;
        private SpriteBatch spriteBatch;

        private MainScene _mainScene;
        private BossFightScene fightScene;
        private KeyboardState os;
        private string gameOverMsg = "Victory!";
        private string mainMenuMsg = "Press Esc to Main Menu!";
        private string quitMsg = "Press Q to Exit!";
        private List<Texture2D> starTextures;
        private Star Star;

        private Song VictorySong;

        public Victory(Game game, GraphicsDevice graphicsDevice,SpriteBatch spriteBatch, MainScene mainScene, BossFightScene bossFightScene,PlayScene playScene) : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            this.gameOverFont = game.Content.Load<SpriteFont>("Fonts/GameoverFont"); 
            this.redirectFont = game.Content.Load<SpriteFont>("Fonts/RedirectFont");
            this.spriteBatch = spriteBatch;
            this._mainScene = mainScene;
            this.fightScene = bossFightScene;
            VictorySong = game.Content.Load<Song>("Music/Victory");
            starTextures = InitializeStar(game);
            Star = new Star(starTextures, 0.1f, true);
            Star.Position = new Vector2(320, 332);
            playScene.PlayerStars += 100;
            playScene.SavePlayerData();
            playScene.LoadPlayerData();
        }
        public override void Show()
        {
            base.Show();
            MediaPlayer.Stop();
            MediaPlayer.Play(VictorySong);
            MediaPlayer.IsRepeating = true;
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
            if((ks.IsKeyDown(Keys.Q)))
            {
               Game.Exit();
            }
            os = ks;
            base.Update(gameTime);
            Star.Update(gameTime);
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
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(new Texture2D(graphicsDevice, 1, 1), new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.Black);

            Vector2 gameoverPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - gameOverFont.MeasureString(gameOverMsg).X / 2, 20);
            Vector2 mainmenuPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - redirectFont.MeasureString(mainMenuMsg).X / 2, 210);
            Vector2 quitposition = new Vector2(graphicsDevice.Viewport.Width / 2 - redirectFont.MeasureString(quitMsg).X / 2, 260);

            spriteBatch.DrawString(gameOverFont, gameOverMsg, gameoverPosition, Color.Green);
            spriteBatch.DrawString(redirectFont, mainMenuMsg, mainmenuPosition, Color.YellowGreen);
            spriteBatch.DrawString(redirectFont, quitMsg, quitposition, Color.YellowGreen);
            Star.Draw(spriteBatch);
            spriteBatch.DrawString(redirectFont, "x 100", new Vector2(380, 340), Color.Cyan);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
