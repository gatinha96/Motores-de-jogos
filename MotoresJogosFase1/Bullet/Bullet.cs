using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MotoresJogosFase1
{
    class Bullet
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

        private Vector3 dir;

        public Vector3 Dir
        {
            get { return dir; }
            set { dir = value; }
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

        //Props
        public Bullet(Vector3 position, float speed, Vector3 dir)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = speed;
            this.dir = dir;
            this.dir.Normalize();
            died = false;

            model = BulletModel.Model;

            foreach (ModelMesh mesh in model.Meshes)
            {
                boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
        }

        public void Update(GameTime gameTime)
        {
            position += speed * gameTime.ElapsedGameTime.Milliseconds * dir;

            //NEW
            if(position.Z <= -1000 || position.Z >= 1000)
            {
                died = true;
            }

            world = Matrix.CreateTranslation(position);

            boundingSphere.Center = position;
        }

        public void Draw(Matrix View, Matrix Projection)
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

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }

        //NEW
        public void Respawn(Vector3 position, Vector3 dir)
        {
            died = false;
            this.position = position;
            this.dir = dir;
        }
    }
}