using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.GeneralScripts
{
    public class PlayerHealth
    {
        private int health;
        private int lives;
        public bool IsGameOver;
        public int Health => health;
        public int Lives => lives;
        public PlayerHealth(int inHealth, int inlives)
        {
            health = inHealth;
            lives = inlives;
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                lives--;
                if (lives <= 0)
                {
                    IsGameOver = true;
                }
                else
                {
                    health = 7;
                    IsGameOver = false;
                }
                
            }
        }
        public void ResetLives()
        {
            lives = 3;
            health = 6;
            IsGameOver = false;
        }
        public string GetCurrentHealthBarImageName(bool isShieled)
        {
            int healthIndex;

            switch(health)
            {
                case 6: healthIndex = 1; break;
                case 5: healthIndex = 2; break;
                case 4: healthIndex = 3; break;
                case 3: healthIndex = 4; break;
                case 2: healthIndex = 5; break;
                case 1: healthIndex = 6; break;
                case 0: healthIndex = 7; break;
                default:healthIndex = 1; break;
            }
            if (isShieled)
            {
                return $"Health_Bar_Golden_{healthIndex}";
            }
            else
            {
                return $"Health_Bar{healthIndex}";
            }
        }
    }
}
