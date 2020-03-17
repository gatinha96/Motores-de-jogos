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
        private float turn;

        public float Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        Vector3 up;

        public Player(Vector3 position, float speed, Vector3 dir, float turn) : base(position, speed, dir)
        {
            Position = position;
            Speed = speed;
            Dir = dir;
            Dir.Normalize();
            this.turn = turn;
            up = Vector3.Up;

            World = Matrix.Identity;
            //died = false;

            if (Speed == 0 || Dir == Vector3.Zero || Dir == null)
            {
                throw new System.Exception("Speed or direction of player is invalid");
            }

            World *= Matrix.CreateTranslation(Dir);

            //MessageBus.InsertNewMessage(new ConsoleMessage(speed.ToString()));
        }

        public void CustomUpdate(GameTime gameTime, InputManager inputManager)
        {
            Rotate(inputManager, gameTime);
            World *= Matrix.CreateTranslation(Speed * gameTime.ElapsedGameTime.Milliseconds * World.Forward);

            //NEW
            if (Position.Z <= -ShipPool.deathDist)
            {
                //died = true;
                Position = Vector3.Zero;
                World *= Matrix.CreateTranslation(Vector3.Zero);
                MessageBus.InsertNewMessage(new ConsoleMessage("Player died, reseted pos"));
            }

            //kill ships we collide with
            foreach (Ship s in ShipPool.ships)
            {
                if (BoundingSphere.Intersects(s.BoundingSphere))
                {
                    s.Died = true;
                }
            }

            SetBoundingSphereCenter(Position);

            //MessageBus.InsertNewMessage(new ConsoleMessage(position.ToString() + " " + speed.ToString() + " " + dir.ToString()));
        }

        public void Rotate(InputManager inputManager, GameTime gameTime)
        {
            if (inputManager.IsPressed(Input.Forward))
            {
                //World *= Matrix.CreateFromAxisAngle(World.Right, -turn);
                World *= Matrix.CreateFromAxisAngle(Vector3.Right, -turn);
            }
            else if (inputManager.IsPressed(Input.Backward))
            {
                //World *= Matrix.CreateFromAxisAngle(World.Right, turn);
                World *= Matrix.CreateFromAxisAngle(Vector3.Right, turn);
            }

            if (inputManager.IsPressed(Input.Left))
            {
                //World *= Matrix.CreateFromAxisAngle(World.Up, turn);
                World *= Matrix.CreateFromAxisAngle(Vector3.Up, turn);
            }
            else if (inputManager.IsPressed(Input.Right))
            {
                // World *= Matrix.CreateFromAxisAngle(World.Up, -turn);
                World *= Matrix.CreateFromAxisAngle(Vector3.Up, -turn);
            }
        }
    }
}