using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MotoresJogosFase1
{
    public class Camera
    {
        bool free;

        private Matrix view;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Matrix projection;

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera(Vector3 position, GraphicsDeviceManager graphics, float nearPlane, float farPlane)
        {
            free = false;
            view = Matrix.CreateLookAt(position, Vector3.Forward, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50f), graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight, nearPlane, farPlane);
        }

        public void Follow(Matrix target, float dist, float up)
        {
            position = target.Translation + target.Backward * dist + target.Up * up;
            view = Matrix.CreateLookAt(position, target.Translation, Vector3.Up);
        }

        public void Update(Matrix target, float dist, float up)
        {
            KeyManager();
            if(!free)
            {
                Follow(target, dist, up);
            }
            else
            {
                Movement();
            }
        }

        void Movement()
        {
            //CHANGE
            free = false;
        }

        void KeyManager()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                free = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                free = false;
            }
        }
    }
}