using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galactic_Conquest.Sprites
{
    public class EnemySpawner
    {
        public List<Enemy> enemies;
        private Texture2D enemyProjectileTexture;
        private Random random;
        private TimeSpan spawnDelay;
        private TimeSpan elapsedSpawnTime;
        private Texture2D enemyTexture1, enemyTexture2, enemyTexture3;
        private GraphicsDevice _graphicsDevice;
        private Game game1;
        private TimeSpan elapsedSwitchTime;
        private int initialHealth;

        public EnemySpawner(Game game,GraphicsDevice graphicsDevice)
        {
            enemies = new List<Enemy>();
            random = new Random();
            spawnDelay = TimeSpan.FromSeconds(4);
            elapsedSpawnTime = TimeSpan.FromSeconds(5);
            enemyTexture1 = game.Content.Load<Texture2D>("Assests/Enemy/enemy_basic");
            enemyTexture2 = game.Content.Load<Texture2D>("Assests/Enemy/enemy_bot");
            enemyTexture3 = game.Content.Load<Texture2D>("Assests/Enemy/enemy_bubble");
            game1 = game;
            _graphicsDevice = graphicsDevice;
        }
        public void Update(GameTime gameTime)
        {
            elapsedSpawnTime += gameTime.ElapsedGameTime;
            elapsedSwitchTime += gameTime.ElapsedGameTime;
            if(elapsedSwitchTime > TimeSpan.FromSeconds(60))
            {
                foreach(Enemy enemy in enemies)
                {
                    spawnDelay = TimeSpan.FromSeconds(3);
                    enemy.ChangeTexture(enemyTexture2);
                    
                }
            }
            if (elapsedSwitchTime > TimeSpan.FromSeconds(120))
            {
                spawnDelay = TimeSpan.FromSeconds(2);
                foreach (Enemy enemy in enemies)
                {
                    enemy.ChangeTexture(enemyTexture3);  
                }
            }
            if (elapsedSpawnTime >= spawnDelay)
            {
                SpawnEnemy();
                elapsedSpawnTime = TimeSpan.Zero;
            }
            foreach(Enemy enemy in enemies.ToList())
            {
                enemy.Update(gameTime);
                enemy.ShootProjectile(gameTime);
                
            }
        }
      
        private void SpawnEnemy()
        {
            System.Numerics.Vector2 initialPosition = new System.Numerics.Vector2(800, random.Next(20,400));
            float enemySpeed = 50f;
            Enemy newEnemy = new Enemy(enemyTexture1, initialPosition, -enemySpeed, _graphicsDevice, game1);
            enemies.Add(newEnemy);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
        public void ResetEnemyState()
        {
            enemies.Clear();
            elapsedSwitchTime = TimeSpan.Zero;
        }
    }
}
