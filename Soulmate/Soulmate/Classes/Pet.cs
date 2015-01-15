﻿using SFML.Graphics;
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
            type = "pet";
            sprite = new Sprite(petTexture);
            sprite.Position = new Vector2f(player.Position.X - 150, player.Position.Y+player.Texture.Size.Y-petTexture.Size.Y);
            position = sprite.Position;
            hitBox = new HitBox(sprite.Position, getWidth(), getHeight());
        }

        override public void update(GameTime time)
        {
            sprite.Position = position;
            hitBox.setPosition(sprite.Position);

            movementSpeed = 0.2f * (float)time.EllapsedTime.TotalMilliseconds;

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            if (petPlayerCollision())
            {
                if(ObjectHandler.player.getFacingDirection().X<0)
                    position.X++;

                if (ObjectHandler.player.getFacingDirection().X > 0)
                    position.X--;

                else
                    position.X++;
            }

            hitFromDirections.Clear();
        }

        public Vector2f getVectorForMove()
        {
            
            
            return new Vector2f(0, 0);
        }
    }
}
