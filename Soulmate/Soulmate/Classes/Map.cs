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
    class Map
    {
        Blocks[,] map;
        Texture blockTex;

        public int objectSize = 32;
        
        public static String white = "ffffffff";//Boden
        public static String black = "ff000000";//Wald

        public bool getWalkable(Sprite sprite, Vector2f vector)
        {
            bool walkable = true;
            Vector2f newPosition = new Vector2f(sprite.Position.X + vector.X, sprite.Position.Y + vector.Y);

            //Map-Grenzen
            if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.X > map.GetLength(0) * objectSize || newPosition.Y > map.GetLength(1) * objectSize)
            {
                return false;
            }
            //Kollision
            if (!(map[(int)(newPosition.X / objectSize), (int)(newPosition.Y / objectSize)].getWalkable()/*links oben*/
                && map[(int)(newPosition.X / objectSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / objectSize)].getWalkable()/*links unten*/
                && map[(int)(newPosition.X / objectSize), (int)((newPosition.Y + sprite.Texture.Size.Y / 2)) / objectSize].getWalkable()/*links mitte*/

                && map[(int)((newPosition.X + sprite.Texture.Size.X) / objectSize), (int)(newPosition.Y / objectSize)].getWalkable()/*rechts oben*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / objectSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / objectSize)].getWalkable()/*rechts unten*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / objectSize), (int)((newPosition.Y + sprite.Texture.Size.Y / 2) / objectSize)].getWalkable()/*rechts mitte*/

                && map[(int)((newPosition.X + sprite.Texture.Size.X / 2) / objectSize), (int)(newPosition.Y / objectSize)].getWalkable()/*oben mitte*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X / 2) / objectSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / objectSize)].getWalkable()/*unten mitte*/
                ))
                walkable = false;

           

            return walkable;
        }

        public Map(Bitmap mask)
        {
            map = new Blocks[mask.Width, mask.Height];

            for(int i=0;i<map.GetLength(0);i++)
            {
                for(int j=0;j<map.GetLength(1);j++)
                {
                    blockTex = new Texture("Pictures/Map/Sand.png");
                    if (mask.GetPixel(i, j).Name == white)
                        map[i, j] = new Blocks(0, new Vector2f(i * objectSize, j * objectSize), blockTex);
                    if (mask.GetPixel(i, j).Name == black)
                        map[i, j] = new Blocks(1, new Vector2f(i * objectSize, j * objectSize), blockTex);
                }
            }
        }

        public void draw(RenderWindow window)
        {
            for(int i=0;i<map.GetLength(0);i++)
            {
                for(int j=0;j<map.GetLength(1);j++)
                {
                    map[i, j].draw(window);
                }
            }
        }
    }
}
