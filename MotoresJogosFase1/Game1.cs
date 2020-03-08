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
        InputManager inputManager;

        Camera camera;
        public static Player player;
        SkyboxTest skyboxTest;

        public static Random random;

        public static float scale;

        public static Color testcolor;

        float speedMultiplier;
        int maxSpeed, minSpeed;

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
            testcolor = Color.Black;

            camera = new Camera(Vector3.Zero, graphics, 1f * scale, 5000f * scale);//old farplane was 5k
            random = new Random();

            maxSpeed = 5;
            minSpeed = 1;
            speedMultiplier = 0.02f;

            MessageBus.Initialize();
            inputManager = new InputManager();
            inputManager.Initialize();
            ControlManager.Initialize();
            DebugShapeRenderer.Initialize(GraphicsDevice);

            ShipPool.Initialize(1000 * scale, 100 * scale, maxSpeed, minSpeed, speedMultiplier * scale);
            BulletPool.Initialize(0.2f * scale);
            player = new Player(Vector3.Zero, minSpeed * speedMultiplier * scale, new Vector3(0, 0, -1f));
            
            //Skybox.Initialize(5000f * scale / 2);

            ShipPool.CreateShips();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Skybox.LoadContent(Content);
            skyboxTest = new SkyboxTest(Content, 5000 * scale / 2f);

            BulletModel.LoadContent(Content);
            ShipModel.LoadContent(Content);

            player.LoadContent();
            ShipPool.LoadContent();
            BulletPool.CreateBullets();

            for (int i = 0; i < 100; i++)
            {
                ShipPool.ActivateOneShip();
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

            ControlManager.Update();

            player.Update(gameTime);
            ShipPool.Update(gameTime,random);
            BulletPool.Update(gameTime, random);

            GameConsole.Update();
            MessageBus.Update();
            MemProfiling.Update();

            //first parameter is player.World, change
            camera.Update(player.World, 10 * scale, 2f * scale);

            inputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(testcolor);

            //Skybox.Draw(camera.View, camera.Projection, camera.Position);
            skyboxTest.Draw(camera.View, camera.Projection, camera.Position);

            ShipPool.Draw(camera);
            BulletPool.Draw(camera);

            player.Draw(camera.View, camera.Projection);

            DebugShapeRenderer.Draw(gameTime, camera.View, camera.Projection);

            base.Draw(gameTime);
        }
    }
}