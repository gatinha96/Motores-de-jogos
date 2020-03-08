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
        static float speed;

        static List<Bullet> bullets;
        static List<Bullet> inactiveBullets;
        static List<Bullet> tempBullets;

        static int deadShipsCounter;

        static public void Initialize(float speed)
        {
            BulletPool.speed = speed;

            bullets = new List<Bullet>(100);
            inactiveBullets = new List<Bullet>(100);
            tempBullets = new List<Bullet>(100);
        }

        static public void CreateBullets()
        {
            for (int i = 0; i < 100; i++)
            {
                inactiveBullets.Add(new Bullet(Vector3.Zero, speed, Vector3.Zero));
            }
        }

        static public void Update(GameTime gameTime, Random random)
        {
            deadShipsCounter = 0;
            for (int j=0;j<bullets.Count;j++)//check if any died
            {
                bullets[j].Update(gameTime);

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i] != bullets[j] && bullets[j].BoundingSphere.Intersects(bullets[i].BoundingSphere))
                    {
                        bullets[j].Died = true;
                        bullets[i].Died = true;

                        MessageBus.Messages.Add(new ConsoleMessage("2 bullets collided!"));
                    }
                }

                foreach (Ship s in ShipPool.ships)
                {
                    if(bullets[j].BoundingSphere.Intersects(s.BoundingSphere))
                    {
                        bullets[j].Died = true;
                        s.Died = true;

                        MessageBus.Messages.Add(new ConsoleMessage("1 ship was hit!"));
                    }
                }

                if (bullets[j].Died)
                {
                    tempBullets.Add(bullets[j]);
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
                if (camera.InView(b.BoundingSphere.Transform(b.World)))
                {
                    b.Draw(camera.View, camera.Projection);
                }
            }
        }

        public static void ActivateOneBullet(Vector3 pos, Vector3 dir)
        {
            dir.Normalize();
            pos += dir * 0.5f;

            if (inactiveBullets.Count > 0)//get one if inactive got bullets
            {
                inactiveBullets[0].Respawn(pos, dir);
                bullets.Add(inactiveBullets[0]);
                inactiveBullets.Remove(inactiveBullets[0]);
            }
            else//create one if inactive doesn't have any
            {
                bullets.Add(new Bullet(pos, speed, dir));
            }
        }
    }
}