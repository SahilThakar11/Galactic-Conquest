using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.Sprites
{
    public class Boss : DrawableGameComponent
    {
        public Texture2D texture;
        private Vector2 position;
        private float rotation;
        private int health;
        private bool isSelected;
        private bool isDefeated;
        private SpriteBatch spriteBatch;
        public Vector2 Origin
        {
            get { return new Vector2(texture.Width / 2, texture.Height / 2); }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public bool IsDefeated
        {
            get { return isDefeated; }
        }
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X - texture.Width / 2,(int)Position.Y - texture.Height /2, texture.Width, texture.Height);
            }
        }
        public int Damage { get; set; }
        private List<Texture2D> healthBarTextures;
        private Game game1;
        private Texture2D currentHealthBar;

        public Boss(string TexturePath,Game game,SpriteBatch spriteBatch) : base(game)
        {
            texture = game.Content.Load<Texture2D>(TexturePath);

            position = Vector2.Zero;
            health = 100;
            LoadHealthBar(game);
            isDefeated = false;
            this.spriteBatch = spriteBatch;
            game1 = game;
        }

        private void LoadHealthBar(Game game)
        {
            string healthBarImgName = GetCurrentHealthBarImageName();
            currentHealthBar = game.Content.Load<Texture2D>($"UI/{healthBarImgName}");
        }

        private string GetCurrentHealthBarImageName()
        {
            switch(health)
            {
                case 100:
                    return "enm_bar1";
                case >90:
                    return "enm_bar2";
                case >80:
                    return "enm_bar3";
                case >70:
                    return "enm_bar4";
                case >60:
                    return "enm_bar5";
                case >50:
                    return "enm_bar6";
                case >40:
                    return "enm_bar7";
                case >30:
                    return "enm_bar8";
                case >20:
                    return "enm_bar9";
                case >10:
                    return "enm_bar10";
                case 00:
                    return "enm_bar11";
                default:
                    return "enm_bar11";
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if(health <= 0)
            {
                isDefeated = true;
            }
        }
        public override void Update(GameTime gameTime)
        {
            LoadHealthBar(game1);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Draw(texture, position,null, Color.White,Rotation,Origin,1f,SpriteEffects.None,0);
            spriteBatch.Draw(currentHealthBar, new Vector2(600,0), Color.White);
            base.Draw(gameTime);
        }
    }
}
