using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galactic_Conquest.Sprites
{
    public class Enemy
    {
        private Texture2D texture;
        private System.Numerics.Vector2 position;
        private System.Numerics.Vector2 velocity;
        private float speed;
        public int Health;
        public List<EnemyProjectile> projectiles {  get; set; }
        public Texture2D EnemyProjectileTexture { get; set; }
        private GraphicsDevice GraphicsDevice;
        public bool isOver;

        public Rectangle Bounds => new((int)position.X,(int)position.Y,texture.Width,texture.Height);
        public Enemy(Texture2D texture, System.Numerics.Vector2 position, float speed,GraphicsDevice graphicsDevice,Game game)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.Health = 1;
            GraphicsDevice = graphicsDevice;
            velocity = new System.Numerics.Vector2(speed,0);
            EnemyProjectileTexture = game.Content.Load<Texture2D>("Assests/Projectiles/enemy_redbeam1");
            projectiles = new List<EnemyProjectile>();
        }

        public void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (EnemyProjectile projectile in projectiles.ToList())
            {
                projectile.Update();
                if(projectile.Position.X < 0|| projectile.Position.X > GraphicsDevice.Viewport.Width)
                {
                    projectiles.Remove(projectile);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Microsoft.Xna.Framework.Color.White);
            foreach(EnemyProjectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

        }
        public void ShootProjectile(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds % 2000 < gameTime.ElapsedGameTime.TotalMilliseconds)
            {
                System.Numerics.Vector2 projectilePosition = new System.Numerics.Vector2(position.X, position.Y + (EnemyProjectileTexture.Height / 2)+10);
                System.Numerics.Vector2 projectileVelocity = new System.Numerics.Vector2(-4f, 0);

                EnemyProjectile newProjectile = new EnemyProjectile(EnemyProjectileTexture,projectilePosition, projectileVelocity);
                projectiles.Add(newProjectile);
            }
        }
        public void ChangeTexture(Texture2D enemyTexture2)
        {
            texture = enemyTexture2;
        }
    }
}
