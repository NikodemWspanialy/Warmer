using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    static public class Variable
    {
        public static string FONT_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "FONTS\\Sygma.ttf");
        public static string SCOREBOARD_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "FILES\\scoreboard.txt");
        public static string SCOREBOARD_COPY_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "FILES\\scoreboardCopy.txt");
        public static string DESCRIPTION_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "FILES\\discription.txt");
        public static string COCKROACH_SPRITE_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "IMG\\Cockroach.png");
        public static string PLAYER_SPRITE_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "IMG\\BEE.png");
        public static string GAME_MUSIC_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "Sounds\\game.wav");
        public static string DEAD_MUSIC_PATH = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "Sounds\\dead.wav");
    }

}
