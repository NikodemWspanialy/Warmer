using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    internal class Button
    {
        RectangleShape button;
        SFML.Graphics.Color fillColor;
        Text text;
        Font fontsFamily;

        float buttonSizeX, buttonSizeY;
        float buttonPosX, buttonPosY;
        SFML.Graphics.Color buttonColor;

        Game game;
        public Button(float sizeX,float sizeY,float posX,float posY, SFML.Graphics.Color color,Game game) 
        {
            this.buttonSizeX = sizeX;
            this.buttonSizeY = sizeY;
            this.buttonPosX = posX;
            this.buttonPosY = posY;
            this.buttonColor = color;
            this.game = game;

            button = new RectangleShape(new SFML.System.Vector2f(buttonSizeX,buttonSizeY));
            button.Origin = new SFML.System.Vector2f(buttonSizeX/2,buttonSizeY/2);
            button.Position = new SFML.System.Vector2f(buttonPosX,buttonPosY);
            button.FillColor = buttonColor;
        }
        public void LoadContent(string value, string fontpath) 
        {
            this.fontsFamily = new Font(fontpath);
            this.text = new Text(value, fontsFamily);
            this.text.CharacterSize = (uint)(buttonSizeY / 2);
            this.text.FillColor = Color.Black;
            text.Position = new SFML.System.Vector2f(buttonPosX, buttonPosY);
            text.Origin = new SFML.System.Vector2f(text.GetLocalBounds().Width / 2, text.GetLocalBounds().Height / 2);
        }

        public FloatRect GetGlobalBounds()
        {
            return button.GetGlobalBounds();
        }
        public void Draw() 
        {
            game.window.Draw(button);
            game.window.Draw(text);
        }
    }
}
