using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Life
    {
        Texture[] lifeHeartTexture = { new Texture("Pictures/Life/LifeHeartZero.png"), new Texture("Pictures/Life/LifeHeartOne-Quarter.png"), new Texture("Pictures/Life/LifeHeartHalf.png"), 
                                   new Texture("Pictures/Life/LifeHeartThree-Quarters.png"), new Texture("Pictures/Life/LifeHeartFull.png") };
        List<Sprite> lifeHeartSprite = new List<Sprite>();

        public Life()
        {

        }

        public void update(GameTime gameTime)
        {

        }

        public void draw(RenderWindow window)
        {
            foreach (Sprite sprite in lifeHeartSprite)
            {
                window.Draw(sprite);
            }
        }
    }
}
