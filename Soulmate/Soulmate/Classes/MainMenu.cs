using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Soulmate.Classes
{
    class MainMenu : GameStates
    {
        bool isPressed;
        int x;

        Texture startSelected;
        Texture startNotSelected;
        Texture exitSelected;
        Texture exitNotSelected;
        Texture controlSelected;
        Texture controlNotSelected;

        Texture testSelected;
        Texture testNotSelected;

        Sprite start;
        Sprite exit;
       
        Sprite test;

        Texture backGroundTex;
        Sprite backGround;

        View view;

        public void initialize()
        {
            isPressed = false;
            x = 0;

            start = new Sprite(startNotSelected);
            start.Position = new Vector2f(300, 100);

            exit = new Sprite(exitNotSelected);
            exit.Position = new Vector2f(300, 300);

            test = new Sprite(testNotSelected);
            test.Position = new Vector2f(300, 500);

            backGround = new Sprite(backGroundTex);
            backGround.Position = new Vector2f(0, 0);

            view = new View(new FloatRect(0, 0, 1280, 720));
        }

        public void loadContent()
        {
            startSelected = new Texture("Pictures/MainMenu/StartSelected.png");
            startNotSelected = new Texture("Pictures/MainMenu/StartNotSelected.png");

            exitSelected = new Texture("Pictures/MainMenu/EndSelected.png");
            exitNotSelected = new Texture("Pictures/MainMenu/EndNotSelected.png");

            testSelected = new Texture("Pictures/MainMenu/TestSelected.png");
            testNotSelected = new Texture("Pictures/MainMenu/TestNotSelected.png");
            
            backGroundTex = new Texture("Pictures/Hintergrund.png");
        }

        public EnumGameStates update(GameTime gameTime)
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                if (x == 0)
                    x = (x + 2) % 3;
                else
                    x = (x - 1) % 3;
                isPressed = true;
            }

            if(Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                x = (x + 1) % 3;
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;



            if(x==0)
            {
                start.Texture = startSelected;
                exit.Texture = exitNotSelected;
                test.Texture = testNotSelected;
            }

            if(x==1)
            {
                start.Texture = startNotSelected;
                exit.Texture = exitSelected;
                test.Texture = testNotSelected;
            }

            if(x==2)
            {
                start.Texture = startNotSelected;
                exit.Texture = exitNotSelected;
                test.Texture = testSelected;
            }


            if (x == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EnumGameStates.inGame;
            if (x == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EnumGameStates.none;

            return EnumGameStates.mainMenu;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            window.Draw(start);
            window.Draw(exit);
            window.Draw(test);
        }
    }
}
