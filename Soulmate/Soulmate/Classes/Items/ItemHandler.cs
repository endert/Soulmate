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
        public static Inventory playerInventory { get; set; }

        public ItemHandler(Map lvlMap, Inventory _playerInventory)
        {
            map = lvlMap;
            playerInventory = _playerInventory;
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].getIsAlive())
                {
                    if (!playerInventory.isFull() && Items[i].distancePlayer() <= 350 && Items[i].onMap)
                    {
                        Items[i].pickUp();
                    }
                    Items[i].update(gameTime);
                }
                else
                {
                    Items.RemoveAt(i);
                    i--;
                }
            }
        }

        static public void updateInventoryMatrix(GameTime gameTime)
        {
            foreach (AbstractItem item in playerInventory.inventoryMatrix)
            {
                if (item != null)
                {
                    item.position = new Vector2f((item.i * 50 + ItemHandler.playerInventory.inventory.Position.X), (item.j * 50 + ItemHandler.playerInventory.inventory.Position.Y));
                    item.visible = true;
                }
            }
        }

        public void deleate()
        {
            Items.Clear();
        }

        static public void drawInventoryItems(RenderWindow window)
        {
            foreach (AbstractItem item in playerInventory.inventoryMatrix)
            {
                if (item != null)
                {
                    item.draw(window);
                }
            }
        }

        public void draw(RenderWindow window)
        {
            foreach (AbstractItem item in Items)
            {
                if (item.visible)
                {
                    item.draw(window);
                }
            }
        }
    }
}
