using SFML.Audio;
using SFML.Window;
using System;

namespace BugTraveler
{
     internal class SoundManager
    {
        private Music music;
        public void WaitFor()
        {
            music = new Music(Variable.GAME_MUSIC_PATH);
            GameStatus gameStatus;
            while (true)
            {
                ThreadManager.sygnal.WaitOne();
                while (ThreadManager.queue.TryDequeue(out gameStatus))
                {
                    Console.WriteLine(gameStatus);
                    switch (gameStatus)
                    {
                        case GameStatus.InGame: 
                            PlayGameMusic();
                            break;
                        case GameStatus.GameOver: 
                            PlayEndMusic();
                            break;
                    }
                }
            }
        }
        private void PlayGameMusic()
        {
            try
            {
                if(music.Status == SoundStatus.Playing) {
                    music.Pause();
                }
            music = new Music(Variable.GAME_MUSIC_PATH);
            music.Play();
            }
            catch(Exception E){ Console.WriteLine("cant read game music\n" + E.ToString()); }
        }
        private     void PlayEndMusic()
        {
            try
            {
                music.Pause();
            music = new Music(Variable.DEAD_MUSIC_PATH);
                music.Play();
            }
            catch { Console.WriteLine("Cant play end sound"); }
        }
    }
}
