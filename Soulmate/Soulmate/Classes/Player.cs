using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Player
    {
        Sprite sprite;

        public Sprite getSprite()
        {
            return sprite;
        }

        public void setSpritePosition(Vector2f pos)
        {
            sprite.Position = pos;
        }

        public void update(GameTime time);

        public void move(Vector2f move)
        {
            
        }
    }
}
