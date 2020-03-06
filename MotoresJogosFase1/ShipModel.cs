using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlienGrab;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MotoresJogosFase1
{
    class ShipModel
    {
        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public void LoadContent(ContentManager contentManager)
        {
            model = contentManager.Load<Model>("p1_saucer");
        }
    }
}
