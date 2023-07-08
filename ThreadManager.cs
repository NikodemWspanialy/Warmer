using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTraveler
{
    static class ThreadManager
    {
        static public Mutex mutex = new Mutex();
        static public ConcurrentQueue<GameStatus> queue = new ConcurrentQueue<GameStatus>();
        static public AutoResetEvent sygnal = new AutoResetEvent(false);
    }
}
