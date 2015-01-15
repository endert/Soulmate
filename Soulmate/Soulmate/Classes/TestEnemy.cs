using SFML.Graphics;
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
        Texture[] enemyTextures = { new Texture("Pictures/Enemy/Enemy1Front.png"), new Texture("Pictures/Enemy/Enemy1Rueck.png"), new Texture("Pictures/Enemy/Enemy1SeiteRechts.png"), new Texture("Pictures/Enemy/Enemy1SeiteLinks.png") };

        public TestEnemy(Vector2f spawnPos, int _lvl)
        {
            sprite = new Sprite(enemyTextures[0]);
            position = spawnPos;
            sprite.Position = position;
            isAlive = true;
            hitBox = new HitBox(sprite.Position, sprite.Texture.Size.X, sprite.Texture.Size.Y);
            lvl = _lvl;
            hp = 1 + 1 * (lvl - 1);
            mp = 1 + 1 * (lvl - 1);
            attackDamage = 1 + 1 * (lvl -1);
            attackRange = 75f;
            aggroRange = 300f;
            movementSpeed = 1f;
            knockBack = 1f;
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
            //if (distancePlayer() <= attackRange)
            //{
            //    attack();
            //}

            //  player direction(to the ca. mid of the sprite)
            //Vector2f playerDirection = new Vector2f((ObjectHandler.player.getPosition().X + (ObjectHandler.player.getWeidth() / 2)) - getPosition().X,
            //    (ObjectHandler.player.getSprite().Position.Y + (ObjectHandler.player.getHeight() / 2)) - getPosition().Y);

            Vector2f playerDirection = new Vector2f(0, 0);
            if (ObjectHandler.player.getHitBox().getPosition().X + ObjectHandler.player.getHitBox().getWidth() < position.X) //player is to the left
            {
                playerDirection.X = -1;
            }
            else if (position.X + hitBox.getWidth() < ObjectHandler.player.getHitBox().getPosition().X)
            {
                playerDirection.X = 1;
            }

            if (ObjectHandler.player.getHitBox().getPosition().Y + ObjectHandler.player.getHitBox().getHeight() < position.Y)
            {
                playerDirection.Y = -1;
            }
            else if (position.Y + getHeight() < ObjectHandler.player.getHitBox().getPosition().Y)
            {
                playerDirection.Y = 1;
            }

            if (!touchedPlayer())
                move(playerDirection);
            else
            {
                move(new Vector2f(-playerDirection.X, -playerDirection.Y));
                facingInDirection = playerDirection;
            }
        }

        public override void notReact()
        {
            moveRandom();
        }

        public override void animate()
        {
            if (facingInDirection.Y>0)
            {
                sprite = new Sprite(enemyTextures[0]);
            }
            else if (facingInDirection.Y < 0)
            {
                sprite = new Sprite(enemyTextures[1]);
            }
            else if (facingInDirection.X > 0)
            {
                sprite = new Sprite(enemyTextures[2]);
            }
            else
            {
                sprite = new Sprite(enemyTextures[3]);
            }
        }
    }
}
