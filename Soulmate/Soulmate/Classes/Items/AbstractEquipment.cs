using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    abstract class AbstractEquipment : AbstractItem
    {
        override abstract public void cloneAndDrop(Vector2f dropPosition);
    }
}
