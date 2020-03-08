//using AlienGrab;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using System;

namespace MotoresJogosFase1
{
    public class Player : Ship
    {
        private Matrix world;
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        private Vector3 position;

        private float speed;

        private BoundingSphere boundingSphere;

        //private bool died;

        Vector3 dir;

        public Player(Vector3 position, float speed, Vector3 dir) : base(position, speed, dir)
        {
            this.position = position;
            this.speed = speed;
            this.dir = dir;
            this.dir.Normalize();

            world = Matrix.CreateTranslation(position);
            //died = false;

            if (this.speed == 0 || this.dir == Vector3.Zero || this.dir == null)
            {
                throw new System.Exception("Speed or direction of player is invalid");
            }

            //MessageBus.InsertNewMessage(new ConsoleMessage(speed.ToString()));
        }

        public override void Update(GameTime gameTime)
        {
            position += speed * gameTime.ElapsedGameTime.Milliseconds * dir;

            //NEW
            if (position.Z <= -ShipPool.deathDist)
            {
                //died = true;
                position = Vector3.Zero;
                MessageBus.InsertNewMessage(new ConsoleMessage("Player died, reseted pos"));
            }

            //kill ships we collide with
            foreach (Ship s in ShipPool.ships)
            {
                if (boundingSphere.Intersects(s.BoundingSphere))
                {
                    s.Died = true;
                }
            }

            boundingSphere.Center = position;
            world = Matrix.CreateTranslation(position);

            //MessageBus.InsertNewMessage(new ConsoleMessage(position.ToString() + " " + speed.ToString() + " " + dir.ToString()));
        }

        public override void Draw(Matrix View, Matrix Projection)
        {
            try
            {
                foreach (ModelMesh mesh in ShipModel.Model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.LightingEnabled = false;
                        effect.World = World;
                        effect.View = View;
                        effect.Projection = Projection;
                    }
                    mesh.Draw();
                }
            }
            catch (Exception e)
            {
                MessageBus.InsertNewMessage(new ConsoleMessage("Failed to draw ship: " + e.ToString()));
            }

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }

    }
}