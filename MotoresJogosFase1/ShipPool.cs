using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

namespace MotoresJogosFase1
{
    public static class ShipPool
    {
        static List<Ship> ships;
        static List<Ship> inactiveShips;
        static List<Ship> tempShips;

        static public void Initialize()
        {
            ships = new List<Ship>(100);
            inactiveShips = new List<Ship>(100);
            tempShips = new List<Ship>(100);
        }

        static public void CreateShips(Random random,ContentManager Content)
        {
            for (int i = 0; i < 100; i++)
            {
                inactiveShips.Add(new Ship(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), Content, 0.5f, 0.01f));
            }
        }

        static int deadShipsCounter;
        static public void Update(GameTime gameTime, Random random, ContentManager contentManager)
        {
            deadShipsCounter = 0;
            foreach (Ship ship in ships)//check if any died
            {
                
                ship.Update(gameTime);
                if (ship.Died)
                {
                    tempShips.Add(ship);
                    deadShipsCounter++;
                }
            }
            foreach (Ship ship in tempShips)//remove dead from active and store them on inactive
            {
                inactiveShips.Add(ship);
                ships.Remove(ship);
            }
            tempShips.Clear();//clear temp list

            //Respawn the dead ships
            for(int i = 0; i < deadShipsCounter; i++)
            {
                ActivateOneShip(random, contentManager);
            }


            //TESTE
            //De vez em quando, ativar mais uma nave
            if(random.Next(0, 2) == 1)
            {
                ActivateOneShip(random, contentManager);
                MessageBus.InsertNewMessage(new ConsoleMessage("Nave adicionada! Total: " + (ships.Count + inactiveShips.Count)));
            }

            //MessageBus.InsertNewMessage(new ConsoleMessage("Inactive ships: " + inactiveShips.Count + " ,Active Ships: " + ships.Count + " Total: " + (ships.Count + inactiveShips.Count)));
        }

        static public void Draw(Camera camera)
        {
            foreach (Ship ship in ships)
            {
                ship.Draw(camera.View, camera.Projection);
            }
        }

        public static void ActivateOneShip(Random random, ContentManager contentManager)
        {
            if (inactiveShips.Count > 0)//create one each frame if inactive got ships
            {
                inactiveShips[0].Respawn(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)));
                ships.Add(inactiveShips[0]);
                inactiveShips.Remove(inactiveShips[0]);
            }
            else//create one if inactive doesn't have any
            {
                ships.Add(new Ship(new Vector3(random.Next(-1000, 1000), random.Next(-1000, 1000), random.Next(-1000, 1000)), contentManager, 0.5f, 0.01f));
            }
        }
    }
}