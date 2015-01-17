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
        public TestEnemy(Vector2f spawnPos, int _lvl)
        {
            drops = new AbstractItem[]{ new TestItem() };
            setEnemyTextures();
            sprite = new Sprite(enemyTextureArray[0]);
            position = spawnPos;
            sprite.Position = position;
            isAlive = true;
            hitBox = new HitBox(sprite.Position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            lvl = _lvl;
            hp = 5 + 1 * (lvl - 1);
            mp = 1 + 1 * (lvl - 1);
            def = 0;
            attackDamage = 2 + 1 * (lvl -1);
            attackRange = 75f;
            aggroRange = 300f;
            knockBack = 50f;
        }

        private void setEnemyTextures()
        {
            enemyTextureArray[0] = new Texture("Pictures/Enemy/Enemy1/Enemy1Front.png");
            enemyTextureArray[1] = new Texture("Pictures/Enemy/Enemy1/Enemy1Rueck.png");
            enemyTextureArray[2] = new Texture("Pictures/Enemy/Enemy1/Enemy1SeiteRechts.png");
            enemyTextureArray[3] = new Texture("Pictures/Enemy/Enemy1/Enemy1SeiteLinks.png");
            enemyTextureArray[4] = new Texture("Pictures/Enemy/Enemy1/Enemy1FrontInvulnerable.png");
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
            if (!touchedPlayer())
                move(getPlayerDirection());
            else
            {
                move(new Vector2f(-getPlayerDirection().X, -getPlayerDirection().Y));
                facingInDirection = getPlayerDirection();
            }
        }

        public override void notReact()
        {
            moveRandom();
        }
    }
}