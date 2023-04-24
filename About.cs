using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BugTraveler
{
    internal class About : IGameObject
    {
        const float BLOCK_DIV_X = 500;
        const float BLOCK_DIV_Y = 100;
        const string DISCRIPTION_FILE = @"C:\PRJs\CS_PRJs\PRJs\BugTraveler\FILES\discription.txt";
        const string RETURN_VALUE = "RETURN";
        SFML.Graphics.Color RETURN_BUTTON_COLOR = SFML.Graphics.Color.White;

        float RETURN_BUTTON_POS_X;
        float RETURN_BUTTON_POS_Y;
        float DISCRIPTION_POS_X;
        float DISCRIPTION_POS_Y;

        SFML.Graphics.Text discription;
        Font font;
        RectangleShape Discription;
        Button Return;
        bool delay = false;

        Game game;
        uint gameX, gameY;

        public void Draw()
        {
            game.window.Draw(discription);
            Return.Draw();
        }

        public void LoadContent(Game game, uint x, uint y)
        {
            Discription = new RectangleShape(new SFML.System.Vector2f(BLOCK_DIV_X, BLOCK_DIV_Y));
            Discription.Origin = new SFML.System.Vector2f(BLOCK_DIV_X / 2, BLOCK_DIV_Y / 2);

            this.game = game;
            this.gameX = x;
            this.gameY = y;
            RETURN_BUTTON_POS_X = gameX / 2;
            RETURN_BUTTON_POS_Y = gameY / 2 + 220;
            Return = new Button(BLOCK_DIV_X, BLOCK_DIV_Y, RETURN_BUTTON_POS_X, RETURN_BUTTON_POS_Y, RETURN_BUTTON_COLOR, game);
            Return.LoadContent(RETURN_VALUE, Variable.FONT_PATH);

            DISCRIPTION_POS_X = gameX / 2;
            DISCRIPTION_POS_Y = gameY / 2 - 150;
            Discription.Position = new SFML.System.Vector2f(DISCRIPTION_POS_X, DISCRIPTION_POS_Y);
            font = new Font(Variable.FONT_PATH);
            if (File.Exists(DISCRIPTION_FILE))
            {
                using (FileStream fileStream = new FileStream(DISCRIPTION_FILE, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        discription = new SFML.Graphics.Text(reader.ReadToEnd(), font);
                    }
                }
            }
            else
            {
                discription = new SFML.Graphics.Text("FILESTREAM", font);
            }
            discription.CharacterSize = (uint)(BLOCK_DIV_Y/4);
            discription.FillColor = Color.Black;
            discription.Position = new SFML.System.Vector2f(DISCRIPTION_POS_X, DISCRIPTION_POS_Y);
            discription.Origin = new SFML.System.Vector2f(discription.GetLocalBounds().Width / 2, discription.GetLocalBounds().Height / 2);
        }

        public void Update()
        {
            if(!Mouse.IsButtonPressed(Mouse.Button.Left)){
                delay = true;
            }
            if (delay == true)
            {
                var MousePos = Mouse.GetPosition(game.window);
                if (Return.GetGlobalBounds().Contains(MousePos.X, MousePos.Y))
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        game.GoToLobby();
                    }
                }
            }
        }
    }
}
