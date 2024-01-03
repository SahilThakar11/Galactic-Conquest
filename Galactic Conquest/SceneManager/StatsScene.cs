using Galactic_Conquest.OtherScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactic_Conquest.SceneManager
{
    public class StatsScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private int TotalStars;
        private SpriteFont myFont;
        private SpriteFont hiFont;
        private PlayScene _playScene;
        private List<TimeSpan> highScores;
        public StatsScene(Game game,PlayScene playScene ) : base(game)
        {
            Game1 game1 = game as Game1;
            this.spriteBatch = game1._spriteBatch;
            myFont = game1.Content.Load<SpriteFont>("Fonts/creditFont");
            hiFont = game1.Content.Load<SpriteFont>("Fonts/HealthFont");
            _playScene = playScene;

            highScores = new List<TimeSpan>();
        }
        public override void Update(GameTime gameTime)
        {
            _playScene.LoadPlayerData();

            highScores = _playScene.LoadHighScores();

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            int x = 100;
            int y = 100;
            spriteBatch.DrawString(hiFont,"High Scores",new Vector2(300,20),Color.Red);
            for(int i = 0; i< Math.Min(highScores.Count,5); i++)
            {
                string playerName = GetPlayerName(i);
                spriteBatch.DrawString(myFont,$"High Score {i + 1} : {highScores[i].ToString("hh\\:mm\\:ss\\.ff")} ",new Vector2(x,y),Color.OrangeRed);
                spriteBatch.DrawString(myFont, $"Player Name: {playerName}",new Vector2(x+250,y),Color.Green);
                y += 50;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private string GetPlayerName(int i)
        {
            if(_playScene._player != null && _playScene._player.ownedSkins != null && i < _playScene._player.ownedSkins.Count)
            {
                Skin playerSkin = _playScene._player.ownedSkins[i];
                if(playerSkin != null)
                {
                    return playerSkin.Name;
                }
            }
            return "Celestic Breeze";
        }
    }
}
