using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate.Classes
{
    class MainMenu
    {
        bool ispressed;

        Texture startSelected;
        Texture startNotSelected;
        Texture exitSelected;
        Texture exitNotSelected;
        Texture controlSelected;
        Texture controlNotSelected;

        public void initialize()
        {

        }

        public void loadContent()
        {

        }

        public EnumGameStates update(GameTime gameTime)
        {

            return EnumGameStates.mainMenu;
        }

        public void draw(RenderWindow window)
        {

        }
    }
}
