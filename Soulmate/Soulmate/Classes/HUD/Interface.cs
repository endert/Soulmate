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
        LifePlayer lifePlayer;

        public Interface()
        {
            lifePlayer = new LifePlayer();
        }

        public void update(GameTime gameTime)
        {
            lifePlayer.update(gameTime);
        }

        public void draw(RenderWindow window)
        {
            lifePlayer.draw(window);
        }
    }
}
