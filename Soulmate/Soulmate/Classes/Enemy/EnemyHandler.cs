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
        private static Map map;

        public static List<AbstractEnemy> ENEMIES
        {
            get
            {
                return enemies;
            }
        }

        private Random random = new Random();

        public List<GameObjects> getEnemiesGameObjects()
        {
            List<GameObjects> _enemies = new List<GameObjects>();
            foreach (AbstractEnemy enemy in enemies)
            {
                _enemies.Add(enemy);
            }
            return _enemies;
        }

        public static Map getMap()
        {
            return map;
        }

        public EnemyHandler(Player p, int _lvlCount, Map _map)
        {
            lvlCount = _lvlCount;
            map = _map;

            switch (lvlCount)
            {
                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        float rX = 600 + random.Next(1000);
                        float rY = 400 + random.Next(400);
                        Vector2f spawnPos = new Vector2f(rX, rY);

                        TestEnemy test = new TestEnemy(spawnPos, 1);
                        if (test.getHitBox().distanceTo(ObjectHandler.player.getHitBox()) > 200 && map.getWalkable(test.getHitBox(), spawnPos))
                            enemies.Add(test);
                        else
                            i--;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
