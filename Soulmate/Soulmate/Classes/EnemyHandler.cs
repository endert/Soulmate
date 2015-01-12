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
        int lvlCount;
        //static so other classes can access this Variables without constructing an object
        private static List<AbstractEnemy> enemies = new List<AbstractEnemy>();
        private static Player player;
        private static Map map;

        private Random random = new Random();

        public static List<AbstractEnemy> getEnemies()
        {
            return enemies;
        }

        public static Player getPlayer()
        {
            return player;
        }

        public static Map getMap()
        {
            return map;
        }


        public EnemyHandler(Player p, int _lvlCount, Map _map)
        {
            player = p;
            lvlCount = _lvlCount;
            map = _map;

            switch (lvlCount)
            {
                case 1:
                    for (int i = 0; i < 2; i++)
                    {
                        float rX = 100 + random.Next(1000);
                        float rY = 100 + random.Next(1000);
                        Vector2f spawnPos = new Vector2f(rX, rY);

                        TestEnemy test = new TestEnemy(spawnPos, 1, i);
                        if (test.distancePlayer() > 200 && map.getWalkable(test.getEnemySprite(), spawnPos))
                            enemies.Add(test);
                        else
                            i--;
                    }
                    break;
                default:
                    break;
            }
        }



        public static HitBox getHitBoxPlayer()
        {
            return new HitBox(player.getSprite().Position, player.getWeidth(), player.getHeight());
        }

        public static Vector2f PosPlayer()
        {
            return new Vector2f(player.getSprite().Position.X + (player.getWeidth() / 2), player.getSprite().Position.Y + (player.getHeight() / 2));
        }

        public bool getHitPlayer()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].getTochedPlayer())
                {
                    return true;
                }
            }
            return false;
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
                if (enemies[i].getIsAlive())
                {
                    enemies[i].update(gameTime);
                }
                else
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
