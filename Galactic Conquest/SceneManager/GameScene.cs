using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.SceneManager
{
    public abstract class GameScene : DrawableGameComponent
    {
        public List<GameComponent> GameComponents { get; set; }

        public virtual void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        public virtual void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        public GameScene(Game game): base(game) 
        { 
            GameComponents = new List<GameComponent>();
            Hide();
        }
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in GameComponents)
            {
                if(component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent component = null;
            foreach (GameComponent gameComponent in GameComponents)
            {
                component = (DrawableGameComponent)gameComponent;
                if (component.Visible)
                {
                    component.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

    }
}
