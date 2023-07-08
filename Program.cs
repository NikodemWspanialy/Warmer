using BugTraveler;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Drawing;
using SFML.Audio;

internal class Program
{

    private static void Main(string[] args)
    {
        SoundManager soundManager = new SoundManager();
        Game game = new Game();
        Thread MusicT = new Thread(soundManager.WaitFor);
        MusicT.Start();
        game.Run();
    }
}