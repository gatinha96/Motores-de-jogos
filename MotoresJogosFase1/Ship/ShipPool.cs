using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

namespace MotoresJogosFase1
{
    public static class ShipPool
    {
        public static List<Ship> ships;
        static List<Ship> inactiveShips;
        static List<Ship> tempShips;

        static float shipArea;
        public static float deathDist;
        static int max, min;
        static float maxMinMultiplier;

        public static void Initialize(float deathDist, float shipArea,int max, int min, float maxMinMultiplier)
        {
            ShipPool.deathDist = deathDist;
            ships = new List<Ship>(100);
            inactiveShips = new List<Ship>(100);
            tempShips = new List<Ship>(100);
            ShipPool.shipArea = shipArea;
            ShipPool.min = min;
            ShipPool.max = max;
            ShipPool.maxMinMultiplier = maxMinMultiplier;
        }

        public static void LoadContent()
        {
            foreach(Ship s in inactiveShips)
            {
                s.LoadContent();
            }
        }

        static public void CreateShips()
        {
            for (int i = 0; i < 100; i++)
            {
                inactiveShips.Add(new Ship(RandomShipPos(), CalculateSpeed(), new Vector3(0, 0, -1)));
            }
        }

        static int deadShipsCounter;
        static public void Update(GameTime gameTime, Random random)
        {
            deadShipsCounter = 0;
            for (int i = 0; i < ships.Count; i++)//check if any died
            {
                ships[i].Update(gameTime);

                //Kill if collided with something
                for (int j = 0; j < ships.Count; j++)
                {
                    if (ships[j] != ships[i])
                    {
                        if (ships[i].BoundingSphere.Intersects(ships[j].BoundingSphere))
                        {
                            ships[i].Died = true;
                            ships[j].Died = true;

                            //MessageBus.Messages.Add(new ConsoleMessage("2 ships collided!"));
                        }
                    }
                }

                if (ships[i].Died)
                {
                    tempShips.Add(ships[i]);
                    deadShipsCounter++;
                }
            }


            for (int i = 0; i < tempShips.Count; i++)//remove dead from active and store them on inactive
            {
                inactiveShips.Add(tempShips[i]);
                ships.Remove(tempShips[i]);
            }
            tempShips.Clear();//clear temp list


            //Respawn the dead ships
            for (int i = 0; i < deadShipsCounter; i++)
            {
                ActivateOneShip();
            }


            //TESTE
            //De vez em quando, ativar mais uma nave
            //if (random.Next(0, 2) == 1)
            //{
            //    ActivateOneShip();
            //    //MessageBus.InsertNewMessage(new ConsoleMessage("Nave adicionada! Total: " + (ships.Count + inactiveShips.Count)));
            //}

            //MessageBus.InsertNewMessage(new ConsoleMessage("Inactive ships: " + inactiveShips.Count + " ,Active Ships: " + ships.Count + " Total: " + (ships.Count + inactiveShips.Count)));
        }

        static public void Draw(Camera camera)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                if (camera.InView(ships[i].BoundingSphere.Transform(ships[i].World)))
                {
                    ships[i].Draw(camera.View, camera.Projection);
                }
            }
        }

        public static void ActivateOneShip()
        {
            if (inactiveShips.Count > 0)//create one each frame if inactive got ships
            {
                inactiveShips[0].Respawn(RandomShipPos());
                ships.Add(inactiveShips[0]);
                inactiveShips.Remove(inactiveShips[0]);
            }
            else//create one if inactive doesn't have any
            {
                Ship s = new Ship(RandomShipPos(), CalculateSpeed(), new Vector3(0, 0, -1));
                s.LoadContent();
                ships.Add(s);
            }
        }

        public static Vector3 RandomShipPos()
        {
            return new Vector3(Game1.random.Next((int)-shipArea, (int)shipArea), Game1.random.Next((int)-shipArea, (int)shipArea), Game1.random.Next((int)-shipArea, (int)shipArea));
        }

        static float CalculateSpeed()
        {
            return Game1.random.Next(min, max) * maxMinMultiplier * Game1.scale;
        }
    }
}