using Galactic_Conquest.OtherScripts;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Galactic_Conquest.SceneManager
{
    public class MainScene : GameScene
    {
        
        public MenuComponents Menu {  get; set; }
        private SpriteBatch spriteBatch;
        public Texture2D menuBackground;
        private KeyboardState os;
        private Texture2D logoTexture;
        string[] menuItems = { 
         "Play","Boss Fight","Shop","Help","Stats","Credits","Quit"
        };

        public Song lobySong;
        private bool musicON = false;

        public MainScene(Game game) : base(game) 
        {
            Game1 game1 =(Game1)game;
            menuBackground = game.Content.Load<Texture2D>("Assests/Background/Back_Main");
            this.spriteBatch = game1._spriteBatch;
            SpriteFont sgFont = game1.Content.Load<SpriteFont>("Fonts/SGFont");
            SpriteFont sgSelectedFont = game1.Content.Load<SpriteFont>("Fonts/SGSelectedFont");

            Menu = new MenuComponents(game,spriteBatch,sgFont,sgSelectedFont,menuItems);
            this.GameComponents.Add(Menu);
       
            logoTexture = game.Content.Load<Texture2D>("UI/Logo");
            lobySong = game.Content.Load<Song>("Music/MenuMusic");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            if(ks.IsKeyDown(Keys.M) && os.IsKeyUp(Keys.M))
            {
               MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            }
            os = ks;
            base.Update(gameTime);

        }
        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            spriteBatch.Draw(menuBackground,new Rectangle(0,0,GraphicsDevice.Viewport.Width,GraphicsDevice.Viewport.Height),Color.MediumPurple);
            spriteBatch.Draw(logoTexture,new Vector2(300,-20),Color.AliceBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Show()
        {
            
                MediaPlayer.Play(lobySong);
                base.Show();
        }
        public override void Hide()
        {
            base.Hide();
        }

    }
    
}
