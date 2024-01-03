using Galactic_Conquest.SceneManager;
using Galactic_Conquest.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.GeneralScripts
{
    public class CollisionManager
    {
        public void CheckCollisions(Player player,EnemySpawner enemySpawner,GameoverScene gameoverScene,PlayScene playScene)
        {
            foreach(Projectile playerProjectile in player.projectiles.ToList())
            {
                foreach(Enemy enemy in enemySpawner.enemies.ToList()) 
                {
                    if(playerProjectile.Bounds.Intersects(enemy.Bounds))
                    {

                        player.projectiles.Remove(playerProjectile);

                            enemySpawner.enemies.Remove(enemy);

                    }
                }
            }

            foreach(Enemy enemy in enemySpawner.enemies.ToList() )
            {
                foreach(EnemyProjectile enemyProjectile in enemy.projectiles.ToList())
                {
                    if(enemyProjectile.Bounds.Intersects(player.Bounds))
                    {
                        enemy.projectiles.Remove(enemyProjectile);
                        player.TakeDamage(1);
                        if (player.health.IsGameOver == true)
                        {
                            playScene.Hide();
                            gameoverScene.Show();
                        }
                    }
                }
                if(player.Bounds.Intersects(enemy.Bounds))
                {
                    enemySpawner.enemies.Remove(enemy);
                    player.TakeDamage(1);
                    if (player.health.IsGameOver == true)
                    {
                        playScene.Hide();
                        gameoverScene.Show();
                    }
                }
            }


        }
    }
}
