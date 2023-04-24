using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Drawing;
using BugTraveler;

namespace gameLoop
{
    abstract public class GameLoop
    {
        const float MAX_FPS = 60f;
        float deltaTime = 1f / MAX_FPS;
        float correctTime = 0f, previousTime = 0f, timeSinceLastFrame = 0f;

        public GameTime gameTime { get; set; }
        public RenderWindow window { get; }
        public SFML.Graphics.Color windowColor { get; set; }


        public GameLoop(uint WINDOW_WIDTH, uint WINDOW_HEIGHT, string title, SFML.Graphics.Color windowColor)
        {
            this.gameTime = new GameTime();
            this.windowColor = windowColor;
            window = new RenderWindow(new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), title);
            window.Closed += WindowClose;
        }

        private void WindowClose(object? sender, EventArgs e)
        {
            window.Close();
        }

        public void Run()
        {
            Initialize();
            LoadContent();
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                correctTime = clock.ElapsedTime.AsSeconds();
                timeSinceLastFrame = correctTime - previousTime;
                if (timeSinceLastFrame >= deltaTime)
                {
                    gameTime.Update(timeSinceLastFrame, clock.ElapsedTime.AsSeconds());
                    timeSinceLastFrame = 0f;
                    previousTime = correctTime;

                    Update(gameTime);

                    window.Clear(windowColor);
                    Draw(gameTime);
                    window.Display();
                }
            }
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Initialize();
        public abstract void LoadContent();

    }
}
