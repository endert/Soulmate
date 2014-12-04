using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class InGame : GameStates
    {
        GameTime time = new GameTime();
        View view;
        Texture backGroundTex;
        Sprite backGround;

        public void initialize()
        {
            time = new GameTime();
            time.Start();
            view = new View(new FloatRect(0, 0, 1280, 720));

            backGround = new Sprite(backGroundTex);
            backGround.Position = new Vector2f(0, 0);
            
        }

        public void loadContent()
        {
            backGroundTex = new Texture("Pictures/Hintergrund.png");
        }

        public EGameStates update(GameTime gameTime)
        {
            time.Update();
            backGround.Position = new Vector2f(view.Center.X - 640, view.Center.Y - 360);

            return EGameStates.inGame;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
        }
    }
}
