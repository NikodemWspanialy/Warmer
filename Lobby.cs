using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    internal class Lobby : IGameObject
    {
        const float BLOCK_DIV_X = 500;
        const float BLOCK_DIV_Y = 100;
        SFML.Graphics.Color PLAY_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string PLAY_VALUE = "PLAY";
        SFML.Graphics.Color SCOREBOARD_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string SCOREBOARD_VALUE = "SCOREBOARD";
        SFML.Graphics.Color ABOUT_BUTTON_COLOR = SFML.Graphics.Color.White;
        const string ABOUT_VALUE = "ABOUT GAME";
        const string TITLE_STRING = "WARMER";


        float PLAY_BUTTON_POS_X;
        float PLAY_BUTTON_POS_Y;
        float SCOREBOARD_BUTTON_POS_X;
        float SCOREBOARD_BUTTON_POS_Y;
        float ABOUT_BUTTON_POS_X;
        float ABOUT_BUTTON_POS_Y;
        float DISCRIPTION_POS_X;
        float DISCRIPTION_POS_Y;


        SFML.Graphics.Text title;
        Font font;
        RectangleShape Discription;
        Button Play;
        Button Scoreboard;
        Button AboutGame;
        bool delay = false;

        Game game;
        uint gameX, gameY;

        public Lobby()
        {
            Discription = new RectangleShape(new SFML.System.Vector2f(BLOCK_DIV_X, BLOCK_DIV_Y));
            Discription.Origin = new SFML.System.Vector2f(BLOCK_DIV_X / 2, BLOCK_DIV_Y / 2);
            Discription.FillColor = Color.White;
        }
        public void Draw()
        {
            game.window.Draw(title);
            Play.Draw();
            Scoreboard.Draw();
            AboutGame.Draw();
        }

        public void LoadContent(Game game, uint x, uint y)
        {
            this.game = game;
            this.gameX = x;
            this.gameY = y;
            PLAY_BUTTON_POS_X = gameX / 2;
            SCOREBOARD_BUTTON_POS_X = gameX / 2;
            DISCRIPTION_POS_X = gameX / 2;
            ABOUT_BUTTON_POS_X = gameX / 2;
            PLAY_BUTTON_POS_Y = gameY / 2;
            SCOREBOARD_BUTTON_POS_Y = gameY / 2 + 110;
            ABOUT_BUTTON_POS_Y = gameY / 2 + 220;
            DISCRIPTION_POS_Y = gameY / 2 - 350;
            Discription.Position = new SFML.System.Vector2f(DISCRIPTION_POS_X, DISCRIPTION_POS_Y);

            Play = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, PLAY_BUTTON_POS_X, PLAY_BUTTON_POS_Y, PLAY_BUTTON_COLOR,game);
            Play.LoadContent(PLAY_VALUE,Variable.FONT_PATH);
            Scoreboard = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, SCOREBOARD_BUTTON_POS_X, SCOREBOARD_BUTTON_POS_Y, SCOREBOARD_BUTTON_COLOR,game);
            Scoreboard.LoadContent(SCOREBOARD_VALUE, Variable.FONT_PATH);
            AboutGame = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, ABOUT_BUTTON_POS_X, ABOUT_BUTTON_POS_Y, ABOUT_BUTTON_COLOR, game);
            AboutGame.LoadContent(ABOUT_VALUE, Variable.FONT_PATH);

            font = new Font(Variable.FONT_PATH);
            title = new SFML.Graphics.Text(TITLE_STRING , font);
            title.CharacterSize = (uint)(BLOCK_DIV_Y);
            title.FillColor = Color.White;
            title.Position = new SFML.System.Vector2f(DISCRIPTION_POS_X, DISCRIPTION_POS_Y);
            title.Origin = new SFML.System.Vector2f(title.GetLocalBounds().Width / 2, title.GetLocalBounds().Height / 2);
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
                if (Play.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GoToGame();
                    }
                }

                if (Scoreboard.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GotoScoreboard(this);
                    }
                }
                if (AboutGame.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GotoAboutMe();
                    }
                }
            }
        }
    }
}
