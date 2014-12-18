using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    public abstract class AbstractEnemy
    {
        protected Sprite enemySprite;
        protected int hp;
        protected int mp;
        protected int lvl;
        protected float attackRange;
        protected float aggroRange;
        protected float movementSpeed;

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
                 enemySprite.Position = new Vector2f(enemySprite.Position.X + path[i].X,enemySprite.Position.Y + path[i].Y);
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(enemySprite);
        }

        public void update(GameTime gameTime, Vector2f midP)
        {
            movementSpeed *= gameTime.EllapsedTime.Milliseconds;

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
