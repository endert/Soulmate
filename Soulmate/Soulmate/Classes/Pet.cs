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
        Vector2f movement;
        
        public Pet(Sprite player)
        {
            sprite = new Sprite(petTexture);
            sprite.Position = new Vector2f(player.Position.X + 200, player.Position.Y+player.Texture.Size.Y-petTexture.Size.Y);
            position = sprite.Position;
            hitBox = new HitBox(sprite.Position, getWidth(), getHeight());
            //movementSpeed = 0.1f;
        }

        override public void update(GameTime time)
        {
            sprite.Position = position;
            hitBox.setPosition(sprite.Position);
            

            //move(EnemyHandler.getPlayer().getSprite());
            //move(new Vector2f(EnemyHandler.getPlayer().getPosition().X-sprite.Position.X, EnemyHandler.getPlayer().getPosition().Y-sprite.Position.Y));

            movementSpeed = 0.2f * (float)time.EllapsedTime.TotalMilliseconds;

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);
        }

        //public void move(Sprite player)
        //{
        //    sprite.Position = new Vector2f(player.Position.X - 200, player.Position.Y);
        //}
    }
}
