using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
//using System.Collections.Generic;

namespace MotoresJogosFase1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        Camera camera;

        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferMultiSampling = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 900;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera(new Vector3(0,0,50), graphics);
            random = new Random();
            DebugShapeRenderer.Initialize(GraphicsDevice);

            ShipPool.Initialize();
            MessageBus.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ShipPool.CreateShips(random, Content);

            for (int i = 0; i < 100; i++)
            {
                ShipPool.ActivateOneShip(random, Content);
            }
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            ShipPool.Update(gameTime,random,Content);
            GameConsole.Update();
            MessageBus.Update();
            MemProfiling.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ShipPool.Draw(camera);

            DebugShapeRenderer.Draw(gameTime, camera.View, camera.Projection);

            base.Draw(gameTime);
        }
    }
}