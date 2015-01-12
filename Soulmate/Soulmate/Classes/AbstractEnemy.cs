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
        protected List<Vector2f> hitFromDirections = new List<Vector2f>();

        protected bool hitPlayer = false;
        protected bool isAlive;

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

        protected HitBox hitBox;
        protected int index;

        private bool moveAwayFromEnemy = false;

        //Getter**************************************************************************************
        public Sprite getEnemySprite()
        {
            return enemySprite;
        }
        public Vector2f getPosition()
        {
            return new Vector2f(enemySprite.Position.X+(enemySprite.Texture.Size.X/2), enemySprite.Position.Y+(enemySprite.Texture.Size.Y/2));
        }

        public bool getIsAlive()
        {
            return isAlive;
        }

        public bool getTochedPlayer()
        {
            return hitPlayer;
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

        public HitBox getHitBox()
        {
            return hitBox;
        }
        //********************************************************************************************

        //Methods*************************************************************************************
        public void update(GameTime gameTime)
        {
            if (isAlive)
            {
                hitBox.setPosition(enemySprite.Position);
                if (sensePlayer())  //if a player is sensed (is in aggroRange) react else not ;)
                {
                    react();
                }
                else
                {
                    notReact();
                }
            }
            hitFromDirections.Clear();
        }

        public void draw(RenderWindow window)
        {
            window.Draw(enemySprite);
        }

        public bool touchedPlayer()
        {
            if (hitBox.hit(EnemyHandler.getHitBoxPlayer()))
            {
                hitPlayer = true;
                return hitPlayer;
            }
            else
            {
                hitPlayer = false;
                return hitPlayer;
            }
        }

        public void move(Vector2f direction)    //get a direction, and move to it with the enemys' movementspeed only left,right,up,down and diagonal don't wanna implements sin/cos just now
        {
            if (hitAnotherEnemy() && !moveAwayFromEnemy && !hitPlayer)
            {
                moveAwayFromEnemy = true;

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
            else if(!hitPlayer)
            {
                moveAwayFromEnemy = false;
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
        }

        private bool hitAnotherEnemy()
        {
            for (int i = 0; i < EnemyHandler.getEnemies().Count; i++)
            {
                if ((i!=index)&&(hitBox.hit(EnemyHandler.getEnemies()[i].getHitBox())))
                {
                    bool notFound = true;
                    for (int j = 0; j < hitFromDirections.Count; j++)
                    {
                        if (hitFromDirections[j].Equals(hitBox.hitFrom(EnemyHandler.getEnemies()[i].getHitBox())))
                        {
                            notFound = false;
                        }
                    }
                    if (notFound)
                        hitFromDirections.Add(hitBox.hitFrom(EnemyHandler.getEnemies()[i].getHitBox()));
                }
            }
            if (hitFromDirections.Count > 0)
            {
                return true;
            }
            else
                return false;
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

        public float distancePlayer()
        {
            float distanceX = 0;
            float distanceY = 0;

            distanceX += Math.Min(Math.Abs((hitBox.getPosition().X + hitBox.getWidth()) - EnemyHandler.getHitBoxPlayer().getPosition().X),  //if the enemy is left of the player it checks the distance from the enemy's right side to the player's left 
                Math.Abs(hitBox.getPosition().X - (EnemyHandler.getHitBoxPlayer().getPosition().X + EnemyHandler.getHitBoxPlayer().getWidth())));

            distanceY += Math.Min(Math.Abs((hitBox.getPosition().Y + hitBox.getHeight()) - EnemyHandler.getHitBoxPlayer().getPosition().Y),
                Math.Abs(hitBox.getPosition().Y - (EnemyHandler.getHitBoxPlayer().getPosition().Y + EnemyHandler.getHitBoxPlayer().getHeight())));

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
