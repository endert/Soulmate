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
        LifePet lifePet; 

        public Interface()
        {
            life = new Life();
            lifePet = new LifePet();
        }

        public void update(GameTime gameTime)
        {
            life.update(gameTime);
            lifePet.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            life.draw(window);
            lifePet.draw(window);
        }
    }
}
