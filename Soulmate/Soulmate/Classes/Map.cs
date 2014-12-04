using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Map
    {
        Blocks[,] map;
        Texture blockTex;
        
        public static String white = "ffffffff";//Boden
        public static String black = "ff000000";//Wald

        public bool getWalkable(Sprite sprite, Vector2f vector)
        {

        }
    }
}
