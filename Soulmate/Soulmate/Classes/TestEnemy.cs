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
        Texture enemyTexture = new Texture("Pictures/Player.png");

        public TestEnemy(Vector2f spawnPos, int _lvl)
        {
            enemySprite = new Sprite(enemyTexture);
            enemySprite.Position = spawnPos;
            lvl = _lvl;
            hp = 1 + 1 * (lvl - 1);
            mp = 1 + 1 * (lvl - 1);
            attackRange = 50f;
            aggroRange = 300f;
            movementSpeed = 1f;
        }

        public override void attack()
        {
            Console.WriteLine("ATTACK THE TITAN!!");
        }

        public override void react()
        {
            if (distancePlayer(EnemyHandler.PosPlayer())<= attackRange)
            {
                attack();
            }

            //  player direction(to the mid of the sprite)
            Vector2f playerDirection = new Vector2f((EnemyHandler.player.getSprite().Position.X+(EnemyHandler.player.getWeidth()/2)) - getPosition().X, 
                (EnemyHandler.player.getSprite().Position.Y+(EnemyHandler.player.getHeight()/2)) - getPosition().Y);

            move(playerDirection);
        }

        public override void notReact()
        {
            moveRandom();
        }
    }
}
