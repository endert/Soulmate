using SFML.Graphics;
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
        public static Player player
        {
            get;
            set;
        }
        public static PlayerPetFusion PPF { get; set; }
        public static bool IsPlayerPetFusion
        {
            get
            {
                return false;
            }
            set
            {
                IsPlayerPetFusion = value;
            }
        }

        public ObjectHandler(Map _lvlMap,Player _player)
        {
            lvlMap = _lvlMap;
            gObjs = new List<GameObjects>();
            player = _player;
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
                if (!gObjs[i].getIsAlive())
                {
                    if (gObjs[i].getType().Equals("enemy"))
                    {
                        //player.setCurrentFusionValue();
                    }
                    gObjs[i].drop();
                    gObjs.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < gObjs.Count; i++)
            {
                gObjs[i].setIndexEntityList(i);
                gObjs[i].update(gameTime);
            }
        }

        public void draw(RenderWindow window)
        {
            foreach (GameObjects gObj in gObjs)
            {
                gObj.draw(window);
            }
        }

        static public void deleate()
        {
            foreach (GameObjects gObj in gObjs)
            {
                gObj.kill();
            }
            ItemHandler.deleate();
        }
    }
}