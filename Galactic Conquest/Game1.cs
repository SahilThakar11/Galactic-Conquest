using Galactic_Conquest.GeneralScripts;
using Galactic_Conquest.SceneManager;
using Galactic_Conquest.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Galactic_Conquest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private MainScene _mainScene;
        private BossSelectionScene _bossSelectionScene;
        private PlayScene _playScene;
        private HelpScene _controlsScene;
        private CreditsScene _creditsScene;
        private StatsScene _statScene;
        private ShopScene _shopScene;

        private BossFightScene _bossFightScene;
        private CollisionManager _collisionManager;
        private Background background;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            _playScene.LoadPlayerData();
  
        }
        private void HideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if(item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.Hide();
                }
            }
        }
        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            
            _mainScene = new MainScene(this);
            this.Components.Add(_mainScene);
            _mainScene.Show();
            
            _playScene = new PlayScene(this,_spriteBatch,_mainScene);
            this.Components.Add(_playScene);
            _bossSelectionScene = new BossSelectionScene(this,_spriteBatch, _mainScene,_playScene);
            this.Components.Add(_bossSelectionScene);
            _creditsScene = new CreditsScene(this);
            this.Components.Add(_creditsScene);
            _controlsScene = new HelpScene(this);
            this.Components.Add(_controlsScene);
            _statScene = new StatsScene(this, _playScene);
            this.Components.Add(_statScene);
            _shopScene = new ShopScene(this,_playScene,_spriteBatch);
            this.Components.Add(_shopScene);
            

        }
        
        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState currentState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();
            if(_mainScene.Enabled)
            {
                selectedIndex = _mainScene.Menu.selectedIndex;
                if (selectedIndex == 0 && currentState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    _playScene.Show();
                }
                else if (selectedIndex == 1 && currentState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    _bossSelectionScene.Show();
                }
                else if (selectedIndex == 3 && currentState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    _controlsScene.Show();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_playScene.WarSong);
                }
                else if (selectedIndex == 2 && currentState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    _shopScene.Show();
                }
                else if (selectedIndex == 4 && currentState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    _statScene.Show();
                }
                else if (selectedIndex == 5 && currentState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    _creditsScene.Show();
                }
                else if (selectedIndex == 6 && currentState.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if ((_creditsScene.Enabled || _controlsScene.Enabled || _statScene.Enabled || _shopScene.Enabled || _bossSelectionScene.Enabled) && currentState.IsKeyDown(Keys.Escape) )
            {

                HideAllScenes();
                _mainScene.Show();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if(_playScene.Enabled)
            {
                _playScene.Update(gameTime);
                _spriteBatch.Begin();                
                _playScene.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            if(_shopScene.Enabled)
            {
                _spriteBatch.Begin();
                _shopScene.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}