using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Galactic_Conquest.GeneralScripts
{
    public class Star
    {
        public Vector2 Position { get; set; }
        private List<Texture2D> frames;
        private int currentFrame;
        private float frameSpeed;
        private float frameTimer;
        private bool isLooping;

        public Star(List<Texture2D> frames, float frameSpeed, bool isLooping)
        {
            this.frames = frames;
           
            this.frameSpeed = frameSpeed;
            this.isLooping = isLooping;
            this.currentFrame = 0;
            this.frameTimer = 0f;
        }
        public void Update(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(frameTimer >= frameSpeed)
            {
                currentFrame++;
                if(currentFrame ==  frames.Count) 
                {
                    currentFrame = isLooping ? 0 : frames.Count - 1;
                }
                frameTimer = 0f;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(frames[currentFrame],Position,Color.White);
        }
        public List<Texture2D> InitializeStar(Game game)
        {
            List<Texture2D> starFrames = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("UI/Star1"),
                game.Content.Load<Texture2D>("UI/Star2"),
                game.Content.Load<Texture2D>("UI/Star3"),
                game.Content.Load<Texture2D>("UI/Star4"),
                game.Content.Load<Texture2D>("UI/Star5"),
                game.Content.Load<Texture2D>("UI/Star6"),
                game.Content.Load<Texture2D>("UI/Star7"),

            };
            return starFrames;

        }
    }
}
