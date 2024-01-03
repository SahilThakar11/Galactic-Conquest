using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using System.Collections.Generic;

namespace Galactic_Conquest.SceneManager
{
    public class BossSelectionScene : GameScene
    {
        private List<string> bosses;
        private SpriteFont sgFont, sgSelectedFont,myFont;
        private int selectedBossIndex;
        public BossFightScene _bossFightScene;
        private Game game1;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        private Texture2D selectedIcon;
        private List<Texture2D> bossTextures;
        private PlayScene playScene;
        private Texture2D background;
        public BossSelectionScene(Game game, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,MainScene mainScene,PlayScene playScene) : base(game)
        {
            bosses = new List<string>()
            {
                "Flash","Vortex","Crimson","FrostBite"
            };
            selectedBossIndex = 0;
            game1 = game as Game1;
            sgFont = game.Content.Load<SpriteFont>("Fonts/SGFont");
            myFont = game.Content.Load<SpriteFont>("Fonts/creditFont");
            sgSelectedFont = game.Content.Load<SpriteFont>("Fonts/SGSelectedFont");
            selectedIcon = game.Content.Load<Texture2D>("UI/vs");
            _spriteBatch = spriteBatch;
            this.mainScene = mainScene;
            this.playScene = playScene;
            BtnSFX = game.Content.Load<SoundEffect>("SFX/btnSFX");
            bossTextures = new List<Texture2D>()
            {
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_flash"),
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_cyclic"),
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_track"),
                game.Content.Load<Texture2D>("Assests/Enemy/enemy_rage"),
            };
            background = game.Content.Load<Texture2D>("Assests/Background/Background_MainMenu");
        }
        private KeyboardState os;
        private MainScene mainScene;

        public SoundEffect BtnSFX { get; }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up) && os.IsKeyUp(Keys.Up))
            {
                BtnSFX.Play();
                selectedBossIndex--;
                if (selectedBossIndex == -1)
                {
                    selectedBossIndex = bosses.Count - 1;
                }
            }
            else if (ks.IsKeyDown(Keys.Down) && os.IsKeyUp(Keys.Down))
            {
                BtnSFX.Play();
                selectedBossIndex++;
                if (selectedBossIndex == bosses.Count)
                {
                    selectedBossIndex = 0;
                }
            }
            os = ks;
            if(ks.IsKeyDown(Keys.Space))
            {
                _bossFightScene = new BossFightScene(game1,_spriteBatch,GetBossNameByIndex(selectedBossIndex),mainScene,playScene);
                game1.Components.Add(_bossFightScene);
                _bossFightScene.Show();
                this.Hide();
            }
            
            base.Update(gameTime);

        }
        private string GetBossNameByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return bosses[0];
                case 1:
                    return bosses[1];
                case 2:
                    return bosses[2];
                case 3:
                    return bosses[3];
                    default:
                    return bosses[0];
            }
        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempState = new Vector2(50,10);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.MediumPurple);
            for (int i = 0; i < bosses.Count; i++)
            {
                if(selectedBossIndex == i)
                {
                    _spriteBatch.DrawString(sgSelectedFont, bosses[i], new Vector2(tempState.X + 30, tempState.Y), Color.Red);
                    _spriteBatch.Draw(selectedIcon,new Vector2(tempState.X - 60, tempState.Y -5), Color.White);
                    tempState.Y += sgSelectedFont.LineSpacing;
                    _spriteBatch.Draw(bossTextures[i],new Vector2(250,0),null, Color.White,0f,Vector2.Zero,3f,SpriteEffects.None,0f);
                }
                else
                {
                    _spriteBatch.DrawString(sgFont, bosses[i], tempState, Color.White);
                    tempState.Y += sgFont.LineSpacing;
                }
            }
            
            _spriteBatch.DrawString(myFont, "Press space to fight vs selected boss",new Vector2(230,400),Color.YellowGreen);
            _spriteBatch.End();
            base.Draw(gameTime);

        }

    }
}
