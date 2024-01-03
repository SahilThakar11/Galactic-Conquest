using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;


namespace Galactic_Conquest.Sprites
{
    public class CelestialBody
    {
        

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public CelestialBody(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Microsoft.Xna.Framework.Color.White);
        }
    }
}