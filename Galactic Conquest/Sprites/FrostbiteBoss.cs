using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    
    public class FrostbiteBoss : Boss
    {
        private float projectileCooldown = 3.0f;
        private float projectileTimer = 0.0f;
        private float missileCooldown = 5.0f;
        private float missileTimer = 0.0f;

        public List<Projectile> projectiles;
        public List<Projectile> missiles;
        private Texture2D missibleTexture;
        private Texture2D projectileTexture;
        private SpriteBatch spriteBatch;
        public FrostbiteBoss(string TexturePath, Game game, SpriteBatch spriteBatch) : base(TexturePath, game, spriteBatch)
        {
            projectiles = new List<Projectile>();
            missiles = new List<Projectile>();
            missibleTexture = game.Content.Load<Texture2D>("Assests/Projectiles/missile-1");
            projectileTexture = game.Content.Load<Texture2D>("Assests/Projectiles/enm_y4");
            this.spriteBatch = spriteBatch;
            this.Health = 100;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HandleProjectileShooting(gameTime);
            HandleMissileLaunching(gameTime);

            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            projectiles.RemoveAll(p =>!p.IsWithinBounds(Game.GraphicsDevice.Viewport.Bounds));

            foreach(var missile in missiles)
            {
                missile.Update(gameTime);
            }
            missiles.RemoveAll(m => !m.IsWithinBounds(Game.GraphicsDevice.Viewport.Bounds));

        }

        private void HandleMissileLaunching(GameTime gameTime)
        {
            projectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(projectileTimer >= projectileCooldown)
            {
                ShootProjectile();
                projectileTimer = 0.0f;
            }
        }

        private void ShootProjectile()
        {
            float speed = 3.0f;
            Projectile projectile = new Projectile(projectileTexture,Position,-Vector2.UnitX*speed);
            projectiles.Add(projectile);
        }

        private void HandleProjectileShooting(GameTime gameTime)
        {
            missileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(missileTimer >= missileCooldown)
            {
                LaunchMissiles();
                missileTimer = 0.0f;
            }
        }

        private void LaunchMissiles()
        {
            Random random = new Random();
            for(int i = 0; i < 5; i++)
            {
                float speed = random.Next(3, 6);
                float angle = MathHelper.ToRadians(random.Next(-30,30));
                Vector2 direction = new Vector2((float)Math.Cos(angle),(float)Math.Sin(angle));

                Projectile missile = new Projectile(missibleTexture,Position,-direction*speed);
                missiles.Add(missile);
            }
        }
        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            foreach(Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        public void DrawMissiles(SpriteBatch spriteBatch)
        {
            foreach (Projectile missile in missiles)
            {
                missile.Draw(spriteBatch);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            DrawMissiles(spriteBatch);
            DrawProjectiles(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
