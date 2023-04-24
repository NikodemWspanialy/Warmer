using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    internal class Game : gameLoop.GameLoop
    {
        // zmienne
        const uint WINDOW_WIDTH = 1400;
        const uint WINDOW_HEIGHT = 1000;
        const string WINDOW_TITLE = "WARMER";
        const string WINDOW_ICON = @"C:\PRJs\CS_PRJs\PRJs\BugTraveler\IMG\Cockroach.png";
        SFML.Graphics.Color GAME_BACKGROUND_COLOR = new SFML.Graphics.Color(211, 130, 115);
        SFML.Graphics.Color END_GAME_COLOR = new SFML.Graphics.Color(100,100,100);
        Image icon;
        public Clock TimeInGame;
        public float score;

        private GameStatus gameStatus;
        private GameStatus goBackAfterScoreboard;
        public GamePlayMenager gamePlayMenager { get; set; }
        public Lobby Lobby { get; set; }
        public GameOver gameOver { get; set; } 
        public About about { get; set; }
        public Scoreboard scoreboard { get; set; }
        public AddScore addScore { get; set; }
        //
        public Game() : base(WINDOW_WIDTH, WINDOW_HEIGHT, WINDOW_TITLE, new SFML.Graphics.Color(211,130,115)) {
            gameStatus = GameStatus.Lobby;
            icon = new Image(WINDOW_ICON);
            window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
        }
        //
        public override void Initialize()
        {
            Lobby = new Lobby();
        }

        public override void LoadContent()
        {
            Lobby.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        public override void Update(GameTime gameTime)
        {
            if (gameStatus == GameStatus.InGame)
            {
                gamePlayMenager.Update();
            }
            else if(gameStatus == GameStatus.GameOver)
            {
                gameOver.Update();

            }
            else if(gameStatus == GameStatus.Lobby)
            {
                Lobby.Update();
            }
            else if(gameStatus == GameStatus.Scoreboard) 
            {
                scoreboard.Update();
            }
            else if(gameStatus == GameStatus.AddScore) 
            { }
            else if(gameStatus == GameStatus.About) 
            {
                about.Update();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (gameStatus == GameStatus.Lobby)
            {
                Lobby.Draw();
            }
            else if(gameStatus == GameStatus.InGame)
            {
                gamePlayMenager.Draw();
            }
            else if(gameStatus == GameStatus.GameOver) // gameStatus == GameStatus.GameOver
            {
                gamePlayMenager.Draw();
                gameOver.Draw();
            }
            else if (gameStatus == GameStatus.Scoreboard) 
            {
                scoreboard.Draw();
            }
            else if (gameStatus == GameStatus.AddScore) { }
            else if (gameStatus == GameStatus.About) 
            {
                about.Draw();
            }
        }
        public void GoToGameOver(Cockroach status)
        {
            windowColor = END_GAME_COLOR;
            score = gamePlayMenager.time;
            Console.WriteLine("Touch at time: " + score.ToString());
            gameStatus = GameStatus.GameOver;
            gameOver = new GameOver();
            gameOver.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GoToGameOver()
        {
            gameStatus = GameStatus.GameOver;
            gameOver = new GameOver();
            gameOver.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GoToGame() 
        {
            windowColor = GAME_BACKGROUND_COLOR;
            gameStatus = GameStatus.InGame;
            gamePlayMenager = new GamePlayMenager();
            gamePlayMenager.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
            TimeInGame = new Clock();
        }
        public void GoToLobby() 
        {

            windowColor = GAME_BACKGROUND_COLOR;
            gameStatus = GameStatus.Lobby;
            Lobby = new Lobby();
            Lobby.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GotoAddScore() 
        {
            addScore = new AddScore();
            addScore.addScoreMethod(score);
            gameOver = new GameOver();
            gameOver.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GotoScoreboard(GameOver status) {
            goBackAfterScoreboard = GameStatus.GameOver;
            gameStatus = GameStatus.Scoreboard;
            scoreboard = new Scoreboard();  
            scoreboard.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GotoScoreboard(Lobby status)
        {
            goBackAfterScoreboard = GameStatus.Lobby;
            gameStatus = GameStatus.Scoreboard;
            scoreboard = new Scoreboard();
            scoreboard.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GotoAboutMe() {
            gameStatus = GameStatus.About;
            about = new About();
            about.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
        public void GoFromScoreboard() 
        {
            if (goBackAfterScoreboard == GameStatus.GameOver)
                GoToGameOver();
            else
                GoToLobby();
        }
    }
}
