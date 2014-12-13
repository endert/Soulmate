using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Inventory
    {
        Texture inventoryTex = new Texture("Pictures/Inventory.png");
        Sprite inventory;
        
        public Inventory()
        {
            inventory = new Sprite(inventoryTex);
            inventory.Position = new Vector2f((1280 - inventoryTex.Size.X) / 2, (720 - inventoryTex.Size.Y) / 2);
        }

        public void update()
        {

        }

        public void draw(RenderWindow window)
        {
            window.Draw(inventory);
        }
    }
}
