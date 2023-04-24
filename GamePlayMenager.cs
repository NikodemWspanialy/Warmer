using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace BugTraveler
{
    internal class GamePlayMenager : IGameObject
    {
        const int TIMER_POSITION_X = 20;
        const int TIMER_POSITION_Y = 20;
        public float time { get; set; }
        Game game;
        uint gameX, gameY;

        Font font;
        Text actualTime;
        SoundManager soundManager;

        public Player player { get; set; }
        public CockroachMenager cockroachMenager { get; set; }
        public GamePlayMenager()
        {
            player = new Player();
            cockroachMenager = new CockroachMenager(player);
            soundManager = new SoundManager();
            soundManager.PlayGameMusic();
        }
        public void Draw()
        {
            player.Draw();
            cockroachMenager.Draw();
            
            game.window.Draw(actualTime);
        }

        public void LoadContent(Game game, uint x, uint y)
        {
            this.game = game;
            gameX = x; gameY = y;
            player.LoadContent(game, x, y);
            cockroachMenager.LoadContent(game, x, y);
            time = 0;
            font = new Font(Variable.FONT_PATH);
            actualTime = new Text(time.ToString(), font,40)
            {
                Position = new Vector2f(TIMER_POSITION_X, TIMER_POSITION_Y),
                FillColor = Color.Black
            };
        }

        public void Update()
        {
            time = game.TimeInGame.ElapsedTime.AsSeconds();
            time = (float)Math.Round(time, 2);
            actualTime.DisplayedString = time.ToString();
            player.Update();
            cockroachMenager.Update();
        }
    }
}
