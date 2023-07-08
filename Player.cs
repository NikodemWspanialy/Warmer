using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace BugTraveler
{
    internal class Player : IGameObject
    {
        //const uint PLAYER_X = 100, PLAYER_Y = 100;
        const uint PLAYER_COLIDER = 40;
        const float MOVING_SPEED = 10F;
        float OBLIQUE_SPEED = (float)(Math.Round(MOVING_SPEED / 1.4, 1));
        Texture TEXTURE = new Texture(Variable.PLAYER_SPRITE_PATH);
        const float PLAYER_START_POSITION_X = 100f, PLAYER_START_POSITION_Y = 100f;

        Sprite player;
        Game game;
        uint gameX, gameY;
        CircleShape player_colider;

        PlayerDirection direction;
        public Player() { }
        public void Update() { Move(); }
        public void Draw() {  game.window.Draw(player);  }
        public void LoadContent(Game game, uint x, uint y) 
        {
            this.game = game;
            gameX = x;
            gameY = y;

            player = new Sprite(TEXTURE)
            {
                
                Texture = new Texture(TEXTURE),
                Origin = new Vector2f(TEXTURE.Size.X / 2f, TEXTURE.Size.Y / 2f),
                Position = new Vector2f(gameX/2, gameY/2)
            };
            player_colider = new CircleShape(PLAYER_COLIDER)
            {
                Origin = new Vector2f(PLAYER_COLIDER / 2, PLAYER_COLIDER / 2),
                FillColor = Color.Blue,
                Position = player.Position
            };
        }
        public FloatRect GetGlobalBounds()
        {
            player_colider.Position = player.Position + new Vector2f(-22,-22);
            return player_colider.GetGlobalBounds();
        }
        public void Move()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && Keyboard.IsKeyPressed(Keyboard.Key.Right))
                direction = PlayerDirection.RightTop;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && Keyboard.IsKeyPressed(Keyboard.Key.Left))
                direction = PlayerDirection.LeftTop;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && Keyboard.IsKeyPressed(Keyboard.Key.Right))
                direction = PlayerDirection.RightBottom;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && Keyboard.IsKeyPressed(Keyboard.Key.Left))
                direction = PlayerDirection.LeftBottom;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                direction = PlayerDirection.Top;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                direction = PlayerDirection.Bottom;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                direction = PlayerDirection.Left;
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                direction = PlayerDirection.Right;
            else
                direction = PlayerDirection.None;
            switch(direction)
            {
                case PlayerDirection.LeftTop:
                    player.Rotation = 225;
                    if(player.Position.X > TEXTURE.Size.X/2)
                        player.Position += new Vector2f(-OBLIQUE_SPEED,0);
                    if (player.Position.Y > TEXTURE.Size.Y / 2)
                        player.Position += new Vector2f(0, -OBLIQUE_SPEED);
                    break;
                case PlayerDirection.RightTop:
                    player.Rotation = 315;
                    if (player.Position.X < gameX - TEXTURE.Size.X / 2)
                        player.Position += new Vector2f(OBLIQUE_SPEED, 0);
                    if (player.Position.Y > TEXTURE.Size.Y / 2)
                        player.Position += new Vector2f(0, -OBLIQUE_SPEED);
                    break;
                case PlayerDirection.RightBottom:
                    player.Rotation = 45;
                    if (player.Position.X < gameX - TEXTURE.Size.X / 2)
                        player.Position += new Vector2f(OBLIQUE_SPEED, 0);
                    if (player.Position.Y < gameY - TEXTURE.Size.Y / 2)
                        player.Position += new Vector2f(0, OBLIQUE_SPEED);
                    break;
                case PlayerDirection.LeftBottom:
                    player.Rotation = 135;
                    if (player.Position.X > TEXTURE.Size.X / 2)
                        player.Position += new Vector2f(-OBLIQUE_SPEED, 0);
                    if (player.Position.Y < gameY - TEXTURE.Size.Y / 2)
                        player.Position += new Vector2f(0, OBLIQUE_SPEED);
                    break;
                case PlayerDirection.Top:
                    player.Rotation = 270;
                    if (player.Position.Y > TEXTURE.Size.Y / 2)
                        player.Position += new Vector2f(0, -MOVING_SPEED);
                    break;
                case PlayerDirection.Right:
                    player.Rotation = 0;
                    if (player.Position.X < gameX - TEXTURE.Size.X / 2)
                        player.Position += new Vector2f(MOVING_SPEED, 0);
                  
                    break;
                case PlayerDirection.Bottom:
                    player.Rotation = 90;
                   
                    if (player.Position.Y < gameY - TEXTURE.Size.Y / 2)
                        player.Position += new Vector2f(0, OBLIQUE_SPEED);
                    break;
                case PlayerDirection.Left:
                    player.Rotation = 180;
                    if (player.Position.X > TEXTURE.Size.X / 2)
                        player.Position += new Vector2f(-OBLIQUE_SPEED, 0);
                    break;
                default:break;
            }
            
        }
    }

}

