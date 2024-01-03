using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class VortexBoss : Boss
    {
        private float spinCoolDown = 1.6f;
        private float stopSpinning = 3.0f;
        private float spinTimer = 0.0f;
        private float projectileCooldown = 1.6f;
        private float projectileTimer = 0.0f;

        private bool isSpinning = false;
        private SpriteBatch spriteBatch;
        public List<Projectile> projectiles {  get; private set; }
        private Texture2D projectileTexture;
        public VortexBoss(string TexturePath, Game game, SpriteBatch spriteBatch) : base(TexturePath, game, spriteBatch)
        {
            projectiles = new List<Projectile>();
            projectileTexture = game.Content.Load<Texture2D>("Assests/Projectiles/red_5");
            this.spriteBatch = spriteBatch;
            this.Health = 100;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(!this.IsDefeated)
            {
                HandleVortexMovements(gameTime);
                HandleProjectileShoting(gameTime);
                HandleSpinning(gameTime);
            }

            foreach(var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            projectiles.RemoveAll(p => !p.IsWithinBounds(Game.GraphicsDevice.Viewport.Bounds));
        }


        private void HandleProjectileShoting(GameTime gameTime)
        {
            if(isSpinning)
            {
                projectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (projectileTimer >= projectileCooldown)
                {
                    Shootprojectiles();

                    projectileTimer = 0.0f;
                }
            }
        }

        private void Shootprojectiles()
        {
            for(int i = 0; i < 26; i++)
            {
                float angle = MathHelper.ToRadians(i * 20f);
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                float speed = 4.5f;
                Projectile projectile = new Projectile(projectileTexture, Position, direction * speed);
                projectiles.Add(projectile);
            }
        }

        private void HandleSpinning(GameTime gameTime)
        {
            spinTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(spinTimer >= spinCoolDown)
            {
                isSpinning = true;
                
                if(spinTimer >= 5.0f)
                {
                    spinTimer = 0.0f;
                    isSpinning = false;
                }
            }
            else
            {
                isSpinning = false;
            }
        }
        private float verticleMovTimer = 0f;
        private float verticleMoveDuration = 0f;
        private float verticalMoveCooldown = 0f;
        private bool isMoovingUp = true;
        private Random random = new Random();
        private void HandleVortexMovements(GameTime gameTime)
        {
            float positionX = Position.X;
            float positionY = Position.Y;
            float leftwardSpeed = 0.1f;
            positionX-= leftwardSpeed;

            verticalMoveCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(verticalMoveCooldown <=0)
            {
                if(isMoovingUp)
                {
                    positionY -= 0.3f;
                    verticleMoveDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(verticleMoveDuration >= 2.0f)
                    {
                        isMoovingUp =false;
                        verticalMoveCooldown = 2.0f;
                        verticleMovTimer = 0.0f;
                    }

                }
                else
                {
                    positionY += 0.3f;
                    verticleMoveDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if(verticleMoveDuration >= 5.0f)
                    {
                        isMoovingUp = true;
                        verticalMoveCooldown = 2.0f + (float)random.NextDouble() * 3.0f;
                        verticleMoveDuration = 0.0f;
                    }
                }
            }

            float minY = GraphicsDevice.Viewport.Bounds.Top + 100f;
            float maxY = GraphicsDevice.Viewport.Bounds.Bottom - 100f;

            positionY = MathHelper.Clamp(positionY, minY, maxY);
            Position = new Vector2(positionX, positionY);

            if(isSpinning
                )
            {
                Rotation += 0.09f;
            }
        }
        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            foreach(var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            DrawProjectiles(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
