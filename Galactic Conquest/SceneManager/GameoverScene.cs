using Galactic_Conquest.GeneralScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Galactic_Conquest.SceneManager
{
    public class GameoverScene : GameScene
    {
        private GraphicsDevice graphicsDevice;
        private SpriteFont gameOverFont;
        private SpriteFont redirectFont;
        private SpriteBatch spriteBatch;

        private PlayScene _playScene;
        private MainScene _mainScene;

        private KeyboardState os;
        private string gameOverMsg = "Game Over!";
        private string restartMsg = "Press R to Restart Game!";
        private string mainMenuMsg = "Press Esc to Main Menu!";
        private List<Texture2D> starTextures;
        private Star Star;

        private Song GameOverSong;
        public GameoverScene(Game game,GraphicsDevice graphicsDevice,SpriteFont gameoverFont,SpriteFont otherFont,SpriteBatch spriteBatch,MainScene mainScene,PlayScene playScene): base(game) 

        {
            this.graphicsDevice = graphicsDevice;
            this.gameOverFont = gameoverFont;
            this.redirectFont = otherFont;
            this.spriteBatch = spriteBatch;
            this._mainScene = mainScene;
            this._playScene = playScene;

            starTextures = InitializeStar(game);
            Star = new Star(starTextures, 0.1f, true);
            Star.Position = new Vector2(320,332);
            GameOverSong = game.Content.Load<Song>("Music/GameOver");
        }
        
        public bool IsRestartRequested { get; private set; }
        public bool IsMainMenuRequested { get; private set; }

        public override void Update(GameTime gameTime)
        {
            
                KeyboardState ks = Keyboard.GetState();
                if(ks.IsKeyDown(Keys.M) && os.IsKeyUp(Keys.M))
                {
                    MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                }
                if(ks.IsKeyDown(Keys.R) && this.Enabled)
                {
                    
                    IsRestartRequested = true;
                    
                }
                if(ks.IsKeyDown(Keys.Escape) && this.Enabled)
                {
                    
                    IsMainMenuRequested = true;
                }
            os = ks;
            RedirectToScene(IsRestartRequested, IsMainMenuRequested);

            IsMainMenuRequested = false;
            IsRestartRequested = false;
            base.Update(gameTime);
            Star.Update(gameTime);

            

        }
        public void RedirectToScene(bool IsRestart,bool IsMainMenu)
        {
            if(IsRestart)
            {
                _playScene.SavePlayerData();
                _playScene.LoadPlayerData();
                this.Hide();
                _playScene.ResetGame();
                _playScene.Show();
            }
            if(IsMainMenu)
            {
                _playScene.SavePlayerData();
                _playScene.LoadPlayerData();
                _playScene.Hide();
                _playScene.ResetGame();
                this.Hide();
                _mainScene.Show();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(new Texture2D(graphicsDevice,1,1), new Rectangle(0,0,graphicsDevice.Viewport.Width,graphicsDevice.Viewport.Height),Color.Black);

            Vector2 gameoverPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - gameOverFont.MeasureString(gameOverMsg).X / 2, 20);
            Vector2 restartPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - redirectFont.MeasureString(restartMsg).X / 2, 170);
            Vector2 mainmenuPosition = new Vector2(graphicsDevice.Viewport.Width / 2 - redirectFont.MeasureString(mainMenuMsg).X / 2, 210);

            spriteBatch.DrawString(gameOverFont, gameOverMsg, gameoverPosition,Color.IndianRed);
            spriteBatch.DrawString(redirectFont, restartMsg, restartPosition, Color.YellowGreen);
            spriteBatch.DrawString(redirectFont, mainMenuMsg, mainmenuPosition, Color.YellowGreen);
            Star.Draw(spriteBatch);
            spriteBatch.DrawString(redirectFont, $"Score:{_playScene.elapsedTime.ToString(@"hh\:mm\:ss\.ff")}", new Vector2(270,270),Color.White);
            spriteBatch.DrawString(redirectFont,$"x{_playScene.PlayerStars.ToString()}",new Vector2(380,340),Color.Cyan);
            spriteBatch.End();
            base.Draw(gameTime);
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
        public override void Show()
        {
            
                MediaPlayer.Play(GameOverSong);
                base.Show();
            
            
        }
    }
}
