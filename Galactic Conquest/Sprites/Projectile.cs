using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class Projectile
    {
        public Texture2D Texture {  get; set; }
        public Microsoft.Xna.Framework.Vector2 Position { get; set; }
        public Microsoft.Xna.Framework.Vector2 Velocity { get; set; }
        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y,Texture.Width, Texture.Height);
        public bool IsOwned { get; set; }
        public Projectile(Texture2D texture, Microsoft.Xna.Framework.Vector2 position, Microsoft.Xna.Framework.Vector2 velocity)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;

        }
        public void Update(GameTime gameTime)
        {
            Position += Velocity;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Microsoft.Xna.Framework.Color.White);
        }
        public bool IsWithinBounds(Rectangle bounds)
        {
            return bounds.Contains(new Point((int)Position.X,(int)Position.Y));
        }

    }
}
