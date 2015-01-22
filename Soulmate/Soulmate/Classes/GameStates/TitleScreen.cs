using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class TitleScreen : GameStates
    {
        bool isPressed;

        Texture titleScreenTexture;
        Sprite titleScreen;


        public void initialize()
        {
            isPressed = false;
            titleScreen = new Sprite(titleScreenTexture);
        }

        public void loadContent()
        {titleScreenTexture = new Texture("Pictures/")

        }

        public EnumGameStates update(GameTime gameTime)
        {
            return EnumGameStates.titleSreen;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(titleScreen);
        }
    }
}
