using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MotoresJogosFase1
{
    public static class MemProfiling
    {
        static long initialMemory, lastMemMeasure, mem;
        static bool retrieveInitialMemory;

        static public void Update()
        {
            if(retrieveInitialMemory)
            {
                initialMemory = GC.GetTotalMemory(false);
                retrieveInitialMemory = false;
            }

            mem = GC.GetTotalMemory(false) - initialMemory;
            if(mem>0&&mem > lastMemMeasure)
            {
                //MessageBus.InsertNewMessage(new ConsoleMessage("MEM ALERT: " + (mem / 1000 + "k")));
            }
            lastMemMeasure = mem;
        }
    }
}