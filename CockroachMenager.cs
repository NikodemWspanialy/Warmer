using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    internal class CockroachMenager : IGameObject
    {
        IEnumerable<Cockroach> cockroaches;
        const int GAME_DIFFICULT = 5;//value which dekrementuje STARTING_FPS_BEETWEN_COCKROACH_SPAWN
        const int LEVEL_INCRESING_EVERY_FPS = 600;//value/fps(default 60) = number of sec after the game lvl increse
        const int STARTING_FPS_BETWEEN_COCKROACH_SPAWN = 60;
        const int LOWEST_FPS_BETWEEN_SPAWN = 10;

        int thisLevelTimeToNextSpawn;
        int timeToNextSpawn;
        int timeToNextIncreseLvl;

        Game game;
        uint gameX, gameY;
        Player player;
        public CockroachMenager(Player player) 
        {
            this.player = player;
            cockroaches = new List<Cockroach>();
        }

        public void Update()
        {
            if(timeToNextIncreseLvl <= 0)
            {
                if(thisLevelTimeToNextSpawn > LOWEST_FPS_BETWEEN_SPAWN)
                     thisLevelTimeToNextSpawn -= GAME_DIFFICULT;
                timeToNextIncreseLvl = LEVEL_INCRESING_EVERY_FPS;
            }
            else
            {
                timeToNextIncreseLvl--;
            }
            if(timeToNextSpawn <= 0)
            {
                Cockroach cockroach = new Cockroach(player);
                cockroach.LoadContent(game, gameX, gameY);
                List<Cockroach> tempList = cockroaches.ToList();
                tempList.Add(cockroach);
                cockroaches = tempList;

                timeToNextSpawn = thisLevelTimeToNextSpawn;
            }
            else
            {
                timeToNextSpawn--;
            }

            foreach(var cockroach in cockroaches)
            {
                if (cockroach != null)
                    cockroach.Update();
            }
        }

        public void Draw()
        {
            foreach (var cockroach in cockroaches)
            {
                if (cockroach.toDelete == false)
                    cockroach.Draw();
                else
                {
                    List<Cockroach> temp = cockroaches.ToList();
                    temp.Remove(cockroach);
                    cockroaches = temp;
                }
            }
        }

        public void LoadContent(Game game, uint x, uint y)
        {
            this.game = game;
            gameX = x;
            gameY = y;
            timeToNextSpawn = STARTING_FPS_BETWEEN_COCKROACH_SPAWN;
            timeToNextIncreseLvl = LEVEL_INCRESING_EVERY_FPS;
            thisLevelTimeToNextSpawn = STARTING_FPS_BETWEEN_COCKROACH_SPAWN;
            foreach (var cockroach in cockroaches)
            {
                if(cockroach != null)
                cockroach.LoadContent(game, x, y);
            }
        }
    }
}
