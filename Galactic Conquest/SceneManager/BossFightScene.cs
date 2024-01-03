using Galactic_Conquest.OtherScripts;
using Galactic_Conquest.Sprites;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Galactic_Conquest.SceneManager
{
    public class BossFightScene : GameScene
    {
        private FlashBoss flashBoss;
        private VortexBoss vortexBoss;
        private Crimson crimsonBoss;
        private FrostbiteBoss frostbiteBoss;
        public Player player;
        private SpriteBatch spriteBatch;
        private List<Texture2D> playerTextures;
        private BossCollisionManager collisionManager;
        private Boss selectedBoss;
        private Victory victory;
        private Defeat deafeat;
        private List<Texture2D> planetTextures = new List<Texture2D>();
        private List<Texture2D> starTextues = new List<Texture2D>();
        private List<Texture2D> moonTextures = new List<Texture2D>();
        private List<Texture2D> blackholeTextures = new List<Texture2D>();
        private List<Texture2D> nebulaTextures = new List<Texture2D>();
        private List<Texture2D> smlNebulaTextures = new List<Texture2D>();
        private Sprites.Background _background;
        MainScene mainScene;
        private Song fightSong;

        public BossFightScene(Game game,SpriteBatch spriteBatch,string bossName,MainScene _mainMenuScene, PlayScene playScene) : base(game)
        {
            flashBoss = new FlashBoss("Assests/Enemy/enemy_flash", Game,spriteBatch);
            flashBoss.Position = new Vector2(700, 100);

            vortexBoss = new VortexBoss("Assests/Enemy/enemy_cyclic", Game, spriteBatch);
            vortexBoss.Position = new Vector2(700, 200);

            crimsonBoss = new Crimson("Assests/Enemy/enemy_track", Game, spriteBatch);
            crimsonBoss.Position = new Vector2(700, 200);

            frostbiteBoss = new FrostbiteBoss("Assests/Enemy/enemy_rage", Game, spriteBatch);
            frostbiteBoss.Position = new Vector2(600,200);
            mainScene = _mainMenuScene;
            this.spriteBatch = spriteBatch;
            playerTextures = new List<Texture2D>()
            {
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Down1"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Down2"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Up1"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Up2"),
                game.Content.Load<Texture2D>("Assests/Player/basic_ship_Idle"),
            };
            List<Texture2D> playerProjectiles = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball1"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball2"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball3"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball4"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball5"),
                game.Content.Load<Texture2D>("Assests/Projectiles/basic_fireball6"),

            };
            List<Texture2D> thrusterTextures = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters1"),
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters2"),
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters3"),
                game.Content.Load<Texture2D>("Assests/Player/Thruster/thrusters4"),
            };
            selectedBoss = CreateBoss(bossName);

            player = new Player(playerTextures, new Vector2((game.GraphicsDevice.Viewport.Width - playerTextures[4].Width) / 2 - 100, (game.GraphicsDevice.Viewport.Height - playerTextures[4].Height) / 2), GraphicsDevice, playerProjectiles[0], thrusterTextures, Game);
            player.health = new GeneralScripts.PlayerHealth(6, 1);
            collisionManager = new BossCollisionManager();
            victory = new Victory(game, GraphicsDevice,spriteBatch, _mainMenuScene, this,playScene);
            deafeat = new Defeat(game, GraphicsDevice, spriteBatch, _mainMenuScene, this,playScene);
            game.Components.Add(victory);
            game.Components.Add(deafeat);
            InitializeBackground(game);
            fightSong = game.Content.Load<Song>("Music/BattleMusic");
        }
        private void InitializeBackground(Game game)
        {
            LoadCelestialBodytextures("planet", 16, planetTextures, game);
            LoadCelestialBodytextures("star", 2, starTextues, game);
            LoadCelestialBodytextures("moon", 8, moonTextures, game);
            LoadCelestialBodytextures("blackhole", 2, blackholeTextures, game);
            LoadCelestialBodytextures("nebula", 1, nebulaTextures, game);
            LoadCelestialBodytextures("smlNebula", 1, nebulaTextures, game);

            _background = new Sprites.Background(GraphicsDevice, planetTextures, starTextues, moonTextures, blackholeTextures, nebulaTextures, smlNebulaTextures);
        }
        private void LoadCelestialBodytextures(string Name, int Count, List<Texture2D> textureList, Game game)
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
            KeyboardState ks = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            if(ks.IsKeyDown(Keys.Escape)){
               this.Hide();
                mainScene.Show();
            }

            selectedBoss.Update(gameTime);
            player.Update(gameTime);
            _background.Update(gameTime);
            collisionManager.CheckBossCollisions(player, new List<Boss> { selectedBoss },deafeat,victory,this);
            base.Update(gameTime);

        }
        public override void Show()
        {
            base.Show();
            MediaPlayer.Stop();
            MediaPlayer.Play(fightSong);
        }
        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            _background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            
            selectedBoss.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private Boss CreateBoss(string name)
        {
            switch (name.ToLower())
            {
                case "flash":
                    return flashBoss;
                case "vortex":
                    return vortexBoss;
                case "crimson":
                    return crimsonBoss;
                case "frostbite":
                    return frostbiteBoss;
                    default: return null;
            }
            
        }
    }
}
