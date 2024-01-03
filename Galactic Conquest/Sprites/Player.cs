using Galactic_Conquest.GeneralScripts;
using Galactic_Conquest.OtherScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galactic_Conquest.Sprites
{
    public class Player
    {
        public List<Texture2D> playerTextures;
        private GraphicsDevice graphicsDevice;
        private int currentFrame;
        private float frameTimer;
        private float frameInterval = 0.1f;
        public PlayerHealth health;
        public Texture2D currentHealthBar;
        public SpriteFont healthFont;
        private bool isMoving;
        private SoundEffect projectileSFX;

        public Vector2 Position {  get; set; }
        public Texture2D currentTexture => playerTextures[currentFrame];

        public List<Projectile> projectiles;
        private Texture2D ProjectileTexture;
        private bool isShooting;
        private float shootTimer;
        private float shootInterval = 400;

        private List<Texture2D> thrusterTextures;
        private int thrusterFrame;
        private float thrusterFrameTimer;
        private float thrusterFrameInterval = 0.2f;
        private Game1 game1;
        
        public Rectangle Bounds => new Rectangle((int)Position.X,(int)Position.Y,currentTexture.Width,currentTexture.Height);


        //Player Skins
        public List<Texture2D> BlueShipTextures, CyanShipTextures, EmeraldShipTextures, GreenShipTextures, GreyShipTextures, PurpleShipTextures, RedShipTextures, YellowShipTextures;
        public Skin currentPlayerSkin;
        public List<Texture2D> BlueProjectiles, RedProjectiles, GreenProjectiles, PurpleProjectiles,YellowProjectiles;

        public List<Skin> ownedSkins;
        public List<Skin> BuySkins;

        public Player(List<Texture2D> textures,Vector2 startPosition, GraphicsDevice graphicsDevice,Texture2D projectileTexture,List<Texture2D> thrusterTexture,Game game)
        {
            playerTextures = textures;
            Position = startPosition;
            this.graphicsDevice = graphicsDevice;
            projectiles = new List<Projectile>();
            ProjectileTexture = projectileTexture;
            this.thrusterTextures = thrusterTexture;
            health = new PlayerHealth(6,3);
            healthFont = game.Content.Load<SpriteFont>("Fonts/HealthFont");
            this.game1 = game as Game1;
            //Ship Skins
            BlueShipTextures = LoadPlayertextures("blue",game);
            CyanShipTextures = LoadPlayertextures("cyan", game);
            EmeraldShipTextures = LoadPlayertextures("emerald", game);
            GreenShipTextures = LoadPlayertextures("green", game); ;
            GreyShipTextures = LoadPlayertextures("grey", game);
            PurpleShipTextures = LoadPlayertextures("purple", game);
            RedShipTextures = LoadPlayertextures("red", game);
            YellowShipTextures = LoadPlayertextures("yellow", game);

            BuySkins = new List<Skin>()
            {
                new Skin(1,100,"Radiant Serenity",BlueShipTextures,BlueProjectiles),
                new Skin(2,100,"Luminous Whisper",CyanShipTextures,BlueProjectiles),
                new Skin(3,100,"Galactic Explorer",EmeraldShipTextures,GreenProjectiles),
                new Skin(4,100,"Infinite Behemoth",GreenShipTextures,GreenProjectiles),
                new Skin(5,100,"Starlight Voyager",GreyShipTextures,RedProjectiles),
                new Skin(6,100,"Nebula Dreamer",PurpleShipTextures,PurpleProjectiles),
                new Skin(7,100,"Titan's Fury",RedShipTextures,RedProjectiles),
                new Skin(8,100,"Solar Leviathan",YellowShipTextures,YellowProjectiles),
                new Skin(0,0,"Celestial Breeze",textures,YellowProjectiles)

            };
            BuySkins[8].IsOwned = true;
            //projectile skins
            BlueProjectiles = LoadProjectileTextures("blue_", game);
            RedProjectiles = LoadProjectileTextures("red_", game);
            GreenProjectiles = LoadProjectileTextures("green_",game);
            PurpleProjectiles = LoadProjectileTextures("purple_",game);
            YellowProjectiles = LoadProjectileTextures("basic_fireball", game);

            //playerSkin = new Skin(100, RedShipTextures,RedProjectiles);
            //playerSkin.ApplyToPlayer(this);
            
            ownedSkins = new List<Skin>();
            for(int i = BuySkins.Count - 1; i >= 0; i--)
            {
                var skin = BuySkins[i];
                if(skin.IsOwned == true)
                {
                    ownedSkins.Add(skin);
                    BuySkins.RemoveAt(i);
                }
            }
            isMoving = false;
            projectileSFX = game.Content.Load<SoundEffect>("SFX/projectileShoot");
        }


        public void UpdateHealthBar(Game game)
        {
            string healthBarImgName = health.GetCurrentHealthBarImageName(false);
            currentHealthBar = game.Content.Load<Texture2D>($"UI/{healthBarImgName}");
        }
        
        public void Update(GameTime gameTime)
        {
            UpdateHealthBar(game1);
            HandleInput(gameTime,graphicsDevice);
            UpdateAnimation(gameTime);
            
            UpdateProjectiles(gameTime);
        }

        private void UpdateProjectiles(GameTime gameTime)
        {
            foreach(Projectile projectile in projectiles.ToList())
            {
                projectile.Update(gameTime);
                if(projectile.Position.X > graphicsDevice.Viewport.Width)
                {
                    projectiles.Remove(projectile);
                }
            }
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (thrusterFrameTimer >= thrusterFrameInterval)
            {
                thrusterFrame = (thrusterFrame + 1) % thrusterTextures.Count;
                thrusterFrameTimer = 0;
            }
            thrusterFrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(isMoving)
            { 
                
                if (frameTimer >= frameInterval)
                {
                    currentFrame = (currentFrame + 1) % 5;
                    frameTimer = 0;
                }
            }
            else
            {
                currentFrame = 4;
            }
        }

        private void HandleInput(GameTime gameTime,GraphicsDevice graphicsDevice)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState Ms = Mouse.GetState();
            Vector2 newPosition = Position;
            if((ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W)) || Ms.LeftButton == ButtonState.Pressed && Position.Y > 0)
            {
                newPosition += new Vector2(0, -2);
                AnimateUpDown(gameTime, 2, 3);
                isMoving = true;
            }
            else if((ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S)) || Ms.RightButton == ButtonState.Pressed && Position.Y < graphicsDevice.Viewport.Height - currentTexture.Height )
            {
                newPosition += new Vector2(0, 2);
                AnimateUpDown(gameTime,0,1);
                isMoving = true;
            }
            else if ((ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) && Position.X > 0)
            {
                newPosition += new Vector2(-2, 0);
                isMoving = true;
            }
            else if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) && Position.X < graphicsDevice.Viewport.Width - currentTexture.Width)
            {
                newPosition += new Vector2(2, 0);
                isMoving = true;
            }
            else
            {
                currentFrame = 4;
                isMoving = false;
            }
            if(newPosition.Y >=0 && newPosition.Y + currentTexture.Height <= graphicsDevice.Viewport.Height)
            {
                Position = newPosition;
            }
            if(ks.IsKeyDown(Keys.E))
            {
                isShooting = true;
                ShootProjectile(gameTime);
            }
            else
            {
                isShooting = false;
            }
        }
        public void TakeDamage(int damage)
        {
            health.TakeDamage(damage);
        }
        private void ShootProjectile(GameTime gameTime)
        {
            shootTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(shootTimer >= shootInterval)
            {
                Vector2 projectilePosition = new Vector2(Position.X + (currentTexture.Width), Position.Y +30);
                Projectile newProjectile = new Projectile(ProjectileTexture, projectilePosition, new System.Numerics.Vector2(5, 0));
                projectileSFX.Play();
                projectiles.Add(newProjectile);
                shootTimer = 0;
            }
        }

        private void AnimateUpDown(GameTime gameTime, int sFrame, int eFrame)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= frameInterval)
            {
                currentFrame = (currentFrame >= sFrame && currentFrame <= eFrame) ? currentFrame +1: sFrame;
            }
            if(currentFrame > eFrame)
            {
                currentFrame = sFrame;
            }
            frameTimer = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            Vector2 thrusterPosition = new Vector2(Position.X -15, Position.Y + 35);
            spriteBatch.Draw(thrusterTextures[thrusterFrame],thrusterPosition,Color.White);
            spriteBatch.Draw(currentTexture,Position,Color.White);
            if(currentHealthBar != null)
            {
                spriteBatch.Draw(currentHealthBar, new Vector2(0, 0), Color.White);
            }
            spriteBatch.DrawString(healthFont,$"{health.Lives}",new Vector2(24,7),Color.WhiteSmoke);
            foreach(Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            Position = new Vector2((graphicsDevice.Viewport.Width - playerTextures[4].Width / 2) / 2, (graphicsDevice.Viewport.Height - playerTextures[4].Height / 2)/2);

            health.ResetLives();
        }
        public List<Texture2D> LoadPlayertextures(string Color, Game game)
        {
            List<Texture2D> textures = new List<Texture2D>();
            for (int i = 1; i <= 5; i++)
            {
                string textureName = $"Assests/Player/{Color}_ship_{i}";
                Texture2D texture = game.Content.Load<Texture2D>(textureName);
                textures.Add(texture);
            }
            return textures;
        }
        public void SetTextures(List<Texture2D> textures)
        {
            playerTextures = textures;
            
        }

        public void SetProjectileTextures(List<Texture2D> projectileTextures)
        {
           if (projectileTextures != null)
            {
                ProjectileTexture = projectileTextures[3];
            }
        }

        private List<Texture2D> LoadProjectileTextures(string Color, Game game)
        {
            List<Texture2D> textures = new List<Texture2D>();
            for (int i = 1; i <= 6; i++)
            {
                string textureName = $"Assests/Projectiles/{Color}{i}";
                Texture2D texture = game.Content.Load<Texture2D>(textureName);
                textures.Add(texture);
            }
            return textures;
        }

        public void LoadOwnedSkinPlayer(List<int> ownedSkinNumbers)
        {

            foreach(int skinNumber in ownedSkinNumbers)
            {
                Skin skin = GetSkinByNumber(skinNumber);
                if(skin != null )
                {
                    skin.IsOwned = true;
                    ownedSkins.Add(skin);
                    BuySkins.Remove(skin);
                }
            }
        }

        private Skin GetSkinByNumber(int skinNumber)
        {
           foreach(Skin skin in BuySkins)
            {
                if(skin.SkinNumber == skinNumber)
                {
                    return skin;
                }
            }
           return null;
        }
    }

}
