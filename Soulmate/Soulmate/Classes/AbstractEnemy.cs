using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Soulmate.Classes
{
    public abstract class AbstractEnemy
    {
        protected Stopwatch watch = new Stopwatch();    //for animations and the random movement
        protected Sprite enemySprite;
        protected Random random = new Random();

        protected int lvl;
        protected float hp;
        protected float mp;
        protected float attackDamage;
        protected float attackRange;
        protected float def;
        protected float aggroRange;
        protected float movementSpeed;
        
        protected int movingFor = 0;    //moving for millisek in one direction
        protected int randomMovingDirection;

        Vector2f unionUpperLeft;
        Vector2f unionBottomRight;

        //Getter**************************************************************************************
        public Sprite getEnemySprite()
        {
            return enemySprite;
        }
        public Vector2f getPosition()
        {
            return new Vector2f(enemySprite.Position.X+(enemySprite.Texture.Size.X/2), enemySprite.Position.Y+(enemySprite.Texture.Size.Y/2));
        }

        public int getLvl()
        {
            return lvl;
        }
        public float getHp()
        {
            return hp;
        }
        public float getMp()
        {
            return mp;
        }
        public float getAttackDamage()
        {
            return attackDamage;
        }
        public float getAttackRange()
        {
            return attackRange;
        }
        public float getDef()
        {
            return def;
        }
        public float getAggroRange()
        {
            return aggroRange;
        }
        public float getMovementSpeed()
        {
            return movementSpeed;
        }

        public Vector2f[] getHitBox()     
        {
            Vector2f[] hitBox = new Vector2f[2];
            //more isn't necessary
            hitBox[0] = new Vector2f(enemySprite.Position.X, enemySprite.Position.Y); //upponLeft
            hitBox[1] = new Vector2f(enemySprite.Position.X + enemySprite.Texture.Size.X, enemySprite.Position.Y + enemySprite.Texture.Size.Y);//bottomRight

            return hitBox;
        }
        //********************************************************************************************

        //Methods*************************************************************************************
        public void update(GameTime gameTime)
        {
            if (sensePlayer())  //if a player is sensed (is in aggroRange) react else not ;)
            {
                react();
            }
            else
            {
                notReact();
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(enemySprite);
        }

        public bool touchedPlayer()
        {
            union();
            if (((unionBottomRight.X - unionUpperLeft.X) <= ((EnemyHandler.getHitBoxPlayer()[1].X - EnemyHandler.getHitBoxPlayer()[0].X) + (getHitBox()[1].X - getHitBox()[0].X))) &&
                ((unionBottomRight.Y - unionUpperLeft.Y) <= ((EnemyHandler.getHitBoxPlayer()[1].Y - EnemyHandler.getHitBoxPlayer()[0].Y) + (getHitBox()[1].Y - getHitBox()[0].Y))))
                return true;
            else
                return false;
        }

        public void move(Vector2f direction)    //get a direction, and move to it with the enemys' movementspeed 
        {
            Vector2f move = new Vector2f(0, 0);

            if (direction.X > 0)
                move.X += movementSpeed;
            else
            {
                if (direction.X < 0)
                    move.X -= movementSpeed;
                else
                    move.X += 0;
            }
            if (direction.Y > 0)
                move.Y += movementSpeed;
            else
            {
                if (direction.Y < 0)
                    move.Y -= movementSpeed;
                else
                    move.Y += 0;
            }
            // this would do the same:

            // Vector2f move = new Vector2f(((direction.X > 0) ? (movementSpeed) : ((direction.X < 0) ? (-movementSpeed) : (0))), ((direction.Y > 0) ? (movementSpeed) : ((direction.Y < 0) ? (-movementSpeed) : (0))));
            
            if (EnemyHandler.getMap().getWalkable(enemySprite, move))    // only move if it's walkable
                enemySprite.Position = new Vector2f(enemySprite.Position.X + move.X, enemySprite.Position.Y + move.Y);

        }

        private void union()
        {
            unionUpperLeft = new Vector2f(((EnemyHandler.getHitBoxPlayer()[0].X <= getHitBox()[0].X) ? (EnemyHandler.getHitBoxPlayer()[0].X) : (getHitBox()[0].X)), 
                ((EnemyHandler.getHitBoxPlayer()[0].Y <= getHitBox()[0].Y) ? (EnemyHandler.getHitBoxPlayer()[0].Y) : (getHitBox()[0].Y)));

            unionBottomRight = new Vector2f(((EnemyHandler.getHitBoxPlayer()[1].X >= getHitBox()[1].X) ? (EnemyHandler.getHitBoxPlayer()[1].X) : (getHitBox()[1].X)),
                ((EnemyHandler.getHitBoxPlayer()[1].Y >= getHitBox()[1].Y) ? (EnemyHandler.getHitBoxPlayer()[1].Y) : (getHitBox()[1].Y)));

        }

        public void moveRandom()
        {
            Vector2f up = new Vector2f(0, -1);
            Vector2f upRight = new Vector2f(1, -1);
            Vector2f right = new Vector2f(1, 0);
            Vector2f downRight = new Vector2f(1, 1);
            Vector2f down = new Vector2f(0, 1);
            Vector2f downLeft = new Vector2f(-1, 1);
            Vector2f left = new Vector2f(-1, 0);
            Vector2f upLeft = new Vector2f(-1, -1);

            if (watch.ElapsedMilliseconds >= movingFor) //only evaluate a new direction, if the enemy isnt moving already
            {
                randomMovingDirection = random.Next(9);
                watch.Restart();
                movingFor = 0;
            }
            switch (randomMovingDirection)  //move in the direction for 1000 millisecounds so 1 second
            {
                case 0:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, up))
                        move(up);
                    watch.Start();
                    movingFor = (int)(1000*random.NextDouble()) + 500;
                    break;
                case 1:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, upRight))
                        move(upRight);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 2:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, right))
                        move(right);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 3:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, downRight))
                        move(downRight);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 4:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, down))
                        move(down);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 5:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, downLeft))
                        move(downLeft);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 6:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, left))
                        move(left);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 7:
                    if (EnemyHandler.getMap().getWalkable(enemySprite, upLeft))
                        move(upLeft);
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                default:    //stand still for one sek
                    move(new Vector2f(0, 0));
                    watch.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
            }
        }

        public float distancePlayer(Vector2f midP)  //evaluating the distance between the mid of the Player and the enemy
        {
            Vector2f midE = new Vector2f(enemySprite.Position.X + (enemySprite.Texture.Size.X / 2), enemySprite.Position.Y + (enemySprite.Texture.Size.Y / 2));

            float distance = (float)Math.Sqrt(Math.Pow(Math.Abs(midE.X - midP.X), 2) + Math.Pow(Math.Abs(midE.Y - midP.Y), 2));

            return distance;
        }

        public float distancePlayer()
        {
            float distanceX = 0;
            float distanceY = 0;

            distanceX += Math.Min(Math.Min(Math.Abs(getHitBox()[0].X - EnemyHandler.getHitBoxPlayer()[0].X), Math.Abs(getHitBox()[0].X - EnemyHandler.getHitBoxPlayer()[1].X)), 
                Math.Min(Math.Abs(getHitBox()[1].X - EnemyHandler.getHitBoxPlayer()[0].X), Math.Abs(getHitBox()[1].X - EnemyHandler.getHitBoxPlayer()[1].X)));

            distanceY += Math.Min(Math.Min(Math.Abs(getHitBox()[0].Y - EnemyHandler.getHitBoxPlayer()[0].Y), Math.Abs(getHitBox()[0].Y - EnemyHandler.getHitBoxPlayer()[1].Y)),
                Math.Min(Math.Abs(getHitBox()[1].Y - EnemyHandler.getHitBoxPlayer()[0].Y), Math.Abs(getHitBox()[1].Y - EnemyHandler.getHitBoxPlayer()[1].Y)));

            float distance = (float)Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));

            return distance;
        }

        public bool sensePlayer()
        {
            if (distancePlayer() <= aggroRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //********************************************************************************************

        //Abstract************************************************************************************
        public abstract void attack();  //the attack animation an etc.

        public abstract void react();   //what the enemy does if it's sense a player

        public abstract void notReact();    //what the enemy normaly does
    }
}
