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
        static KeyboardState kState;
        static Command fireweapon = new FireWeapon();

        public static Command Update()
        {
            kState = Keyboard.GetState();

            if(kState.IsKeyDown(Keys.Space))
            {
                MessageBus.InsertNewMessage(new ConsoleMessage("Firing!"));
                return fireweapon;
            }

            return null;
        }

    }
}
