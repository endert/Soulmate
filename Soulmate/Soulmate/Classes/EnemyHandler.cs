using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Soulmate.Classes
{
    class EnemyHandler
    {
        public List<AbstractEnemy> enemies = new List<AbstractEnemy>();
        Player player;
        int lvlCount;
        public static Random random = new Random();
        public static Map map;

        public EnemyHandler(Player p, int _lvlCount, Map _map)
        {
            player = p;
            lvlCount = _lvlCount;
            map = _map;

            switch (lvlCount)
            {
                case 1:
                    for (int i = 0; i < 1; i++)
                    {
                        float rX = 130 + random.Next(1000);
                        float rY = 50 + random.Next(1000);

                        enemies.Add(new TestEnemy(new Vector2f(rX, rY), 1));    
                    }
                    break;
                default:
                    break;
            }
        }

        public Vector2f PosPlayer()
        {
            return new Vector2f(player.getSprite().Position.X + (player.getWeidth() / 2), player.getSprite().Position.Y + (player.getHeight() / 2));
        }

        public void draw(RenderWindow window)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].draw(window);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].update(gameTime,PosPlayer());
            }
        }
    }
}
