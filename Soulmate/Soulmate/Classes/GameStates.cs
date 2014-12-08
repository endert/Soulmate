using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Soulmate.Classes
{
    public enum EnumGameStates
    {
        none,
        mainMenu,
        inGame,
        credits,
        gameWon,
        village,
        controls,
    }

    interface GameStates
    {
        void initialize();

        void loadContent();

        EnumGameStates update(GameTime time);

        void draw(RenderWindow window);
    }
}
