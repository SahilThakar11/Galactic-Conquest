using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Galactic_Conquest.SceneManager
{
    public class MenuComponents : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont sgFont,sgSelectedFont;
        private List<string> menuItems;
        private Vector2 position;
        public int selectedIndex;
        private Color sgColor = Color.White;
        private Color sgSelectedColor = Color.Cyan;
        private KeyboardState oldState;
        private bool isFontVisible = true;
        private float blinkTimer = 0f;
        private float blinkInterval = 0.55f;
        private Texture2D selectedIcon;
        private SoundEffect BtnSFX;
        public MenuComponents(Game game,SpriteBatch spriteBatch,SpriteFont sgFont,SpriteFont sgSelectedFont, string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.sgFont = sgFont;
            this.sgSelectedFont = sgSelectedFont;
            menuItems = new List<string>(menus);
            position = new Vector2(40,0);
            selectedIcon = game.Content.Load<Texture2D>("UI/Selection");
            BtnSFX = game.Content.Load<SoundEffect>("SFX/btnSFX");
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            if(currentState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                BtnSFX.Play();
                if(selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if(currentState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                BtnSFX.Play();
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            blinkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(blinkTimer >= blinkInterval)
            {
                isFontVisible = !isFontVisible;
                blinkTimer = 0f;
            }

            oldState = currentState;
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempState = position;
            spriteBatch.Begin();
            for(int i = 0; i < menuItems.Count; i++)
            {
                if(selectedIndex == i)
                {
                    if(isFontVisible)
                    {
                        spriteBatch.DrawString(sgSelectedFont, menuItems[i], new(tempState.X + 30, tempState.Y), sgSelectedColor);
                        spriteBatch.Draw(selectedIcon, new Vector2(tempState.X - 40, tempState.Y + 20), Color.White);
                    }
                    tempState.Y += sgSelectedFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(sgFont, menuItems[i], tempState, sgColor);
                    tempState.Y += sgFont.LineSpacing;
                }
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
