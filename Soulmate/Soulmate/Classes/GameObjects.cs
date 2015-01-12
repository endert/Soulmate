using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    abstract class GameObjects
    {
        protected Vector2f position;
        protected Sprite sprite;
        protected HitBox hitBox;

        public Sprite getSprite()
        {
            return this.sprite;
        }

        public Vector2f getPosition()
        {
            return this.position;
        }

        public float getWeidth()
        {
            return sprite.Texture.Size.X;
        }

        public float getHeight()
        {
            return sprite.Texture.Size.Y;
        }

         

        virtual public void draw(RenderWindow window)
        {
            window.Draw(sprite);
        }
    }
}
