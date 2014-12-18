using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Particle
    {
        private Texture texture;
        private Sprite sprite;

        public Particle(Vector2f position)
        {
            sprite = new Sprite(texture);
            sprite.Position = position;

        }

        public void loadContent()
        {
            texture = new Texture("Pictures/Snow/flake.png");
        }

        public void update(GameTime gameTime)
        {

        }

        public void draw(RenderWindow window)
        {

        }
    }
}
