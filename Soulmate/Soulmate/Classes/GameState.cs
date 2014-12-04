using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Soulmate
{
    public enum EGameStates
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

        EGameStates update(GameTime time);

        void draw(RenderWindow window);
    }
}
