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
            lifeSprite.Position = new Vector2f(((EnemyHandler.getPlayer().getPosition().X + (EnemyHandler.getPlayer().getWidth() / 2)) - 620), (EnemyHandler.getPlayer().getPosition().Y + (EnemyHandler.getPlayer().getHeight() / 2)) + 320);
        }

        public void update(GameTime gameTime)
        {
            lifeSprite.Position = new Vector2f(((EnemyHandler.getPlayer().getPosition().X + (EnemyHandler.getPlayer().getWidth() / 2)) - 620), (EnemyHandler.getPlayer().getPosition().Y + (EnemyHandler.getPlayer().getHeight() / 2)) + 320);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifeSprite);
        }
    }
}
