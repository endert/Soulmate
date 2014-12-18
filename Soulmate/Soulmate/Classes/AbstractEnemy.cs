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

        public void move(Vector2f[] path)
        {
            for (int i = 0; i < path.Length; i++)
            {
                move(path[i]);
            }
        }

        public void move(Vector2f move)
        {
            enemySprite.Position = new Vector2f(enemySprite.Position.X + move.X, enemySprite.Position.Y + move.Y);
        }

        //Its an idea but it doesnt work just like I want it to
        //public void move(Vector2f move)
        //{
        //    int moveCountX = (int)(move.X / movementSpeed);
        //    int moveCountY = (int)(move.Y / movementSpeed);

        //    //normaly I write comments in english but im tiered and its easier to explain in german:
        //    //entschuldigung für den ()?():() aber es wird sonst viiiieeeeel umständlicher, es über
        //    //prüft lediglich ob man öfters nach X gehen muss oder nach Y, es wählt dann das größere
        //    for (int i = 0; i < ((moveCountX >= moveCountY) ? (moveCountX) : (moveCountY)); i++)
        //    {
        //        //hier ist es einfach das wenn noch in x/y richtung gegangen werden muss wird dies getan, ansonsten wird um 0 erhöht in die jeweilige richtung
        //        enemySprite.Position = new Vector2f(enemySprite.Position.X + ((i < moveCountX) ? (movementSpeed) : (0)), enemySprite.Position.Y + ((i < moveCountY) ? (movementSpeed) : (0)));
        //    }

        //}

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
            movementSpeed = _movementSpeed * gameTime.EllapsedTime.Milliseconds;

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
