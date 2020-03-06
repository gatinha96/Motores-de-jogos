using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MotoresJogosFase1
{
    public class BulletModel
    {
        static private Model model;

        public static Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public static void LoadContent(ContentManager contentManager)
        {
            //CHANGE
            model = contentManager.Load<Model>("rocket");
        }
    }
}
