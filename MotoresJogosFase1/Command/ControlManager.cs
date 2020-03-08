using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MotoresJogosFase1
{
    public class ControlManager
    {
        public static List<Command> currentCommands;
        //static List<Command> oldCommands;

        public static void Initialize()
        {
            currentCommands = new List<Command>();
            //oldCommands = new List<Command>();
        }

        public static void Update()
        {
            if (currentCommands.Count > 0)
            {
                foreach(Command c in currentCommands)
                {
                    c.Execute();
                    //oldCommands.Add(c);
                }
                currentCommands.Clear();
            }
        }

    }
}
