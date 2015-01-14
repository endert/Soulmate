using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class ObjectHandler
    {
        public static List<GameObjects> gObjs { get; set; }
        public static Map lvlMap { get; set; }

        public ObjectHandler(Map _lvlMap)
        {
            lvlMap = _lvlMap;
            gObjs = new List<GameObjects>();
        }

        public void add(GameObjects obj)
        {
            gObjs.Add(obj);
        }

        public void add(List<GameObjects> objs)
        {
            foreach (GameObjects obj in objs)
            {
                gObjs.Add(obj);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < gObjs.Count; i++)
            {
                gObjs[i].setIndexEntityList(i);
            }
        }
    }
}
