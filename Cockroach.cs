using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    internal class Cockroach : IGameObject
    {
        //consts
        const float COCKROACH_COLIDER_SIZE = 50;
        const float BASIC_SPEED = 5F;
        float OBLIQUE_SPEED = (float)(Math.Round(BASIC_SPEED / 1.4,1));
        const int TIME_IN_MIDDLE = 300;
        const int TIME_BEFORE_CHANGE_DIR = 150;

        Texture TEXTURE = new Texture(@"C:\PRJs\CS_PRJs\PRJs\BugTraveler\IMG\Cockroach.png");
        //cockroach
        Sprite cockroach;
        public bool toDelete = false;
        CircleShape cockroach_colider;
        //Gracz oraz plansza 
        Player player;
        Game game;
        uint gameX, gameY,tenPercentOfX,tenPercentOfY;
        //movement params
        bool wasOnMidle = false;
        CockroachInOutDirection spawnPoint;
        CockroachInOutDirection leavingDir;
        Random random = new Random();
        int timeInMiddle;
        float dirX, dirY;
        int timePerMove;
        bool firstDir = true;
        int rotation;

        public Cockroach(Player player)
        {
            this.player = player;
        }
        public void LoadContent(Game game, uint x, uint y) 
        {
            this.game = game;
            gameX = x;
            gameY = y;
            tenPercentOfX = x / 10;
            tenPercentOfY = y / 10;

            cockroach = new Sprite(TEXTURE)
            {
                Origin = new SFML.System.Vector2f(TEXTURE.Size.X / 2, TEXTURE.Size.Y / 2)
            };
            cockroach_colider = new CircleShape(COCKROACH_COLIDER_SIZE)
            {
                Position = cockroach.Position,
                FillColor = Color.Cyan,
                Origin = cockroach.Position,
            };
            Spawn();
            timeInMiddle = TIME_IN_MIDDLE;
            timePerMove = 0;
            leavingDir = CockroachInOutDirection.Null;
        }

        private void Spawn()
        {
            int randomSide = random.Next(1, 5);
            spawnPoint = (CockroachInOutDirection)randomSide;
            float spawnX, spawnY;
            switch (randomSide)
            {
                case 1:
                    spawnX = (float)-(tenPercentOfX);
                    spawnY = (float)random.Next((int)tenPercentOfY, (int)(gameY - tenPercentOfY));
                    break;
                case 2:
                    spawnX = (float)(gameX + tenPercentOfX);
                    spawnY = (float)random.Next((int)tenPercentOfY, (int)(gameY - tenPercentOfY));
                    break;
                case 3:
                    spawnX = (float)random.Next((int)tenPercentOfX, (int)(gameX - tenPercentOfX));
                    spawnY = (float)-(tenPercentOfY);
                    break;
                case 4:
                    spawnX = (float)random.Next((int)tenPercentOfX, (int)(gameX - tenPercentOfX));
                    spawnY = (float)(gameY + tenPercentOfY);
                    break;
                default:
                    spawnX = 0;
                    spawnY = 0;
                    break;
            }
            cockroach.Position = new Vector2f(spawnX, spawnY);
        }

        public void Update() 
        {
            cockroach_colider.Position = cockroach.Position + new Vector2f(-COCKROACH_COLIDER_SIZE, -COCKROACH_COLIDER_SIZE);
            MoveMenager();
            if (cockroach_colider.GetGlobalBounds().Intersects(player.GetGlobalBounds())) 
            {
                game.GoToGameOver(this);
                //zderzenie i przegrana koniec gry wywolanie jakies funcji jeszcze nie wiem jakiej pewnie z klasy Game
            }
        }
        public void Draw() 
        {
            game.window.Draw(cockroach); 
            //game.window.Draw(cockroach_colider); 
        }

        private void MoveMenager()
        {
            if(!wasOnMidle)
            wasOnMidle = CheckIfMiddle();
            if (!wasOnMidle)
            {
                GoToMiddle();
            }
            else if (timeInMiddle > 0)
                InMiddle();
            else
            {
                GoOut();
            }
        }


        private bool CheckIfMiddle()
        {
            if (cockroach.Position.X > tenPercentOfX && cockroach.Position.X < (gameX - tenPercentOfX))
                if (cockroach.Position.Y > tenPercentOfY && cockroach.Position.Y < (gameY - tenPercentOfY))
                    return true;
            return false;
        }

        private void GoToMiddle()
        {
            switch (spawnPoint)
            {
                case CockroachInOutDirection.Left:
                    cockroach.Position += new SFML.System.Vector2f(BASIC_SPEED,0);
                    cockroach.Rotation = 0;
                    break;
                case CockroachInOutDirection.Right:
                    cockroach.Position += new SFML.System.Vector2f(-BASIC_SPEED, 0);
                    cockroach.Rotation = 180;
                    break;
                case CockroachInOutDirection.Top:
                    cockroach.Position += new SFML.System.Vector2f(0, BASIC_SPEED);
                    cockroach.Rotation = 90;
                    break;
                case CockroachInOutDirection.Bottom:
                    cockroach.Position += new SFML.System.Vector2f(0, -BASIC_SPEED);
                    cockroach.Rotation = 270;
                    break;
            }
        }
        private void GoOut()
        {
            if (chceckIfOutOfborder())
            {
                toDelete = true;
            }
            if(leavingDir == CockroachInOutDirection.Null)
            {
                float leftDis = cockroach.Position.X;
                float rightDis = gameX - cockroach.Position.X;
                float topDis = cockroach.Position.Y;
                float bottomDis = gameY - cockroach.Position.Y;
                if (leftDis <= rightDis && leftDis <= bottomDis && leftDis <= topDis)
                    leavingDir = CockroachInOutDirection.Left;
                else if (rightDis <= bottomDis && rightDis <= topDis && rightDis <= leftDis)
                    leavingDir = CockroachInOutDirection.Right;
                else if (topDis <= rightDis && topDis <= bottomDis && topDis <= leftDis)
                    leavingDir = CockroachInOutDirection.Top;
                else
                    leavingDir = CockroachInOutDirection.Bottom;
            }
            if(leavingDir == CockroachInOutDirection.Left)
            {
                cockroach.Position += new Vector2f(-BASIC_SPEED,0);
                cockroach.Rotation = 180;
            }
            else if (leavingDir == CockroachInOutDirection.Right)
            {
                cockroach.Position += new Vector2f(BASIC_SPEED, 0);
                cockroach.Rotation = 0;
            }
            else if (leavingDir == CockroachInOutDirection.Top)
            {
                cockroach.Position += new Vector2f(0,-BASIC_SPEED);
                cockroach.Rotation = 90;
            }
            else
            {
                cockroach.Position += new Vector2f(0,BASIC_SPEED);
                cockroach.Rotation = -90;
            }
        }

        private bool chceckIfOutOfborder()
        {
            if (cockroach.Position.X > gameX + tenPercentOfX || cockroach.Position.X < -tenPercentOfX)
                return true;
            if (cockroach.Position.Y > gameY + tenPercentOfY || cockroach.Position.Y < -tenPercentOfY)
                return true;
            return false;
        }

        private void InMiddle()
        {
            timeInMiddle--;
            int newDir = default;
            if (timePerMove > 0)
            {
                cockroach.Position += new Vector2f(dirX, dirY);
                cockroach.Rotation = rotation;
                timePerMove--;
            }
            else
            {
                timePerMove = TIME_BEFORE_CHANGE_DIR;
                if(firstDir == true)
                {
                    firstDir = false;
                    if (spawnPoint == CockroachInOutDirection.Top)
                        newDir = random.Next(4, 7);
                    else if (spawnPoint == CockroachInOutDirection.Left)
                        newDir = random.Next(2, 5);
                    else if (spawnPoint == CockroachInOutDirection.Right)
                        newDir = random.Next(6, 9);
                    else if (spawnPoint == CockroachInOutDirection.Bottom)
                    {
                        newDir = random.Next(1, 4);
                        if (newDir == 3)
                            newDir = 8;
                    }
                }
                else
                {
                    newDir = random.Next(1, 9);

                }
                switch (newDir)
                {
                    case 1:
                        dirX = 0;
                        dirY = -BASIC_SPEED;
                        rotation = -90;
                        break;
                    case 2:
                        dirX = OBLIQUE_SPEED;
                        dirY = -OBLIQUE_SPEED;
                        rotation = -45;
                        break;
                    case 3:
                        dirX = BASIC_SPEED;
                        dirY = 0;
                        rotation = 0;
                        break;
                    case 4:
                        dirX = OBLIQUE_SPEED;
                        dirY = OBLIQUE_SPEED;
                        rotation = 45;
                        break;
                    case 5:
                        dirX = 0;
                        dirY = BASIC_SPEED;
                        rotation = 90;
                        break;
                    case 6:
                        dirX = -OBLIQUE_SPEED;
                        dirY = OBLIQUE_SPEED;
                        rotation = 135;
                        break;
                    case 7:
                        dirX = -BASIC_SPEED;
                        dirY = 0;
                        rotation = 180;
                        break;
                    case 8:
                        dirX = -OBLIQUE_SPEED;
                        dirY = -OBLIQUE_SPEED;
                        rotation = -135;
                        break;

                }
            }

        }
    }
}
