using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Sword : AbstractEquipment
    {
         public Sword()
        {
            name = "Sword";
            dropRate = 100;
            texture = new Texture("Pictures/Items/Schwert.png");
            sprite = new Sprite(texture);
            ItemHandler.Items.Add(this);
        }

        override public void cloneAndDrop(Vector2f dropPosition)
        {
            (new Sword()).drop(dropPosition);
        }
    }
}
