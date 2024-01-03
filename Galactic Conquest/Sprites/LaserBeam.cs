using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Galactic_Conquest.Sprites
{
    public class LaserBeam
    {
        private List<Texture2D> animationTextures;
        private int currentFrame;
        private float frameDuration;
        private float elapsedFrameTime;
        private Microsoft.Xna.Framework.Vector2 position;
        private float duration;
        private float elapsedDuration;
        private float damagePerSecond;
        private Microsoft.Xna.Framework.Rectangle boundingBox;

        public bool IsActive { get; private set; }
        public LaserBeam(List<Texture2D> animationTexture, Microsoft.Xna.Framework.Vector2 startPosition, float duration, float damagePerSecond = 3f)
        {
            this.animationTextures = animationTexture;
            this.currentFrame = 0;
            this.frameDuration = 0.1f;
            this.elapsedFrameTime = 0f;
            position = startPosition;
            this.duration = 2.5f;
            this.damagePerSecond = damagePerSecond;
            elapsedDuration = 0f;
            IsActive = true;

            boundingBox = new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, animationTexture[0].Width, animationTexture[0].Height);
        }

        public void Update(GameTime gameTime)
        {
            elapsedDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedFrameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedFrameTime >= frameDuration)
            {
                currentFrame = (currentFrame + 1) % animationTextures.Count;
                elapsedFrameTime = 0.0f;
            }
            if (elapsedDuration >= duration)
            {
                IsActive = false;
                return;
            }
            

            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationTextures[currentFrame], position, Microsoft.Xna.Framework.Color.OrangeRed);
        }
        public Microsoft.Xna.Framework.Rectangle GetBoundingBox()
        {
            return boundingBox;
        }
        public void InflictDamage(Player player)
        {
            //if(IsActive && player.)
        }
    }
}
