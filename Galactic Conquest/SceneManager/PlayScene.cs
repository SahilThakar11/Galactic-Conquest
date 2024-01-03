using Galactic_Conquest.GeneralScripts;
using Galactic_Conquest.OtherScripts;
using Galactic_Conquest.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Galactic_Conquest.SceneManager
{
    public class PlayScene : GameScene
    {
        public Player _player;
        private Sprites.Background _background;
        private Projectile _projectile;
        private EnemySpawner _enemySpawner;       
        private CollisionManager _collisionManager;
        private GameoverScene _gameoverScene;
        private MainScene _mainMenuScene;
        private KeyboardState os;
        private List<Texture2D> planetTextures = new List<Texture2D>();
        private List<Texture2D> starTextues = new List<Texture2D>();
        private List<Texture2D> moonTextures = new List<Texture2D>();
        private List<Texture2D> blackholeTextures = new List<Texture2D>();
        private List<Texture2D> nebulaTextures = new List<Texture2D>();
        private List<Texture2D> smlNebulaTextures = new List<Texture2D>();
        private bool isEscapedKeyPressed = false;
        private bool isPaused = false;
        public TimeSpan elapsedTime;
        private SpriteFont timerFont;
        private SpriteBatch _spriteBatch;
        public Song WarSong;
        public int TotalStars { get;  set; }
        public int PlayerStars { get; set; }
        public TimeSpan HighestTime { get;  set; }

        private Game game1;

        public PlayScene(Game game,SpriteBatch spriteBatch,MainScene mainScene) : base(game)
        {
            
            LoadPlayerData();
            game1 = game;
            List<Texture2D> playerProjectiles = new List<Texture2D> 
            {
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball1"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball2"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball3"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball4"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball5"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball6"),
                
            };
            List<Texture2D> playerTextures = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Down1"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Down2"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Up1"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Up2"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Idle"),

            };
            List<Texture2D> thrusterTextures = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters1"),
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters2"),
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters3"),
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters4"),
            };
            _player = new Player(playerTextures,new Vector2((game.GraphicsDevice.Viewport.Width - playerTextures[4].Width)/2 - 100,(game.GraphicsDevice.Viewport.Height - playerTextures[4].Height) / 2), GraphicsDevice, playerProjectiles[0],thrusterTextures,game);

            InitializeBackground(game);
            _enemySpawner = new EnemySpawner(game,GraphicsDevice);
            _collisionManager = new CollisionManager();
            _mainMenuScene = mainScene;
            _gameoverScene = new GameoverScene(game,GraphicsDevice,game.Content.Load<SpriteFont>("Fonts/GameoverFont"), game.Content.Load<SpriteFont>("Fonts/RedirectFont"),spriteBatch, _mainMenuScene,this);
            game.Components.Add(_gameoverScene);
            timerFont = game.Content.Load<SpriteFont>("Fonts/HealthFont");
            _spriteBatch = spriteBatch;
            WarSong = game.Content.Load<Song>("Music/BattleMusic");

        }
        public override void Initialize()
        {
            game1.IsMouseVisible = true;
        }
        private void InitializeBackground(Game game)
        {
            LoadCelestialBodytextures("planet", 16, planetTextures,game);
            LoadCelestialBodytextures("star", 2, starTextues, game);
            LoadCelestialBodytextures("moon", 8, moonTextures, game);
            LoadCelestialBodytextures("blackhole", 2, blackholeTextures, game);
            LoadCelestialBodytextures("nebula", 1, nebulaTextures, game);
            LoadCelestialBodytextures("smlNebula", 1, nebulaTextures, game);

            _background = new Sprites.Background(GraphicsDevice, planetTextures, starTextues, moonTextures, blackholeTextures, nebulaTextures, smlNebulaTextures);
        }
        private void LoadCelestialBodytextures(string Name, int Count, List<Texture2D> textureList,Game game)
        {
            for (int i = 1; i <= Count; i++)
            {
                string textureName = $"Assests/Background/{Name}{i}";
                Texture2D texture = game.Content.Load<Texture2D>(textureName);
                textureList.Add(texture);
            }
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            
            if (ks.IsKeyDown(Keys.M) && os.IsKeyUp(Keys.M))
            {
                MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }
           
            if(ks.IsKeyDown(Keys.Escape))
            {
                this.Hide();
                SavePlayerData();
                _mainMenuScene.Show();
                ResetGame();
            }
            else
            {
               
            }
            
                elapsedTime += gameTime.ElapsedGameTime;
                if(_player != null)
                {
                    PlayerStars = (int)(elapsedTime.TotalSeconds / 5);
                    _player.Update(gameTime);
                }
                
                _background.Update(gameTime);
                _enemySpawner.Update(gameTime);
                _collisionManager.CheckCollisions(_player, _enemySpawner, _gameoverScene,this);
                _gameoverScene.Update(gameTime);
            
            os = ks;
            base.Update(gameTime);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            _enemySpawner.Draw(spriteBatch);
            
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(timerFont,$"{elapsedTime:hh\\:mm\\:ss\\.ff}",new Vector2(600,0),Color.White);
            _spriteBatch.End();
        }
        public void ResetGame()
        {
            _player.Reset();
            _enemySpawner.ResetEnemyState();
            _background.Reset();
            elapsedTime = TimeSpan.Zero;
        }
        public override void Hide()
        {
            
            base.Hide();
        }
        public override void Show()
        {
            MediaPlayer.Play(WarSong);
            base.Show(); 
            
        }
        private const int MaxhighScores = 5;
        private List<TimeSpan> highScores = new List<TimeSpan>();
        private string saveFilePath = "player_details.txt";
        public List<int> ownedSkinNumbers { get; set; } = new List<int>();
        public void SavePlayerData()
        {
            try
            {
                List<TimeSpan> scores = LoadHighScores();
                TimeSpan currentSurvivalTime = elapsedTime;

                scores.Add(currentSurvivalTime);
                scores.Sort((a,b) => -a.CompareTo(b));

                highScores = scores.Take(MaxhighScores).ToList();

                int CurrentStars = LoadStars();
                using (StreamWriter file = new StreamWriter(saveFilePath))
                {
                   
                    for(int i=0;i< highScores.Count; i++)
                    {
                        file.WriteLine($"HighScore{i + 1}-{highScores[i]}");
                    }
                    int newStars = CurrentStars + PlayerStars;
                    file.WriteLine($"TotalStars-{newStars}");
                    file.WriteLine($"OwnedSkins-{string.Join(',',ownedSkinNumbers)}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
                
            }
        }
        private int LoadStars()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    string[] lines = File.ReadAllLines(saveFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('-');
                        if (parts.Length == 2)
                        {
                            string Key = parts[0];
                            string Value = parts[1];

                            if (Key == "TotalStars")
                            {
                                return int.Parse(Value);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
            return 0;
        }

       public List<TimeSpan> LoadHighScores()
        {
            List<TimeSpan> highScores = new List<TimeSpan>();

            try
            {
                if(File.Exists(saveFilePath))
                {
                    string[] lines =File.ReadAllLines(saveFilePath);
                    foreach(string line in lines)
                    {
                        string[] parts = line.Split('-');
                        if(parts.Length == 2)
                        {
                            string Key = parts[0];
                            string Value = parts[1];

                            if(Key.StartsWith("HighScore"))
                            {
                                TimeSpan score = TimeSpan.Parse(Value);
                                highScores.Add(score);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return highScores;
        }

        public void LoadPlayerData()
        {
            try
            {
                if(File.Exists(saveFilePath))
                {
                    string[] lines = File.ReadAllLines(saveFilePath);
                    foreach(string line in lines)
                    {
                        string[] parts = line.Split('-');
                        if(parts.Length == 2)
                        {
                            string Key = parts[0];
                            string Value = parts[1];

                            switch(Key)
                            {
                                case "SurvivalTime":
                                    HighestTime = TimeSpan.Parse(Value);
                                    break;
                                case "TotalStars":
                                    TotalStars = int.Parse(Value);
                                    break;
                                case "OwnedSkins":
                                    LoadOwnedSkins(Value);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.Write("Error");
            }
        }

        private void LoadOwnedSkins(string value)
        {
            try
            {
                this.ownedSkinNumbers = value.Split(',').Select(int.Parse).ToList();
                if(_player != null)
                {
                    _player.LoadOwnedSkinPlayer(this.ownedSkinNumbers);
                }
                    

            }
            catch (Exception)
            {   

            }
        }
    }
}
