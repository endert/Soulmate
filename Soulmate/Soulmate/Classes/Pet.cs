using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Pet : GameObjects
    {
        Texture petTexture = new Texture("Pictures/Pet/WolfSeiteLinks.png");
        
        public Pet(Sprite player)
        {
            sprite = new Sprite(petTexture);
            sprite.Position = new Vector2f(player.Position.X - 100, player.Position.Y);
            hitBox = new HitBox(sprite.Position, getWidth(), getHeight());
            movementSpeed = 0.1f;
        }

        override public void update(GameTime time)
        {
            hitBox.setPosition(sprite.Position);
            sprite.Position = position;

            //move(EnemyHandler.getPlayer().getSprite());
            //move(new Vector2f(EnemyHandler.getPlayer().getPosition().X-sprite.Position.X, EnemyHandler.getPlayer().getPosition().Y-sprite.Position.Y));
            move(getKeyPressed(movementSpeed));
        }

        public void move(Sprite player)
        {
            sprite.Position = new Vector2f(player.Position.X - 200, player.Position.Y);
        }
    }
}
