using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Interface
    {
        Life life;

        public Interface()
        {
            life = new Life();
        }

        public void update(GameTime gameTime)
        {
            life.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            life.draw(window);
        }
    }
}
