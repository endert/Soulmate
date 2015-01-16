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
        View viewInventory;
        Texture backGroundTex;
        Sprite backGround;
        Map map;
        ObjectHandler objcs;
        Player player;
        Pet pet;
        EnemyHandler enemies;
        Inventory inventory;
        Interface hud;

        bool inventoryOpen;
        bool isKlickedInventory = false;

        public void initialize()
        {
            time = new GameTime();
            time.Start();
            view = new View(new FloatRect(0, 0, 1280, 720));
            viewInventory = new View(new FloatRect(0, 0, 1280, 720));

            backGround = new Sprite(backGroundTex);
            backGround.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            backGroundTex = new Texture("Pictures/Hintergrund.png");

            map = new Map(new Bitmap("Pictures/Map/Map2.bmp"));

            player = new Player(new Vector2f(32 * 15, 32 * 10 - 219), map);
 
            objcs = new ObjectHandler(map,player);

            enemies = new EnemyHandler(player, 1, map); //1 = lvl(map)

            pet = new Pet(player.getSprite());

            objcs.add(enemies.getEnemiesGameObjects());
            objcs.add(pet);
            objcs.add(player);

            inventory = new Inventory();

            hud = new Interface();
        }

        public EnumGameStates update(GameTime gameTime)
        {
            time.Update();
            

            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && !inventoryOpen)
            {
                inventoryOpen = true;
                isKlickedInventory = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.I))
                isKlickedInventory = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && inventoryOpen == true)
            {
                inventoryOpen = false;
                isKlickedInventory = true;
            }

            if (inventoryOpen==true)
            {
                inventory.update(gameTime);
            }

            else
            {
                backGround.Position = new Vector2f(view.Center.X - 640, view.Center.Y - 360);
                view.Move(new Vector2f((player.getPosition().X + (player.getWidth() / 2)), (player.getPosition().Y + (player.getHeight() / 2))) - view.Center);

                objcs.update(gameTime);

                player.update(gameTime);
                if (player.getLife() <= 0)
                {
                    ObjectHandler.deleate();
                    return EnumGameStates.mainMenu;
                }

                pet.update(gameTime);
               
                enemies.update(gameTime);

                hud.update(gameTime);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    return EnumGameStates.mainMenu;
            }
            return EnumGameStates.inGame;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.SetView(view);
            map.draw(window);
            objcs.draw(window);
            hud.draw(window);
            
            if(inventoryOpen==true)
            {
                window.SetView(viewInventory);
                inventory.draw(window);
            }
        }
    }
}
