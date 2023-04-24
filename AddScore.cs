using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.IO;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace BugTraveler
{
    internal class AddScore
    {
        const string BUTTON_TEXT = "ENTER";

        const int SHAPE_POSITION_X = 170;
        const int SHAPE_POSITION_Y = 200;
        const int TEXT_POSITION_X = 185;
        const int TEXT_POSITION_Y = 210;
        const int ERROR_POSITION_X = 185;
        const int ERROR_POSITION_Y = 260;
        const int BUTTON_POSITION_X = 270;
        const int BUTTON_POSITION_Y = 300;
        const int BUTTON_TEXT_POSITION_X = 280;
        const int BUTTON_TEXT_POSITION_Y = 305;

        const int SHAPE_SIZE_X = 300;
        const int SHAPE_SIZE_Y = 50;
        const int BUTTON_SIZE_X = 100;
        const int BUTTON_SIZE_Y = 30;

        float score;
        string ERROR_TEXT = "";

        private RenderWindow renderWindow;
        private RectangleShape shape;
        private Font font;
        private Text text;
        private string name = "";
        private RectangleShape button;
        private Text buttonText;
        private Text error;
        private FloatRect buttonArea;

        string newScoreboard;
        public AddScore()
        {
            renderWindow = new RenderWindow(new VideoMode(640, 480), "ENTER YOUR NAME");
            renderWindow.Closed += (sender, e) => renderWindow.Close();
            renderWindow.TextEntered += OnTextEntered;

            shape = new RectangleShape(new Vector2f(SHAPE_SIZE_X, SHAPE_SIZE_Y))
            {
                Position = new Vector2f(SHAPE_POSITION_X, SHAPE_POSITION_Y),
                OutlineThickness = 2,
                FillColor = Color.White,
                OutlineColor = Color.Black
            };

            font = new Font(Variable.FONT_PATH);
            text = new Text("", font, 20)
            {
                Position = new Vector2f(TEXT_POSITION_X, TEXT_POSITION_Y),
                FillColor = Color.Black
            };
            error = new Text(ERROR_TEXT, font, 20)
            {
                Position = new Vector2f(ERROR_POSITION_X, ERROR_POSITION_Y),
                FillColor = Color.Red
            };
            button = new RectangleShape(new Vector2f(BUTTON_SIZE_X, BUTTON_SIZE_Y))
            {
                Position = new Vector2f(BUTTON_POSITION_X, BUTTON_POSITION_Y),
                OutlineThickness = 2,
                FillColor = Color.White,
                OutlineColor = Color.Black
            };

            buttonText = new Text(BUTTON_TEXT, font, 20)
            {
                Position = new Vector2f(BUTTON_TEXT_POSITION_X, BUTTON_TEXT_POSITION_Y),
                FillColor = Color.Black
            };

            buttonArea = new FloatRect(button.Position.X, button.Position.Y,
                button.Size.X, button.Size.Y);

            renderWindow.MouseButtonPressed += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left && buttonArea.Contains(e.X, e.Y))
                {
                    ButtonAction();
                }
            };

            renderWindow.Draw(button);
            renderWindow.Draw(buttonText);
        }

        public void addScoreMethod(float score)
        {
            this.score = score;
            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();
                renderWindow.Clear(Color.White);
                renderWindow.Draw(shape);
                renderWindow.Draw(text);
                renderWindow.Draw(button);
                renderWindow.Draw(buttonText);
                renderWindow.Draw(error);
                renderWindow.Display();
            }
        }

        private void OnTextEntered(object sender, TextEventArgs e)
        {
            if (e.Unicode == "\n") { return; }
            if (e.Unicode == "\t") { return; }
            if (e.Unicode == "\b")
            {
                if (name.Length > 0)
                {
                    name = name.Substring(0, name.Length - 1);
                    text.DisplayedString = name;
                }
            }
            else
            {
                Console.WriteLine("Wpisano tekst: " + e.Unicode);
                name += e.Unicode;
                text.DisplayedString = name;
            }
        }

        private void ButtonAction()
        {
            {
                string pattern = ".{3,}";
                bool zgodnoscRegexDlugosc = Regex.IsMatch(name, pattern);
                if (!zgodnoscRegexDlugosc)
                {
                    ERROR_TEXT = "More than 3 characters!";
                    error.DisplayedString = ERROR_TEXT;
                    return;
                }
                // pattern = ".{,10}";
                // zgodnoscRegexDlugosc = Regex.IsMatch(name, pattern);
                //if (!zgodnoscRegexDlugosc)
                //{
                //    ERROR_TEXT = "Less tha 10 characters!";
                //    error.DisplayedString = ERROR_TEXT;
                //    return;
                //}
                bool zgodnoscRangesZnaki = name.All(c => (char.IsAsciiLetterOrDigit(c)) || (Char.IsWhiteSpace(c))); //true jesli zly name
                if (zgodnoscRangesZnaki)
                {
                    SaveScore();
                    renderWindow.Close();
                }
                else
                {
                    ERROR_TEXT = "ONLY ENGLISH ALPHABET AND SPACEBAR";
                    error.DisplayedString = ERROR_TEXT;
                    name = "";
                    text.DisplayedString = name;
                }
            }
        }
        private void SaveScore()
        {
            try
            {
                Console.WriteLine(name);
                if (File.Exists(Variable.SCOREBOARD_FILE))
                {
                    string newScoreboard = "";
                    string tempText = File.ReadAllText(Variable.SCOREBOARD_FILE);
                    string[] strings = tempText.Split('\n');
                    int columnToWrite = -1;
                    for (int i = 0; i < strings.Length; i++)
                    {
                        string[] temp = strings[i].Split("#");
                        if(temp.Length != 4)
                        {
                            throw new Exception("bład w plikach lub brak rekordu do top10");
                        }
                        float value;
                        if(!float.TryParse(temp[1], out value))
                        {
                            throw new Exception("Blad konwersji typu na float");
                        }    
                        if (value < score)
                        {
                            columnToWrite = i;
                            break;
                        }
                    }
                    if (columnToWrite == -1)
                    {
                        throw new Exception("Dane nie zapisane wynik mniejszy niz top10");
                    }
                    else
                    {
                        int counter = 0;
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == columnToWrite)
                            {
                                newScoreboard += "TIME:#" + score.ToString() + "#NICK:#" + name + '\n';
                                counter++;
                            }
                            if (counter < strings.Length)
                            {
                                newScoreboard += strings[i] + '\n';
                                counter++;
                            }
                        }
                        this.newScoreboard = newScoreboard;

                    }

                    if (File.Exists(Variable.SCOREBOARD_COPY_FILE))
                        File.Delete(Variable.SCOREBOARD_COPY_FILE);
                    File.Copy(Variable.SCOREBOARD_FILE, Variable.SCOREBOARD_COPY_FILE);

                    File.WriteAllText(Variable.SCOREBOARD_FILE, newScoreboard);
                    return;


                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex + "\n");
            }
        }
    }
}

