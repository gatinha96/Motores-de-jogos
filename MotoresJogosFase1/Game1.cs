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
        SpriteBatch spriteBatch;
        BasicEffect basicEffect;

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

            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
        }

        protected override void Initialize()
        {
            GameManager.Initialize();

            scale = 0.5f;
            testcolor = Color.Black;

            camera = new Camera(Vector3.Zero, graphics, 1f * scale, 5000f * scale);
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
            
            //Skybox.Initialize(5000f * scale / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.View = Matrix.CreateLookAt(new Vector3(50, 50, 50), new Vector3(0, 0, 0), Vector3.Up);
            basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            MenuManager.LoadContent(Content);

            //Skybox.LoadContent(Content);
            skyboxTest = new SkyboxTest(Content, 5000 * scale / 2f);

            BulletModel.LoadContent(Content);
            ShipModel.LoadContent(Content);

            player = new Player(Vector3.Zero, minSpeed * speedMultiplier * scale, new Vector3(0, 0, -1f), 0.005f);
            ShipPool.CreateShips();
            BulletPool.CreateBullets();

            for (int i = 0; i < 100; i++)
            {
                ShipPool.ActivateOneShip();
            }
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            ControlManager.Update();

            if (GameManager.GameState == GameState.InGame)
            {
                //Fire
                if ((inputManager.Clicked(Input.Fire) || inputManager.Clicked(MouseInput.LeftButton)))
                {
                    FireWeapon f = new FireWeapon(player);
                    ControlManager.currentCommands.Add(f);
                }

                //Pause
                if (inputManager.Clicked(Input.Pause))
                {
                    GameManager.GameState = GameState.Pause;
                }

                player.CustomUpdate(gameTime, inputManager);
                ShipPool.Update(gameTime, random);
                BulletPool.Update(gameTime, random);
            }
            else if (GameManager.GameState == GameState.Pause)
            {
                if (inputManager.Clicked(Input.Pause))
                {
                    GameManager.GameState = GameState.InGame;
                }
            }
            else if (GameManager.GameState == GameState.MainMenu)
            {
                if (inputManager.Clicked(Input.Enter))
                {
                    GameManager.GameState = GameState.InGame;
                }
            }
            else if (GameManager.GameState == GameState.Lost)
            {
                if (inputManager.Clicked(Input.Enter))
                {
                    GameManager.GameState = GameState.MainMenu;
                }
            }

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
            basicEffect.CurrentTechnique.Passes[0].Apply();

            if (GameManager.GameState == GameState.InGame) //Game logic
            {
                //Skybox.Draw(camera.View, camera.Projection, camera.Position);
                skyboxTest.Draw(camera.View, camera.Projection, camera.Position);

                ShipPool.Draw(camera);

                //testing purposes
                //foreach (Ship s in ShipPool.ships)
                //{
                //    if (camera.InView(s.BoundingSphere))
                //    {
                //        testWorld(GraphicsDevice, s.World);
                //    }
                //}

                BulletPool.Draw(camera);

                //Draw Player
                player.Draw(camera.View, camera.Projection);
                //Testing Player
                //testWorld(GraphicsDevice, player.World);
            }
            else if (GameManager.GameState == GameState.MainMenu || GameManager.GameState == GameState.Pause || GameManager.GameState == GameState.Lost) //Menu Logic
            {
                MenuManager.Draw(spriteBatch, graphics);
            }

            DebugShapeRenderer.Draw(gameTime, camera.View, camera.Projection);

            base.Draw(gameTime);
        }

        public void testWorld(GraphicsDevice graphicsDevice, Matrix World)
        {
            DrawLine(graphicsDevice, World.Translation, World.Translation + 10 * World.Forward * scale);
            DrawLine(graphicsDevice, World.Translation, World.Translation + 10 * World.Right * scale);
            DrawLine(graphicsDevice, World.Translation, World.Translation + 10 * World.Left * scale);
            DrawLine(graphicsDevice, World.Translation, World.Translation + 10 * World.Backward * scale);
            DrawLine(graphicsDevice, World.Translation, World.Translation + 10 * World.Up * scale);
            DrawLine(graphicsDevice, World.Translation, World.Translation + 10 * World.Down * scale);
        }

        public static void DrawLine(GraphicsDevice graphicsDevice, Vector3 origin, Vector3 endPos)
        {
            var vertices = new[] { new VertexPositionColor(origin, Color.Red), new VertexPositionColor(endPos, Color.Red) };
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
        }
    }
}