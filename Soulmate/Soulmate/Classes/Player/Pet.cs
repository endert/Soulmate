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

            movementSpeed = 0.4f * (float)time.EllapsedTime.TotalMilliseconds;
            movement = getVectorForMove();
            move(movement);

            hitFromDirections.Clear();
        }

        public bool isBehindPlayer()
        {
            Vector2f playerMovingDirection = ObjectHandler.player.getFacingDirection();
            bool behindX = false;   // for diagonal moving;
            bool behindY = false;

            if (!(playerMovingDirection.X != 0 && playerMovingDirection.Y != 0))
            {
                switch (ObjectHandler.player.getNumFacingDirection())
                {
                    case (0):
                        if (position.Y + getHeight() <= ObjectHandler.player.getPosition().Y)
                        {
                            return true;
                        }
                        break;
                    case (1):
                        if (position.Y >= ObjectHandler.player.getPosition().Y + ObjectHandler.player.getHeight())
                        {
                            return true;
                        }
                        break;
                    case (2):
                        if (position.X + getWidth() <= ObjectHandler.player.getPosition().X)
                        {
                            return true;
                        }
                        break;
                    case (3):
                        if (position.X >= ObjectHandler.player.getPosition().X + ObjectHandler.player.getWidth())
                        {
                            return true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else      // moving diagonal
            {
                if (playerMovingDirection.X > 0)
                {
                    if (position.X + getWidth() <= ObjectHandler.player.getPosition().X)
                    {
                        behindX = true;
                    }
                }
                else if (playerMovingDirection.X < 0)
                {
                    if (position.X >= ObjectHandler.player.getPosition().X + ObjectHandler.player.getWidth())
                    {
                        behindX = true;
                    }
                }

                if (playerMovingDirection.Y > 0)
                {
                    if (position.Y + getHeight() <= ObjectHandler.player.getPosition().Y)
                    {
                        behindY = true;
                    }
                }
                else if (playerMovingDirection.Y < 0)
                {
                    if (position.Y >= ObjectHandler.player.getPosition().Y + ObjectHandler.player.getHeight())
                    {
                        behindY = true;
                    }
                } 
                
                return (behindX || behindY);
            }
            return false;
        }

        public Vector2f getVectorForMove()
        {
            if (ObjectHandler.player.isMoving)
            {
                if (isBehindPlayer())
                {
                    return getPlayerDirection();
                }
                else if (!(ObjectHandler.player.getFacingDirection().X != 0 && ObjectHandler.player.getFacingDirection().Y != 0))    //If player dont facing diagonal
                {
                    return new Vector2f(-ObjectHandler.player.getFacingDirection().X, -ObjectHandler.player.getFacingDirection().Y);
                }
                else
                {
                    switch (ObjectHandler.player.getNumFacingDirection())
                    {
                        case 0:
                            return new Vector2f(0, -1);
                        case 1:
                            return new Vector2f(0, 1);
                        case 2:
                            return new Vector2f(-1, 0);
                        case 3:
                            return new Vector2f(1, 0);
                        default:
                            return new Vector2f(0, 0);
                    }
                }
            }
            else
            {
                if (isBehindPlayer()&& hitBox.distanceTo(ObjectHandler.player.getHitBox())>=50f)
                {
                    return getPlayerDirection();
                }
                else if (!isBehindPlayer())    //If player dont facing diagonal
                {
                    if (!(ObjectHandler.player.getFacingDirection().X != 0 && ObjectHandler.player.getFacingDirection().Y != 0))    //If player dont facing diagonal
                    {
                        return new Vector2f(-ObjectHandler.player.getFacingDirection().X, -ObjectHandler.player.getFacingDirection().Y);
                    }
                    else
                    {
                        switch (ObjectHandler.player.getNumFacingDirection())
                        {
                            case 0:
                                return new Vector2f(0, -1);
                            case 1:
                                return new Vector2f(0, 1);
                            case 2:
                                return new Vector2f(-1, 0);
                            case 3:
                                return new Vector2f(1, 0);
                            default:
                                return new Vector2f(0, 0);
                        }
                    }
                }
                else
                {
                    return new Vector2f(0, 0);
                }
            }
        }
    }
}
