//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MotoresJogosFase1
{
    public class Ship
    {
        Model model;

        private Matrix world;
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private BoundingSphere boundingSphere;

        public BoundingSphere BoundingSphere
        {
            get { return boundingSphere; }
            set { boundingSphere = value; }
        }

        private bool died;
        public bool Died
        {
            get { return died; }
            set { died = value; }
        }

        private Vector3 dir;

        public Vector3 Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        //Props
        public Ship(Vector3 position, float speed, Vector3 dir)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            dir.Normalize();
            this.dir = dir;
            died = false;
        }

        public virtual void LoadContent()
        {
            model = ShipModel.Model;

            if(model == null)
            {
                throw new System.Exception("Model is null");
            }

            foreach (ModelMesh mesh in model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(boundingSphere, mesh.BoundingSphere);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            position += speed * gameTime.ElapsedGameTime.Milliseconds * dir;

            //NEW
            if(position.Z <= -ShipPool.deathDist)
            {
                died = true;
            }

            boundingSphere.Center = position;
            world = Matrix.CreateTranslation(position);
        }

        public virtual void Draw(Matrix View, Matrix Projection)
        {
            try
            {
                foreach (ModelMesh mesh in model.Meshes)
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

        public void Respawn(Vector3 position)
        {
            died = false;
            this.position = position;
        }

        public void Fire()
        {
            BulletPool.ActivateOneBullet(position, new Vector3(0f, 0f, -1f));
        }
    }
}