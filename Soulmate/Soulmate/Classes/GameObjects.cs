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
        protected String type = "object";
        protected Vector2f position;
        protected Sprite sprite;

        protected HitBox hitBox;
        protected List<Vector2f> hitFromDirections = new List<Vector2f>();

        protected bool isAlive;
        protected float knockBack = 0.1f;

        protected Vector2f movementInDirection { get; set; }
        protected bool moveAwayFromEntity = false;
        protected int indexEntityList;  //index in the ObjectList of the ObjectHandler
        protected float movementSpeed;

        public void setIndexEntityList(int index)
        {
            indexEntityList = index;
        }

        public Sprite getSprite()
        {
            return this.sprite;
        }

        public HitBox getHitBox()
        {
            return hitBox;
        }

        virtual public Vector2f getPosition()
        {
            return this.position;
        }

        public float getWidth()
        {
            return sprite.Texture.Size.X;
        }

        public float getHeight()
        {
            return sprite.Texture.Size.Y;
        }

        virtual public void move(Vector2f direction)    //get a direction, and move to it with the enemys' movementspeed only left,right,up,down and diagonal don't wanna implements sin/cos just now
        {
            if (!direction.Equals(new Vector2f(0, 0)))
            {
                if (hitAnotherEntity() && !moveAwayFromEntity && ((type.Equals("player"))?(true):(!touchedPlayer()))) //if an entity is not a player it should not touch the player
                {
                    moveAwayFromEntity = true;

                    for (int i = 0; i < hitFromDirections.Count; i++)
                    {
                        if (Math.Abs((direction.X >= hitFromDirections[i].X) ? (direction.X) : (hitFromDirections[i].X)) > Math.Abs(direction.X - hitFromDirections[i].X) ||
                            Math.Abs((direction.X >= hitFromDirections[i].X) ? (direction.X) : (hitFromDirections[i].X)) < Math.Abs(direction.X - hitFromDirections[i].X))//if they have the same sign otherwise it doesn't matter
                        {
                            direction.X = knockBack*(-hitFromDirections[i].X/Math.Abs(hitFromDirections[i].X));
                        }

                        if (Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) > Math.Abs(direction.Y - hitFromDirections[i].Y) ||
                            Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) < Math.Abs(direction.Y - hitFromDirections[i].Y))
                        {
                            direction.Y = knockBack*(-hitFromDirections[i].Y/Math.Abs(hitFromDirections[i].Y));
                        }
                    }
                    move(direction);
                }
                else
                {
                    moveAwayFromEntity = false;
                    Vector2f movement = new Vector2f(0, 0);

                    if (direction.X > 0)
                        movement.X += movementSpeed;
                    else
                    {
                        if (direction.X < 0)
                            movement.X -= movementSpeed;
                        else
                            movement.X += 0;
                    }
                    if (direction.Y > 0)
                        movement.Y += movementSpeed;
                    else
                    {
                        if (direction.Y < 0)
                            movement.Y -= movementSpeed;
                        else
                            movement.Y += 0;
                    }

                    if (ObjectHandler.lvlMap.getWalkable(sprite, movement))    // only move if it's walkable
                    {
                        position = new Vector2f(position.X + movement.X, position.Y + movement.Y);
                        movementInDirection = movement;

                    }
                }
            }
        }

        private bool hitAnotherEntity()
        {
            for (int i = 0; i < ObjectHandler.gObjs.Count; i++)
            {
                if ((i != indexEntityList) && (hitBox.hit(ObjectHandler.gObjs[i].getHitBox())))
                {
                    bool notFound = true;
                    for (int j = 0; j < hitFromDirections.Count; j++)
                    {
                        if (hitFromDirections[j].Equals(hitBox.hitFrom(ObjectHandler.gObjs[i].getHitBox())))
                        {
                            notFound = false;
                        }
                    }
                    if (notFound)
                    {
                        hitFromDirections.Add(hitBox.hitFrom(ObjectHandler.gObjs[i].getHitBox()));
                    }
                }
            }
            if (hitFromDirections.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool touchedPlayer()
        {
            if (hitBox.hit(ObjectHandler.player.getHitBox()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2f getKeyPressed(float movementSpeed)
        {
            Vector2f result = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                result.X = -movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                result.Y = -movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                result.Y = movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                result.X = movementSpeed;

            return result;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(sprite);
        }

        public abstract void update(GameTime gameTime);
    }
}
