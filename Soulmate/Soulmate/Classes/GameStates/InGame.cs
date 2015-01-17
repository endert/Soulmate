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
        ItemHandler items;
        Inventory inventory;
        InGameMenu inGameMenu;
        Interface hud;

        bool inventoryOpen;
        bool isKlickedInventory = false;

        bool inGameMenuOpen;
        bool isKlickedInGameMenu = false;
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
            inGameMenu = new InGameMenu();

            items = new ItemHandler(map, inventory);
            
            hud = new Interface();
        }

        public bool getInventoryOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && !inventoryOpen)
            {
                isKlickedInventory = true;
                return inventoryOpen = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.I))
                isKlickedInventory = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && !isKlickedInventory && inventoryOpen == true)
            {
                isKlickedInventory = true;
                return inventoryOpen = false;
            }

            return false;
        }

        public bool getInGameMenuOpen()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isKlickedInGameMenu && !inGameMenuOpen)
            {
                isKlickedInGameMenu = true;
                return inGameMenuOpen = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                isKlickedInGameMenu = false;

            if (((Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isKlickedInGameMenu) || (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 0)) && inGameMenuOpen == true)
            {
                isKlickedInGameMenu = true;
                return inGameMenuOpen = false;
            }

            return false;
        }

        public EnumGameStates update(GameTime gameTime)
        {
            time.Update();

            getInventoryOpen();

            if (inventoryOpen==true)
            {
                inventory.update(gameTime);
            }

            getInGameMenuOpen();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && inGameMenu.getX() == 1)
            {
                ObjectHandler.deleate();
                return EnumGameStates.mainMenu;
            }

            if (inGameMenuOpen == true)
            {
                inGameMenu.update(gameTime);
            }

            else if(!inventoryOpen && !inGameMenuOpen)
            {
                backGround.Position = new Vector2f(view.Center.X - 640, view.Center.Y - 360);
                view.Move(new Vector2f((player.getPosition().X + (player.getWidth() / 2)), (player.getPosition().Y + (player.getHeight() / 2))) - view.Center);

                objcs.update(gameTime);
                items.update(gameTime);

                player.update(gameTime);
                if (player.getLife() <= 0)
                {
                    ObjectHandler.deleate();
                    items.deleate();
                    inventory.clear();
                    return EnumGameStates.mainMenu;
                }

                pet.update(gameTime);

                enemies.update(gameTime);

                hud.update(gameTime);
                }
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
            items.draw(window);
            
            if (inventoryOpen == true)
            {
                window.SetView(viewInventory);
                inventory.draw(window);
            }

            if (inGameMenuOpen == true)
            {
                window.SetView(viewInventory);
                inGameMenu.draw(window);
            }
        }
    }
}
