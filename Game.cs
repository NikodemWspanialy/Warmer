using SFML.Graphics;
using SFML.System;
using System.Collections.Concurrent;

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
        //Time and Score
        public Clock TimeInGame { get; set; }
        public float score { get; set; }
        
        //gameStatus
        private GameStatus goBackAfterScoreboard;
        private IGameObject CorrctScene;
        //music and thread
        SoundManager soundManager;

        public Game() : base(WINDOW_WIDTH, WINDOW_HEIGHT, WINDOW_TITLE, new SFML.Graphics.Color(211,130,115)) {
            icon = new Image(WINDOW_ICON);
            window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
        }
        //
        public override void Initialize()
        {
            CorrctScene = new Lobby();
            LoadContent();
        }
        public override void LoadContent()
        {
            CorrctScene.LoadContent(this, WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        public override void Update(GameTime gameTime)
        {
            CorrctScene.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            CorrctScene.Draw();
        }
        public void GoToGameOver(Cockroach status)
        {
            windowColor = END_GAME_COLOR;
            score = TimeInGame.ElapsedTime.AsSeconds();
            Console.WriteLine("Touch at time: " + score.ToString());
            CorrctScene = new GameOver();
            LoadContent();
            ThreadManager.queue.Enqueue(GameStatus.GameOver);
            ThreadManager.sygnal.Set();
        }
        public void GoToGameOver()
        {
            CorrctScene = new GameOver();
            LoadContent();
        }
        public void GoToGame() 
        {
            windowColor = GAME_BACKGROUND_COLOR;
            CorrctScene = new GamePlayMenager();
            TimeInGame = new Clock();
            ThreadManager.queue.Enqueue(GameStatus.InGame);
            ThreadManager.sygnal.Set();
            LoadContent();
        }
        public void GoToLobby() 
        {

            windowColor = GAME_BACKGROUND_COLOR;
            CorrctScene = new Lobby();
            LoadContent();
        }
        public void GotoAddScore() 
        {
            AddScore addScore = new AddScore();
            addScore.addScoreMethod(score);
            CorrctScene = new GameOver();
            LoadContent();
        }
        public void GotoScoreboard(GameOver status) {
            goBackAfterScoreboard = GameStatus.GameOver;
            CorrctScene = new Scoreboard();
            LoadContent();
        }
        public void GotoScoreboard(Lobby status)
        {
            goBackAfterScoreboard = GameStatus.Lobby;
            CorrctScene = new Scoreboard();
            LoadContent();
        }
        public void GotoAboutMe() {
            CorrctScene = new About();
            LoadContent();
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
