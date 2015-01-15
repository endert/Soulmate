using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        protected Stopwatch watchInvulnerablety = new Stopwatch();    //for Invulnerablety

        protected HitBox hitBox;
        protected List<Vector2f> hitFromDirections = new List<Vector2f>();

        protected bool isAlive = true;
        protected bool tookDmg { get; set; }
        protected int invulnerableFor = 500; //0.5s invulnerable
        protected float knockBack = 150f;

        protected Vector2f facingInDirection { get; set; }
        protected bool moveAwayFromEntity = false;
        protected int indexEntityList;  //index in the ObjectList of the ObjectHandler
        protected float movementSpeed { get; set; }

        public void setIndexEntityList(int index)
        {
            indexEntityList = index;
        }

        public Vector2f getFacingDirection()
        {
            return facingInDirection;
        }

        public bool getIsAlive()
        {
            return isAlive;
        }

        public float getKnockBack()
        {
            return knockBack;
        }

        public Sprite getSprite()
        {
            return this.sprite;
        }

        public HitBox getHitBox()
        {
            return hitBox;
        }

        public List<String> getTypeFromTouchedEntities()
        {
            List<String> types = new List<string>();
            for (int i = 0; i < ObjectHandler.gObjs.Count; i++)
            {
                if ((i != indexEntityList) && (hitBox.hit(ObjectHandler.gObjs[i].getHitBox())))
                {
                    types.Add(ObjectHandler.gObjs[i].type);
                }
            }
            return types;
        }

        virtual public Vector2f getPosition()
        {
            return this.position;
        }

        public void kill()
        {
            isAlive = false;
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
                            direction.X = -hitFromDirections[i].X;
                        }

                        if (Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) > Math.Abs(direction.Y - hitFromDirections[i].Y) ||
                            Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) < Math.Abs(direction.Y - hitFromDirections[i].Y))
                        {
                            direction.Y = -hitFromDirections[i].Y;
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
                        facingInDirection = movement;

                    }
                }
            }
        }

        public bool hitAnotherEntity()
        {
            for (int i = 0; i < ObjectHandler.gObjs.Count; i++)
            {
                if ((i != indexEntityList) && (hitBox.hit(ObjectHandler.gObjs[i].getHitBox())) && !petPlayerCollision())
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

        public bool petPlayerCollision()
        {
            if (type.Equals("pet"))
            {
                foreach (GameObjects gObj in ObjectHandler.gObjs)
                {
                    if (hitBox.hit(gObj.hitBox) && !gObj.type.Equals("pet") && gObj.type.Equals("player"))
                    {
                        return true;
                    }
                }
            }

            if (type.Equals("player"))
            {
                foreach (GameObjects gObj in ObjectHandler.gObjs)
                {
                    if (hitBox.hit(gObj.hitBox)  && !gObj.type.Equals("player") && gObj.type.Equals("pet"))
                    {
                        return true;
                    }
                }
            }

            return false;
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

        public void knockedBack(Vector2f direction, float knockBack)
        {
            Vector2f knocking = new Vector2f(0, 0);

            if (direction.X > 0)
                knocking.X += knockBack;
            else
            {
                if (direction.X < 0)
                    knocking.X -= knockBack;
                else
                    knocking.X += 0;
            }
            if (direction.Y > 0)
                knocking.Y += knockBack;
            else
            {
                if (direction.Y < 0)
                    knocking.Y -= knockBack;
                else
                    knocking.Y += 0;
            }

            if (ObjectHandler.lvlMap.getWalkable(sprite, knocking))    // only move if it's walkable
            {
                position = new Vector2f(position.X + knocking.X, position.Y + knocking.Y);
            }
        }

        public Vector2f getKeyPressed(float movementSpeed)
        {
            Vector2f result = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                result.X = -movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                result.Y = -movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                result.Y = movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                result.X = movementSpeed;

            return result;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(sprite);
        }

        public bool isVulnerable()
        {
            bool vulnerable = true;
            if (tookDmg)
            {
                vulnerable = false;
                watchInvulnerablety.Start();
                if (watchInvulnerablety.ElapsedMilliseconds>=invulnerableFor)
                {
                    tookDmg = false;
                    vulnerable = true;
                    watchInvulnerablety.Reset();
                }
            }
            return vulnerable;
        }

        public void finalize()
        {
            hitFromDirections.Clear();
        }

        public void animate(Texture[] textureArray)
        {
            if (isVulnerable())
            {
                if (facingInDirection.Y > 0)
                {
                    sprite = new Sprite(textureArray[0]); //front
                }
                else if (facingInDirection.Y < 0)
                {
                    sprite = new Sprite(textureArray[1]); // back
                }
                else if (facingInDirection.X > 0)
                {
                    sprite = new Sprite(textureArray[2]); // right
                }
                else
                {
                    sprite = new Sprite(textureArray[3]); // left
                }
            }
            else
            {
                if (textureArray.Length>4)
                {
                    sprite = new Sprite(textureArray[4]); // invulnerablety
                }
            }
        }

        public abstract void update(GameTime gameTime);
    }
}
