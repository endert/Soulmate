﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Soulmate.Classes
{
    abstract class AbstractEnemy : GameObjects
    {
        protected Stopwatch watch1 = new Stopwatch();    //random movement
        protected Random random = new Random();

        protected Texture[] enemyTextureArray = new Texture[5];

        protected AbstractItem[] drops;

        protected bool hitPlayer = false;

        protected int lvl;
        protected int mp;
        protected int attackDamage;
        protected int def;
        protected float aggroRange;
        protected float attackRange;
        protected int movingFor = 0;    //moving for millisek in one direction
        protected int randomMovingDirection;
        //Getter**************************************************************************************

        public bool getTochedPlayer()
        {
            return hitPlayer;
        }

        public int getLvl()
        {
            return lvl;
        }
        public int getCurrentHP()
        {
            return currentHP;
        }
        public int getMp()
        {
            return mp;
        }
        public int getAttackDamage()
        {
            return attackDamage;
        }
        public float getAttackRange()
        {
            return attackRange;
        }
        public int getDef()
        {
            return def;
        }
        public float getAggroRange()
        {
            return aggroRange;
        }

        
        //********************************************************************************************

        //Methods*************************************************************************************
        override public void update(GameTime gameTime)
        {
            movementSpeed = movementSpeedConstante * (float)gameTime.EllapsedTime.TotalMilliseconds;
            type = "enemy";
            animate(enemyTextureArray);
            sprite.Position = position;
            takeDmg();
            if (currentHP<=0)
            {
                isAlive = false;
            }
            if (isAlive)
            {
                hitBox.setPosition(sprite.Position);
                
                if (sensePlayer())  //if a player is sensed (is in aggroRange) react else not ;)
                {
                    react();
                }
                else
                {
                    notReact();
                }
            }

            lifeBar.update(this);
            
            finalize();
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

            if (watch1.ElapsedMilliseconds >= movingFor) //only evaluate a new direction, if the enemy isnt moving already
            {
                randomMovingDirection = random.Next(9);
                watch1.Restart();
                movingFor = 0;
            }
            switch (randomMovingDirection)  //move in the direction for 1000 millisecounds so 1 second
            {
                case 0:
                    if (EnemyHandler.getMap().getWalkable(hitBox, up))
                        move(up);
                    watch1.Start();
                    movingFor = (int)(1000*random.NextDouble()) + 500;
                    break;
                case 1:
                    if (EnemyHandler.getMap().getWalkable(hitBox, upRight))
                        move(upRight);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 2:
                    if (EnemyHandler.getMap().getWalkable(hitBox, right))
                        move(right);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 3:
                    if (EnemyHandler.getMap().getWalkable(hitBox, downRight))
                        move(downRight);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 4:
                    if (EnemyHandler.getMap().getWalkable(hitBox, down))
                        move(down);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 5:
                    if (EnemyHandler.getMap().getWalkable(hitBox, downLeft))
                        move(downLeft);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 6:
                    if (EnemyHandler.getMap().getWalkable(hitBox, left))
                        move(left);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                case 7:
                    if (EnemyHandler.getMap().getWalkable(hitBox, upLeft))
                        move(upLeft);
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
                default:    //stand still for one sek
                    move(new Vector2f(0, 0));
                    watch1.Start();
                    movingFor = (int)(1000 * random.NextDouble()) + 500;
                    break;
            }
        }

        public bool sensePlayer()
        {
            if (hitBox.distanceTo(ObjectHandler.player.getHitBox()) <= aggroRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void takeDmg()
        {
            if (hitBox.hit(ObjectHandler.player.getHitBoxSword()) && isVulnerable() && ObjectHandler.player.pressedKeyForAttack())
            {
                int dmg = ObjectHandler.player.getAtt() - def;
                currentHP -= dmg;
                tookDmg = true;

                Vector2f knockedInDirection = ObjectHandler.player.getFacingDirection();

                knockedBack(knockedInDirection, ObjectHandler.player.getKnockBack());
            }
        }

        override public void drop()
        {
            for (int i = 0; i < drops.Length; i++)
            {
                if (drops[i].getDropRate() > random.Next(101))
                {
                    for (int j = 0; j < random.Next(50); j++)
                    {
                        drops[i].cloneAndDrop(new Vector2f(sprite.Position.X + random.Next(100), sprite.Position.Y + random.Next(100)));
                    }
                }
            } 
        }
        //********************************************************************************************

        //Abstract************************************************************************************
        public abstract void attack();  //the attack animation an etc.

        public abstract void react();   //what the enemy does if it's sense a player

        public abstract void notReact();    //what the enemy normaly does
    }
}
