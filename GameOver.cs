using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BugTraveler
{
    internal class GameOver : IGameObject
    {
        const float BLOCK_DIV_X = 500;
        const float BLOCK_DIV_Y = 100;
        SFML.Graphics.Color REPLAY_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string REPLAY_VALUE = "REPLAY";
        SFML.Graphics.Color ADDSCORE_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string ADDSCORE_VALUE = "SAVE SCORE";
        SFML.Graphics.Color SCOREBOARD_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string SCOREBOARD_VALUE = "SCOREBOARD";
        SFML.Graphics.Color MENU_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string MENU_VALUE = "MENU";
        const string DISCRIPTION_STRING = "CONGRATULATION! YOU SURVIVED ";


        float REPLAY_BUTTON_POS_X;
        float REPLAY_BUTTON_POS_Y;
        float ADDSCORE_BUTTON_POS_X;
        float ADDSCORE_BUTTON_POS_Y;
        float SCOREBOARD_BUTTON_POS_X;
        float SCOREBOARD_BUTTON_POS_Y;
        float MENU_BUTTON_POS_X;
        float MENU_BUTTON_POS_Y;
        float DISCRIPTION_POS_X;
        float DISCRIPTION_POS_Y;


        SFML.Graphics.Text discriptionText;
        Font font;
        RectangleShape Discription;
        Button Replay;
        Button Scoreboard;
        Button Menu;
        Button AddScore;

        Game game;
        uint gameX, gameY;
        bool delay = false;

        public GameOver()
        {
            Discription = new RectangleShape(new SFML.System.Vector2f(BLOCK_DIV_X, BLOCK_DIV_Y));
            Discription.Origin = new SFML.System.Vector2f(BLOCK_DIV_X / 2, BLOCK_DIV_Y / 2);
            Discription.FillColor = Color.White;
        }
        public void Draw()
        {
            game.window.Draw(discriptionText);
            Replay.Draw();
            Scoreboard.Draw();
            Menu.Draw();
            AddScore.Draw();    
        }

        public void LoadContent(Game game, uint x, uint y)
        {
            this.game = game;
            this.gameX = x;
            this.gameY = y;
            REPLAY_BUTTON_POS_X = gameX / 2;
            SCOREBOARD_BUTTON_POS_X = gameX / 2;
            ADDSCORE_BUTTON_POS_X = gameX / 2;
            DISCRIPTION_POS_X = gameX / 2;
            MENU_BUTTON_POS_X = gameX / 2;
            REPLAY_BUTTON_POS_Y = gameY / 2 + 110;
            SCOREBOARD_BUTTON_POS_Y = gameY / 2;
            ADDSCORE_BUTTON_POS_Y = gameY / 2 - 110;
            DISCRIPTION_POS_Y = gameY / 2 - 370;
            MENU_BUTTON_POS_Y = gameY / 2 + 220;
            Discription.Position = new SFML.System.Vector2f(DISCRIPTION_POS_X, DISCRIPTION_POS_Y);

            Replay = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, REPLAY_BUTTON_POS_X, REPLAY_BUTTON_POS_Y, REPLAY_BUTTON_COLOR, game);
            Replay.LoadContent(REPLAY_VALUE, Variable.FONT_PATH);
            Menu = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, MENU_BUTTON_POS_X, MENU_BUTTON_POS_Y, MENU_BUTTON_COLOR, game);
            Menu.LoadContent(MENU_VALUE, Variable.FONT_PATH);
            Scoreboard = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, SCOREBOARD_BUTTON_POS_X, SCOREBOARD_BUTTON_POS_Y, SCOREBOARD_BUTTON_COLOR, game);
            Scoreboard.LoadContent(SCOREBOARD_VALUE, Variable.FONT_PATH);
            AddScore = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, ADDSCORE_BUTTON_POS_X, ADDSCORE_BUTTON_POS_Y, ADDSCORE_BUTTON_COLOR, game);
            AddScore.LoadContent(ADDSCORE_VALUE, Variable.FONT_PATH);

            font = new Font(Variable.FONT_PATH);
            discriptionText = new SFML.Graphics.Text(DISCRIPTION_STRING + game.score.ToString() + " SEC!", font);
            discriptionText.CharacterSize = (uint)(BLOCK_DIV_Y/2);
            discriptionText.FillColor = Color.White;
            discriptionText.Position = new SFML.System.Vector2f(DISCRIPTION_POS_X, DISCRIPTION_POS_Y);
            discriptionText.Origin = new SFML.System.Vector2f(discriptionText.GetLocalBounds().Width / 2, discriptionText.GetLocalBounds().Height / 2);
        }

        public void Update()
        {
            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                delay = true;
            }
            if (delay == true)
            {
                var MousePos = Mouse.GetPosition(game.window);
                if (Replay.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GoToGame();
                    }
                }
                if (Menu.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GoToLobby();
                    }
                }
                if (Scoreboard.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GotoScoreboard(this);
                    }
                }
                if (AddScore.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GotoAddScore();
                    }
                }
            }

        }
    }
}
