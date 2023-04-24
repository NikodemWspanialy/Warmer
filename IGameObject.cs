using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    interface IGameObject
    {
        public  void Update();
        public  void Draw();
        public  void LoadContent(Game game, uint x, uint y);
    }

}
