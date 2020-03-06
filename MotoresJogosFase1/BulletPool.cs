using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

namespace MotoresJogosFase1
{
    public static class BulletPool
    {
        static List<Bullet> bullets;
        static List<Bullet> inactiveBullets;
        static List<Bullet> tempBullets;

        static public void Initialize()
        {
            bullets = new List<Bullet>(100);
            inactiveBullets = new List<Bullet>(100);
            tempBullets = new List<Bullet>(100);
        }

        static public void CreateBullets()
        {
            for (int i = 0; i < 100; i++)
            {
                inactiveBullets.Add(new Bullet(Vector3.Zero, 0.5f, 0.01f, Vector3.Zero));
            }
        }

        static int deadShipsCounter;
        static public void Update(GameTime gameTime, Random random, ContentManager contentManager)
        {
            deadShipsCounter = 0;
            foreach (Bullet b in bullets)//check if any died
            {
                
                b.Update(gameTime);
                if (b.Died)
                {
                    tempBullets.Add(b);
                    deadShipsCounter++;
                }
            }
            foreach (Bullet b in tempBullets)//remove dead from active and store them on inactive
            {
                inactiveBullets.Add(b);
                bullets.Remove(b);
            }
            tempBullets.Clear();//clear temp list
        }

        static public void Draw(Camera camera)
        {
            foreach (Bullet b in bullets)
            {
                b.Draw(camera.View, camera.Projection);
            }
        }

        public static void ActivateOneBullet(Vector3 pos, Vector3 dir)
        {
            if (inactiveBullets.Count > 0)//get one if inactive got bullets
            {
                inactiveBullets[0].Respawn(pos, dir);
                bullets.Add(inactiveBullets[0]);
                inactiveBullets.Remove(inactiveBullets[0]);
            }
            else//create one if inactive doesn't have any
            {
                bullets.Add(new Bullet(pos, 0.5f, 0.01f, dir));
            }
        }
    }
}