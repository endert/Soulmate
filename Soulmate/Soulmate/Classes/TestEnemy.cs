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
            _movementSpeed = 1f;
        }

        public override void attack()
        {

        }

        public override void react()
        {
            if (EnemyHandler.map.getWalkable(enemySprite, new Vector2f(1, 1)))
            {
                move(new Vector2f(10, 10));
            }

            //notReact();

        }

        public override void notReact()
        {
            //List<Vector2f> path = new List<Vector2f>();

            //for (int i = 0; i < 10; i++)
            //{
            //    path.Add(new Vector2f(1, 0));
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    path.Add(new Vector2f(0, 1));
            //}
            //for (int i = 0; i < 10; i++)
            //{
            //    path.Add(new Vector2f(-1, -1));
            //}

            //move(path);
        }
    }
}
