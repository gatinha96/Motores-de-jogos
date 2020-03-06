//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MotoresJogosFase1
{
    public class Ship
    {
        ShipModel model;

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

        //NEW
        private bool died;
        public bool Died
        {
            get { return died; }
            set { died = value; }
        }

        //Props /\
        public Ship(Vector3 position, ContentManager contentManager, float speed)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            died = false;

            model = new ShipModel();
            model.LoadContent(contentManager);

            foreach (ModelMesh mesh in this.model.Model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
        }

        public void Update(GameTime gameTime)
        {
            position.Z -= speed * gameTime.ElapsedGameTime.Milliseconds;

            //NEW
            if(position.Z <= -ShipPool.deathDist)
            {
                died = true;
            }

            world = Matrix.CreateTranslation(position);

            boundingSphere.Center = position;
        }

        public void Draw(Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
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