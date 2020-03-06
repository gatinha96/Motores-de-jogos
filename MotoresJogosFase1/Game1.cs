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
        Player player;
        SkyboxTest SkyboxTest;

        public static Random random;

        public static float scale;

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
            scale = 0.5f;

            camera = new Camera(new Vector3(0, 0, 50), graphics, 1f * scale, 5000f * scale);
            random = new Random();
            DebugShapeRenderer.Initialize(GraphicsDevice);

            player = new Player(Vector3.Zero, Content, 0.01f * scale);

            ShipPool.Initialize(1000 * scale, 100 * scale, 5, 1, 0.01f);
            BulletPool.Initialize();
            MessageBus.Initialize();
            //Skybox.Initialize(5000f * scale / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Skybox.LoadContent(Content);
            SkyboxTest = new SkyboxTest(Content);
            BulletModel.LoadContent(Content);
            BulletPool.CreateBullets();

            ShipPool.CreateShips(Content);

            for (int i = 0; i < 100; i++)
            {
                ShipPool.ActivateOneShip(Content);
            }
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            Command c = ControlManager.Update();
            if (c != null)
            {
                //INSERT PLAYER
                //c.Execute();
            }

            player.Update(gameTime);
            ShipPool.Update(gameTime,random,Content);
            BulletPool.Update(gameTime, random, Content);

            GameConsole.Update();
            MessageBus.Update();
            MemProfiling.Update();

            camera.Update(player.World, 2 * scale, 0.2f * scale);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Skybox.Draw(camera.View, camera.Projection, camera.Position);
            SkyboxTest.Draw(camera.View, camera.Projection, camera.Position);
            ShipPool.Draw(camera);
            BulletPool.Draw(camera);

            DebugShapeRenderer.Draw(gameTime, camera.View, camera.Projection);

            base.Draw(gameTime);
        }
    }
}