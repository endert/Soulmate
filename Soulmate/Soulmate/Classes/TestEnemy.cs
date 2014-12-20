﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class TestEnemy : AbstractEnemy
    {
        Texture enemyTexture = new Texture("Pictures/Player.png");

        public TestEnemy(Vector2f spawnPos, int _lvl)
        {
            enemySprite = new Sprite(enemyTexture);
            enemySprite.Position = spawnPos;
            lvl = _lvl;
            hp = 100 + 10 * (lvl - 1);
            mp = 100 + 10 * (lvl - 1);
            attackRange = 10f;
            aggroRange = 560f;
            _movementSpeed = 0.1f;
        }

        public override void attack()
        {

        }

        public override void react()
        {

            //still not working
            //move(EnemyHandler.player.getSprite().Position);
        }

        public override void notReact()
        {
            //int t = 0;
            //int c = 1000;

            //if (!isWaiting())
            //{
            //    switch (EnemyHandler.random.Next(8))
            //    {
            //        case 0:
            //            //move up
            //            Vector2f up = new Vector2f(0, -movementSpeed);

            //            moveTimes(up, c, t+100);
            //            break;
            //        case 1:
            //            //move up right
            //            Vector2f upR = new Vector2f(movementSpeed, -movementSpeed);

            //            moveTimes(upR, c, t+200);
            //            break;
            //        case 2:
            //            //move right
            //            Vector2f right = new Vector2f(movementSpeed, 0);

            //            moveTimes(right, c, t+300);
            //            break;
            //        case 3:
            //            //move down right
            //            Vector2f downR = new Vector2f(movementSpeed, movementSpeed);

            //            moveTimes(downR, c, t+400);
            //            break;
            //        case 4:
            //            //move down
            //            Vector2f down = new Vector2f(0, movementSpeed);

            //            moveTimes(down, c, t+500);
            //            break;
            //        case 5:
            //            //move down left
            //            Vector2f downL = new Vector2f(-movementSpeed, movementSpeed);

            //            moveTimes(downL, c, t+1000);
            //            break;
            //        case 6:
            //            //move left
            //            Vector2f left = new Vector2f(-movementSpeed, 0);

            //            moveTimes(left, c, t-200);
            //            break;
            //        case 7:
            //            //move up left
            //            Vector2f upL = new Vector2f(-movementSpeed, -movementSpeed);

            //            moveTimes(upL, c, t);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        private void moveTimes(Vector2f direction, int count, int wTime)
        {
            //warks but its odd
            int c = count;
            Vector2f[] path = new Vector2f[c];
            for (int i = 0; i < c; i++)
            {
                if (EnemyHandler.map.getWalkable(enemySprite, c*direction))
                {
                    path[i] = direction;        
                }
            }
            move(path);
            if (!isWaiting())
            {
                wait(wTime);
            }
            
        }
    }
}