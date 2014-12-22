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
        protected Stopwatch watch = new Stopwatch();
        protected Sprite enemySprite;
        protected int hp;
        protected int mp;
        protected int lvl;
        protected float attackRange;
        protected float aggroRange;
        protected float movementSpeed;
        protected float _movementSpeed;
        protected bool waiting = false;
        protected int isWaitingFor;

        //getter
        public Sprite getEnemySprite()
        {
            return enemySprite;
        }

        public int getHp()
        {
            return hp;
        }
        public int getMp()
        {
            return mp;
        }
        public int getLvl()
        {
            return lvl;
        }
        public float getAttackRange()
        {
            return attackRange;
        }
        public float getAggroRange()
        {
            return aggroRange;
        }
        public float getMovementSpeed()
        {
            return movementSpeed;
        }

        //public void move(List<Vector2f> path)
        //{
        //    for (int i = 0; i < path.Count; i++)
        //    {
        //        move(path[i]);
        //    }
        //}

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

            enemySprite.Position = new Vector2f(enemySprite.Position.X + move.X, enemySprite.Position.Y + move.Y);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(enemySprite);
        }

        public void wait(int t)
        {
            waiting = true;
            isWaitingFor = t;
            watch.Start();
            isWaiting();
        }

        public bool isWaiting()
        {
            if (watch.ElapsedMilliseconds>=isWaitingFor)
            {
                waiting = false;
                watch.Reset();
                watch.Stop();
            }
            return waiting;
        }

        public void update(GameTime gameTime, Vector2f midP)
        {
            movementSpeed = _movementSpeed;

            if (sensePlayer(midP))
            {
                react();
            }
            else
            {
                notReact();
            }
        }

        public abstract void attack();

        public abstract void react();

        public abstract void notReact();

        public float distancePlayer(Vector2f midP)
        {
            Vector2f midE = new Vector2f(enemySprite.Position.X + (enemySprite.Texture.Size.X / 2), enemySprite.Position.Y + (enemySprite.Texture.Size.Y / 2));

            float distance = (float)Math.Sqrt(Math.Pow(Math.Abs(midE.X - midP.X), 2) + Math.Pow(Math.Abs(midE.Y - midP.Y), 2));

            return distance;
        }

        public bool sensePlayer(Vector2f midP)
        {
            if (distancePlayer(midP) <= aggroRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      
    }
}
