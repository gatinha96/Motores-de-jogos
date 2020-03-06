using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MotoresJogosFase1
{
    static class Skybox
    {
        static TextureCube skyBox;
        static Model cube;
        static Effect effect;
        static float size;

        public static void Initialize(float size)
        {
            Skybox.size = size;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            //try
            //{
            //    skyBox = contentManager.Load<TextureCube>("Skybox/CubeMap");
            //}
            //catch (Exception e)
            //{
            //    skyBox = null;
            //    MessageBus.Messages.Add(new ConsoleMessage(e.ToString()));
            //}
            skyBox = contentManager.Load<TextureCube>("Skybox/CubeMap");
            cube = contentManager.Load<Model>("Skybox/Cube");
            effect = contentManager.Load<Effect>("Skybox/Effect");
        }

        public static void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                foreach (ModelMesh mesh in cube.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = effect;
                        part.Effect.Parameters["World"].SetValue(Matrix.CreateScale(size) * Matrix.CreateTranslation(cameraPosition));
                        part.Effect.Parameters["View"].SetValue(view);
                        part.Effect.Parameters["Projection"].SetValue(projection);
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(skyBox);
                        part.Effect.Parameters["CameraPosition"].SetValue(cameraPosition);
                    }
                    mesh.Draw();
                }
            }
        }
    }
}
