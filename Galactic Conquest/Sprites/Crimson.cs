using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class Crimson : Boss
    {
        private float verticalSpeed = 50.0f;
        private float laserCooldown = 3.5f;
        private float laserTimer = 0.0f;
        private float laserDuration = 1.0f;
        
        private List<Texture2D> laserTextures;
        public LaserBeam laserBeam;
        private bool isShooting = false;
        private SpriteBatch spriteBatch;
        private SoundEffect laserSFX;

        public Crimson(string TexturePath, Game game, SpriteBatch spriteBatch) : base(TexturePath, game, spriteBatch)
        {
            laserTextures = new List<Texture2D>();
            for(int i = 1; i <= 12; i++)
            {
                laserTextures.Add(game.Content.Load<Texture2D>($"Assests/Projectiles/enm_{i}"));
            }
            laserBeam = null;
            this.spriteBatch = spriteBatch;
            this.Health = 100;
            laserSFX = game.Content.Load<SoundEffect>("SFX/heavyBeam");
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(!isShooting )
            {
                HandleMovements(gameTime);
            }
            HandleLaserAttack(gameTime);
        }
        private void HandleMovements(GameTime gameTime)
        {
            float positionX = Position.X;
            float positionY = Position.Y;

            positionY += verticalSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(positionY < 70f || positionY > (GraphicsDevice.Viewport.Bounds.Height - 70f))
            {
                verticalSpeed *= -1;
            }

            Position = new Vector2(positionX, positionY);
        }

        private float GetRandomeVerticleDirection()
        {
            Random random = new Random();
            return random.Next(2) == 0 ? 1 : -1;
        }

        private void HandleLaserAttack(GameTime gameTime)
        {
            if(laserBeam == null)
            {
                laserTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(laserTimer >= laserCooldown)
                {
                    isShooting = true;
                    laserSFX.Play();
                    StartLaserAttack();
                    laserTimer = 0.0f;

                }

                
            }
            else
            {
                laserBeam.Update(gameTime);
                if(!laserBeam.IsActive)
                {
                    laserBeam = null;
                    MediaPlayer.Stop();
                    isShooting = false;
                }
            }
           
        }

        private void StartLaserAttack()
        {
            Vector2 laserStartPosition = new Vector2(Position.X-700, Position.Y + texture.Height / 2 -80);
            laserBeam = new LaserBeam(laserTextures, laserStartPosition, 2.0f);
        }
        public void DrawLaserBeam(SpriteBatch spriteBatch)
        {
            laserBeam?.Draw(spriteBatch);
        }
        public override void Draw(GameTime gameTime)
        {
            DrawLaserBeam(spriteBatch);
            base.Draw(gameTime);
            
        }
    }
}
