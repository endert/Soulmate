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
            hp = 100 + 10 * (lvl - 1);
            mp = 100 + 10 * (lvl - 1);
            attackRange = 10f;
            aggroRange = 560f;
            movementSpeed = 0.1f;
        }

        public override void attack()
        {

        }

        public override void react()
        {
           
        }

        public override void notReact()
        {
            
        }
    }
}
