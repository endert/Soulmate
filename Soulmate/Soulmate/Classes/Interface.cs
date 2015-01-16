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
        Texture lifeTexture = new Texture("Pictures/LifeFull.png");
        Sprite lifeSprite;

        public Interface()
        {
            lifeSprite = new Sprite(lifeTexture);
            lifeSprite.Position = getLifeSpritePosition();
        }

        public Vector2f getLifeSpritePosition()
        {
            return new Vector2f(((ObjectHandler.player.getPosition().X + (ObjectHandler.player.getWidth() / 2)) - 620),
                                 (ObjectHandler.player.getPosition().Y + (ObjectHandler.player.getHeight() / 2)) + 320);
        }

        public void update(GameTime gameTime)
        {
            lifeSprite.Position = getLifeSpritePosition();
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifeSprite);
        }
    }
}
