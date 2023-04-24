
namespace BugTraveler
{
    public enum CockroachInOutDirection
    {
        Null = 0, Left = 1, Right = 2, Top = 3, Bottom = 4,
    }
    public enum CockroachMiddleDirection
    {
        Left = 7, Right = 3, Top = 1, Bottom = 5, LeftTop = 8, LeftBottom = 6, RightTop = 2, RightBottom = 4
    }
    public enum GameStatus
    {
        Lobby, InGame, GameOver, About , Scoreboard , AddScore
    }
    public enum PlayerDirection
    {
        None = 0, Left = 7, Right = 3, Top = 1, Bottom = 5, LeftTop = 8, LeftBottom = 6, RightTop = 2, RightBottom = 4
    }


}
