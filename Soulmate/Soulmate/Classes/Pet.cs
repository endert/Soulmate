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

            move(getVectorForMove());

            if (petPlayerCollision())
            {
                if (ObjectHandler.player.getFacingDirection().X < 0)
                    position.X++;

                if (ObjectHandler.player.getFacingDirection().X > 0)
                    position.X--;

                else
                    position.X++;
            }

            hitFromDirections.Clear();
        }

        public bool isBehindPlayer()
        {
            switch (ObjectHandler.player.getNumFacingDirection())
            {
                case(0):
                    if (position.Y+getHeight()<ObjectHandler.player.getPosition().Y)
                    {
                        return true;
                    }
                    break;
                case(1):
                    if (position.Y>ObjectHandler.player.getPosition().Y+ObjectHandler.player.getHeight())
                    {
                        return true;
                    }
                    break;
                case(2):
                    if (position.X+getWidth()<ObjectHandler.player.getPosition().X)
                    {
                        return true;
                    }
                    break;
                case(3):
                    if (position.X>ObjectHandler.player.getPosition().X+ObjectHandler.player.getWidth())
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        public Vector2f getVectorForMove()
        {
            if (isBehindPlayer())
            {
                return getPlayerDirection();
            }
            else
            {
                return new Vector2f(-ObjectHandler.player.getFacingDirection().X, -ObjectHandler.player.getFacingDirection().Y);
            }
        }

        public Vector2f getPlayerDirection()
        {
            Vector2f playerDirection = new Vector2f(0, 0);
            if (ObjectHandler.player.getHitBox().getPosition().X + ObjectHandler.player.getHitBox().getWidth() < position.X) //player is to the left
            {
                playerDirection.X = -1;
            }
            else if (position.X + hitBox.getWidth() < ObjectHandler.player.getHitBox().getPosition().X)
            {
                playerDirection.X = 1;
            }

            if (ObjectHandler.player.getHitBox().getPosition().Y + ObjectHandler.player.getHitBox().getHeight() < position.Y)
            {
                playerDirection.Y = -1;
            }
            else if (position.Y + getHeight() < ObjectHandler.player.getHitBox().getPosition().Y)
            {
                playerDirection.Y = 1;
            }
            return playerDirection;
        }
    }
}
