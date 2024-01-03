using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class FlashBoss : Boss
    {
        private float movementSpeed = 0.6f;
        private float teleportCoolDown = 2.5f;
        private float teleportTimer = 0.0f;
        private int damage = 3;

        private float burstCooldown = 2.0f;
        private float burstTimer = 0.0f;
        
        private SpriteBatch spriteBatch;
        public List<Projectile> projectiles {  get; private set; }
        private Texture2D projectileTexture;
        private Random random = new Random();
        public FlashBoss(string TexturePath, Game game,SpriteBatch spriteBatch) : base(TexturePath, game,spriteBatch)
        {
            projectiles = new List<Projectile>();
            projectileTexture = game.Content.Load<Texture2D>("Assests/Projectiles/f_3");
            this.spriteBatch = spriteBatch;
            this.Health = 100;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HandleFlashMovements();
            HandleBurstShooting(gameTime);
            HandleTeleportation(gameTime);
            
            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            projectiles.RemoveAll(p => !p.IsWithinBounds(Game.GraphicsDevice.Viewport.Bounds));

        }

        private void HandleBurstShooting(GameTime gameTime)
        {
            burstTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if( burstTimer >= burstCooldown )
            {
                ShotBurst();

                burstTimer = 0.0f;
            }

        }

        private void ShotBurst()
        {
            for(int i = 0; i< 3; i++)
            {   
                float speed = 3.0f;
                Vector2 variance = new Vector2(0, i * 30);
                Projectile projectile = new Projectile(projectileTexture, Position + variance, -Vector2.UnitX * speed);
                
                projectiles.Add(projectile);
            }
        }

        private void HandleFlashMovements()
        {
            Position += new Vector2(-movementSpeed, 0);
        }

        private void HandleTeleportation(GameTime gameTime)
        {
            teleportTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(teleportTimer >= teleportCoolDown)
            {
                Position = RandomTelePortPosition();
                teleportTimer = 0.0f;
            }
        }

        private Vector2 RandomTelePortPosition()
        {
            float x = random.Next(400, 500);
            float y = random.Next(200, 300);

            return new Vector2(x, y);
        }

        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            foreach(Projectile projectile in projectiles)
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
