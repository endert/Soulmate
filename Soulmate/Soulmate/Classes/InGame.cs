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
    class InGame : GameStates
    {
        GameTime time = new GameTime();
        View view;
        Texture backGroundTex;
        Sprite backGround;
        Map map;
        Player player;

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

            map = new Map(new Bitmap("Pictures/Map/Map2.bmp"));
            player = new Player(new Vector2f(32 * 5, 32 * 10-219), map);
        }

        public EnumGameStates update(GameTime gameTime)
        {
            time.Update();
            backGround.Position = new Vector2f(view.Center.X - 640, view.Center.Y - 360);
            //view.Move(new Vector2f((player.getSprite().Position.X + (player.getWeidth()/2)), (player.getSprite().Position.Y + (player.getHeight()/2))) - view.Center);

            player.update(gameTime);

            return EnumGameStates.inGame;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            map.draw(window);
            player.draw(window);
            
        }
    }
}
