using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Soulmate.Classes
{
    class ItemHandler
    {
        public static List<AbstractItem> Items = new List<AbstractItem>();
        public static Map map;

        public ItemHandler(Map lvlMap)
        {
            map = lvlMap;
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].getIsAlive())
                {
                    Items[i].update(gameTime);
                }
                else
                {
                    Items.RemoveAt(i);
                    i--;
                }
            }
        }

        public void draw(RenderWindow window)
        {
            foreach (AbstractItem item in Items)
            {
                if (item.onMap)
                {
                    item.draw(window);
                } 
            }
        }
    }
}
