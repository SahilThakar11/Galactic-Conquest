using Galactic_Conquest.SceneManager;
using Galactic_Conquest.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.OtherScripts
{
    public class BossCollisionManager
    {
        public void CheckBossCollisions(Player player,List<Boss> bosses,Defeat defeatScene,Victory victoryScene,BossFightScene bossFightScene)
        {
            foreach (Boss boss in bosses)
            {
                if ((player.Bounds.Intersects(boss.Bounds)))
                {
                    player.TakeDamage(1);
                    player.Position = new Vector2(20, 200);
                    if(player.health.IsGameOver == true
                        )
                    {
                        bossFightScene.Hide();
                        defeatScene.Show();
                    }
                }
            }
            VortexBoss vortexBoss = bosses.Find(b => b is VortexBoss) as VortexBoss;
            FlashBoss flashBoss = bosses.Find(b => b is FlashBoss) as FlashBoss;
            Crimson crimsonBoss = bosses.Find(b => b is Crimson) as Crimson;
            FrostbiteBoss frostbitBoss = bosses.Find(b => b is FrostbiteBoss) as FrostbiteBoss;
            if (vortexBoss != null)
            {
                HandleVortexCollision(vortexBoss, player,defeatScene, bossFightScene,victoryScene);
            }
            if (flashBoss != null)
            {
                HandleFlashCollision(flashBoss, player, defeatScene, bossFightScene,victoryScene);
            }
            if(crimsonBoss != null)
            {
                HandleCrimsonCollision(crimsonBoss, player, defeatScene, bossFightScene,victoryScene);
            }
            if(frostbitBoss != null)
            {
                HandleFrosbitCollision(frostbitBoss, player, defeatScene, bossFightScene,victoryScene);
            }
            
        }

        private void HandleFrosbitCollision(FrostbiteBoss frostbitBoss, Player player,Defeat defeatScene, BossFightScene bossFightScene, Victory victoryScene)
        {
            List<Projectile> projectilesToRemove = new List<Projectile>();
            foreach (var projectile in frostbitBoss.projectiles)
            {
                if (projectile.Bounds.Intersects(player.Bounds))
                {
                    player.TakeDamage(1);
                    projectilesToRemove.Add(projectile);
                    if (player.health.IsGameOver == true
                        )
                    {
                        bossFightScene.Hide();
                        defeatScene.Show();
                    }

                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                frostbitBoss.projectiles.Remove(projectile);
            }
            projectilesToRemove.Clear();
            foreach (var projectile in frostbitBoss.missiles)
            {
                if (projectile.Bounds.Intersects(player.Bounds))
                {
                    player.TakeDamage(3);
                    projectilesToRemove.Add(projectile);
                    if (player.health.IsGameOver == true
                       )
                    {
                        bossFightScene.Hide();
                        defeatScene.Show();
                    }
                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                frostbitBoss.missiles.Remove(projectile);
            }
            foreach(var projectile in player.projectiles)
            {
                if (projectile.Bounds.Intersects(frostbitBoss.Bounds))
                {
                    frostbitBoss.TakeDamage(3);
                    projectilesToRemove.Add(projectile);
                    if(frostbitBoss.IsDefeated == true)
                    {
                        bossFightScene.Hide();
                        victoryScene.Show();

                    }

                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                player.projectiles.Remove(projectile);
            }
        }

        private void HandleCrimsonCollision(Crimson crimsonBoss, Player player, Defeat defeatScene, BossFightScene bossFightScene, Victory victoryScene)
        {
            if(crimsonBoss.laserBeam != null && crimsonBoss.laserBeam.GetBoundingBox().Intersects(player.Bounds))
            {
                player.TakeDamage(1);
                
            }
            List<Projectile> projectilesToRemove = new List<Projectile>();
            foreach (var projectile in player.projectiles)
            {
                if (projectile.Bounds.Intersects(crimsonBoss.Bounds))
                {
                    crimsonBoss.TakeDamage(3);
                    projectilesToRemove.Add(projectile);
                    if (crimsonBoss.IsDefeated == true)
                    {
                        bossFightScene.Hide();
                        victoryScene.Show();
                    }
                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                player.projectiles.Remove(projectile);
                if (player.health.IsGameOver == true
                       )
                {
                    bossFightScene.Hide();
                    defeatScene.Show();
                }
            }
            projectilesToRemove.Clear();
        }

        private void HandleFlashCollision(FlashBoss flashBoss, Player player, Defeat defeatScene, BossFightScene bossFightScene, Victory victoryScene)
        {
            List<Projectile> projectilesToRemove = new List<Projectile>();
            foreach (var projectile in flashBoss.projectiles)
            {
                if (projectile.Bounds.Intersects(player.Bounds))
                {
                    player.TakeDamage(1);
                    projectilesToRemove.Add(projectile);
                    if (player.health.IsGameOver == true
                       )
                    {
                        bossFightScene.Hide();
                        defeatScene.Show();
                    }
                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                flashBoss.projectiles.Remove(projectile);
            }
            projectilesToRemove.Clear();
            foreach (var projectile in player.projectiles)
            {
                if (projectile.Bounds.Intersects(flashBoss.Bounds))
                {
                    flashBoss.TakeDamage(3);
                    projectilesToRemove.Add(projectile);
                    if (flashBoss.IsDefeated == true)
                    {
                        bossFightScene.Hide();
                        victoryScene.Show();
                    }
                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                player.projectiles.Remove(projectile);
            }
        }

        private void HandleVortexCollision(VortexBoss vortexBoss, Player player, Defeat defeatScene, BossFightScene bossFightScene, Victory victoryScene)
        {
            List<Projectile> projectilesToRemove = new List<Projectile>();
            foreach(var projectile in vortexBoss.projectiles)
            {
                if(projectile.Bounds.Intersects(player.Bounds))
                {
                    player.TakeDamage(1);
                    projectilesToRemove.Add(projectile);
                    if (player.health.IsGameOver == true
                       )
                    {
                        bossFightScene.Hide();
                        defeatScene.Show();
                    }
                }
            }
            foreach(var projectile in projectilesToRemove)
            {
                vortexBoss.projectiles.Remove(projectile);
            }
            projectilesToRemove.Clear();
            foreach (var projectile in player.projectiles)
            {
                if (projectile.Bounds.Intersects(vortexBoss.Bounds))
                {
                    vortexBoss.TakeDamage(3);
                    projectilesToRemove.Add(projectile);
                    if (vortexBoss.IsDefeated == true)
                    {
                        bossFightScene.Hide();
                        victoryScene.Show();
                    }
                }
            }
            foreach (var projectile in projectilesToRemove)
            {
                player.projectiles.Remove(projectile);
            }
        }
    }
}
