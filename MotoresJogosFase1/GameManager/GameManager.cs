using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresJogosFase1
{
    public enum GameState
    {
        MainMenu,
        Pause,
        InGame,
        Lost
    }


    public static class GameManager
    {
        private static GameState gameState;

        public static GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public static void Initialize()
        {
            gameState = GameState.MainMenu;
        }
    }
}
