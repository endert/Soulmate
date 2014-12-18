using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class NormalEnemy : AbstractEnemy
    {
        Texture enemyTexture = new Texture("Pictures/Player.png");

        public NormalEnemy(Vector2f spawnPos, int _lvl)
        {
            enemySprite = new Sprite(enemyTexture);
            enemySprite.Position = spawnPos;
            hp = 100;
            mp = 100;
            lvl = _lvl;
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

        /*public override float distancePlayer()
        {
            //  Position of players mid: adding the spirtes mid to the sprites position
            Vector2f midP = new Vector2f(player.getSprite().Position.X + (player.getWeidth() / 2), player.getSprite().Position.Y + (player.getHeight() / 2));
            Vector2f midE = new Vector2f(enemySprite.Position.X + (enemySprite.Texture.Size.X / 2), enemySprite.Position.Y + (enemySprite.Texture.Size.Y / 2));

            float distance = (float)Math.Sqrt(Math.Pow(Math.Abs(midE.X - midP.X), 2) + Math.Pow(Math.Abs(midE.Y - midP.Y), 2));

            return distance;
        }*/
    }
}
