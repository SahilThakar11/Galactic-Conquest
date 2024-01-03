using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class Background
    {
        private List<CelestialBody> celestialBodies = new List<CelestialBody>();
        private Random random = new Random();
        private float BackgroundSpeed = 1.0f;
        private GraphicsDevice graphicsDevice;
        private List<Texture2D> StarTextures;
        private List<Texture2D> PlanetTextures;
        private List<Texture2D> MoonTexture;
        private List<Texture2D> BlackholeTexture;
        private List<Texture2D> NebulaTexture;
        private List<Texture2D> SmlNebulaTexture;

        private float elapsedTimePlanet = 3.5f;
        private float spawnIntervalPlanet = 4.5f;

        private float elapsedTimeStar = 3.0f;
        private float spawnIntervalStar = 0.25f;

        private float elapsedTimeblackhole = 10.0f;
        private float spawnTimeblackhole = 20.0f;

        private float elapsedTimeNebula = 7.0f;
        private float spawnTimeNebula = 7.0f;

        private float elapsedTimeMoon = 2.0f;
        private float spawnTimeMoon = 4.0f;



        public Background(GraphicsDevice gd, List<Texture2D> planetTextures, List<Texture2D> starTextures, List<Texture2D> moonTextures, List<Texture2D> blackholeTextures, List<Texture2D> nebulaTextures, List<Texture2D> smlNebulaTexture)
        {
            graphicsDevice = gd;
            GenerateCelestialBody(planetTextures, moonTextures, blackholeTextures, nebulaTextures, smlNebulaTexture, starTextures);
            StarTextures = starTextures;
            PlanetTextures = planetTextures;
            MoonTexture = moonTextures;
            BlackholeTexture = blackholeTextures;
            NebulaTexture = nebulaTextures;
            SmlNebulaTexture = smlNebulaTexture;

        }

        private void GenerateCelestialBody(List<Texture2D> planetTexture, List<Texture2D> moonTexture, List<Texture2D> blackholeTexture, List<Texture2D> nebulaTexture, List<Texture2D> smlNebulaTexture, List<Texture2D> starTexture)
        {
           
            for (int i = 0;i <30;i++)
            {
                System.Numerics.Vector2 newPosition = new System.Numerics.Vector2(random.Next(0, graphicsDevice.Viewport.Width), random.Next(0, graphicsDevice.Viewport.Height));
                Texture2D randomStarTexture = GetRandomeTexture(starTexture);
                if(randomStarTexture != null)
                {
                    celestialBodies.Add(new CelestialBody(GetRandomeTexture(starTexture), newPosition));
                }
            }
            for(int i = 0; i < 1;i++)
            {
                System.Numerics.Vector2 Position = new System.Numerics.Vector2(random.Next(0, graphicsDevice.Viewport.Width), random.Next(0, graphicsDevice.Viewport.Height));
                Texture2D blackhole = blackholeTexture[0];     
                celestialBodies.Add(new CelestialBody(blackhole, Position));
                
            }
            for(int i = 0; i <1 ;i++)
            {
                System.Numerics.Vector2 Position = new System.Numerics.Vector2(random.Next(0, graphicsDevice.Viewport.Width), random.Next(0, graphicsDevice.Viewport.Height));
                Texture2D nebula = nebulaTexture[0];
                celestialBodies.Add(new CelestialBody(nebula, Position));
            }
            for (int i = 0;i < 1;i++)
            {
                System.Numerics.Vector2 Position = new System.Numerics.Vector2(random.Next(0, graphicsDevice.Viewport.Width), random.Next(0, graphicsDevice.Viewport.Height));
                Texture2D planet = GetRandomeTexture(planetTexture);
                if (planet != null)
                {
                    celestialBodies.Add(new CelestialBody(planet, Position));
                }
            }
            for(int i =0;i < 3;i++)
            {
                System.Numerics.Vector2 Position = new System.Numerics.Vector2(random.Next(0, graphicsDevice.Viewport.Width), random.Next(0, graphicsDevice.Viewport.Height));
                Texture2D moon = GetRandomeTexture(moonTexture);
                if (moon != null)
                {
                    celestialBodies.Add(new CelestialBody(moon, Position));
                }
            }
            
        }
        private Texture2D GetRandomeTexture(List<Texture2D> textures)
        {
            int index = random.Next(textures.Count);
            return textures[index];
        }
        public void Update(GameTime gameTime)
        {

            foreach (var celestialBody in celestialBodies.ToList())
            {
                celestialBody.Position += new System.Numerics.Vector2(BackgroundSpeed,0);
                if(celestialBody.Position.X > graphicsDevice.Viewport.Width)
                {
                    celestialBodies.Remove(celestialBody);
                }
            }
            elapsedTimePlanet += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTimeStar += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTimeNebula += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTimeblackhole += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTimeMoon += (float)gameTime.ElapsedGameTime.TotalSeconds;
            GenerateNewCelestialBody();
        }

        private void GenerateNewCelestialBody()
        {
            Vector2 newPosition = new Vector2(-100, random.Next(0, graphicsDevice.Viewport.Height));
            if (elapsedTimePlanet >= spawnIntervalPlanet)
            {
                newPosition.Y = random.Next(0,graphicsDevice.Viewport.Height-30); 
                if(!IsTooCloseToOtherBodies(newPosition))
                {
                    celestialBodies.Add(new CelestialBody(GetRandomeTexture(PlanetTextures), newPosition));
                    elapsedTimePlanet = 0;
                }
            }
            if(elapsedTimeStar >= spawnIntervalStar)
            {
                newPosition.Y = random.Next(0, graphicsDevice.Viewport.Height);
                if(!IsTooCloseToOtherBodies(newPosition))
                {
                    celestialBodies.Add(new CelestialBody(GetRandomeTexture(StarTextures), newPosition));
                    elapsedTimeStar = 0;
                }
            }
            if (elapsedTimeblackhole >= spawnTimeblackhole)
            {
                newPosition.Y = random.Next(0, graphicsDevice.Viewport.Height);
                if (!IsTooCloseToOtherBodies(newPosition))
                {
                    celestialBodies.Add(new CelestialBody(GetRandomeTexture(BlackholeTexture), newPosition));
                    elapsedTimeblackhole = 0;
                }
            }
            if (elapsedTimeNebula >= spawnTimeNebula)
            {
                newPosition.Y = random.Next(0, graphicsDevice.Viewport.Height);
                if (!IsTooCloseToOtherBodies(newPosition))
                {
                    celestialBodies.Add(new CelestialBody(GetRandomeTexture(NebulaTexture), newPosition));
                    elapsedTimeNebula = 0;
                }
            }
            if (elapsedTimeMoon >= spawnTimeMoon)
            {
                newPosition.Y = random.Next(0, graphicsDevice.Viewport.Height);
                if (!IsTooCloseToOtherBodies(newPosition))
                {
                    celestialBodies.Add(new CelestialBody(GetRandomeTexture(MoonTexture), newPosition));
                    elapsedTimeMoon = 0;
                }
            }
            
        }
        private bool IsTooCloseToOtherBodies(Vector2 position)
        {
            float minDistace = 50.0f;
            foreach(var body in celestialBodies)
            {
                float distance = Vector2.Distance(position, body.Position);
                if(distance < minDistace)
                {
                    return true;
                }
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var body in celestialBodies)
            {
                body.Draw(spriteBatch);
            }
        }
        public void Reset()
        {
            celestialBodies.Clear();
            GenerateCelestialBody(PlanetTextures, MoonTexture, BlackholeTexture, NebulaTexture, SmlNebulaTexture, StarTextures);

        }
    }
}
