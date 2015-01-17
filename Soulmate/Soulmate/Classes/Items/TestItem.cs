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
    class TestItem : AbstractItem
    {
        public TestItem()
        {
            name = "Pete";
            dropRate = 100;
            texture = new Texture("Pictures/Items/TestItem(Pete).png");
            sprite = new Sprite(texture);
            ItemHandler.Items.Add(this);
        }
    }
}
