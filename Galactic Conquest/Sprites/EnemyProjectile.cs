using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class EnemyProjectile
    {
        public Texture2D Texture { get; set; }
        public System.Numerics.Vector2 Position { get; set; }
        public System.Numerics.Vector2 Velocity { get; set; }
        public Rectangle Bounds => new Rectangle((int)Position.X,(int)Position.Y,Texture.Width,Texture.Height);
        public EnemyProjectile(Texture2D texture, System.Numerics.Vector2 position, System.Numerics.Vector2 velocity)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
        }
        public void Update()
        {
            Position += Velocity;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
