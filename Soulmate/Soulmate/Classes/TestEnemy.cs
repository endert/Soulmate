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
        Texture enemyTexture = new Texture("Pictures/Enemy/Enemy1Front.png");

        public TestEnemy(Vector2f spawnPos, int _lvl)
        {
            sprite = new Sprite(enemyTexture);
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
            Vector2f playerDirection = new Vector2f((EnemyHandler.getPlayer().getPosition().X + (EnemyHandler.getPlayer().getWeidth() / 2)) - getPosition().X,
                (EnemyHandler.getPlayer().getSprite().Position.Y + (EnemyHandler.getPlayer().getHeight() / 2)) - getPosition().Y);

            if (!touchedPlayer())
                move(playerDirection);
            else
                move(new Vector2f(0, 0));
            foreach (Vector2f v in hitFromDirections)
            {
                Console.WriteLine(v.ToString());    
            }
            
        }

        public override void notReact()
        {
            moveRandom();
        }
    }
}
