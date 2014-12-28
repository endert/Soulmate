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

        public TestEnemy(Vector2f spawnPos, int _lvl, int _index)
        {
            enemySprite = new Sprite(enemyTexture);
            enemySprite.Position = spawnPos;
            isAlive = true;
            index = _index;
            hitBox = new HitBox(enemySprite.Position, enemySprite.Texture.Size.X, enemySprite.Texture.Size.Y);
            lvl = _lvl;
            hp = 1 + 1 * (lvl - 1);
            mp = 1 + 1 * (lvl - 1);
            attackRange = 75f;
            aggroRange = 300f;
            movementSpeed = 1f;
        }

        public override void attack()
        {
            if (touchedPlayer())
            {
            }
            //EnemyHandler.player.setHealth(EnemyHandler.player.getHealth()-attackDamage);
        }

        public override void react()
        {
            if (distancePlayer() <= attackRange)
            {
                attack();
            }

            //  player direction(to the mid of the sprite)
            Vector2f playerDirection = new Vector2f((EnemyHandler.getPlayer().getSprite().Position.X + (EnemyHandler.getPlayer().getWeidth() / 2)) - getPosition().X,
                (EnemyHandler.getPlayer().getSprite().Position.Y + (EnemyHandler.getPlayer().getHeight() / 2)) - getPosition().Y);

            if (!touchedPlayer())
                move(playerDirection);
            else
                move(new Vector2f(0, 0));
        }

        public override void notReact()
        {
            moveRandom();
        }
    }
}
