using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Inventory
    {
        Texture inventoryTexture = new Texture("Pictures/Inventory/inventory.png");
        public Sprite inventory { get; set; }

        Texture selectedTexture = new Texture("Pictures/Inventory/Selected.png");
        Sprite selected;

        bool isPressed = false;
        int x = 0, y = 0; //Inventarsteurung
        
        uint inventoryWidth;
        uint inventoryLength;

        public AbstractItem[,] inventoryMatrix { get; set; }
        
        public Inventory()
        {
            inventory = new Sprite(inventoryTexture);
            inventory.Position = new Vector2f((1280 - inventoryTexture.Size.X) / 2, (720 - inventoryTexture.Size.Y) / 2);

            selected = new Sprite(selectedTexture);
            selected.Position = inventory.Position;

            inventoryWidth = inventoryTexture.Size.X / selectedTexture.Size.X;
            inventoryLength = inventoryTexture.Size.Y / selectedTexture.Size.Y;

            inventoryMatrix = new AbstractItem[inventoryLength, inventoryWidth];
        }

        public void update(GameTime gameTime)
        {
            managment();
        }

        public bool isFull()
        {
            foreach (AbstractItem item in inventoryMatrix)
            {
                if (item == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void managment()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                y = (y + (inventoryMatrix.GetLength(0) - 1)) % inventoryMatrix.GetLength(0);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                y = (y + 1) % inventoryMatrix.GetLength(0);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && !isPressed)
            {
                x = (x + 1) % inventoryMatrix.GetLength(1);
                isPressed = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && !isPressed)
            {
                x = (x + (inventoryMatrix.GetLength(1) - 1)) % inventoryMatrix.GetLength(1);
                isPressed = true;
            }


            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up) && !Keyboard.IsKeyPressed(Keyboard.Key.Right) && !Keyboard.IsKeyPressed(Keyboard.Key.Left))
                isPressed = false;

            selected.Position = new Vector2f(x * 50 + inventory.Position.X, y * 50 + inventory.Position.Y);
        }


        public void draw(RenderWindow window)
        {
            window.Draw(inventory);
            ItemHandler.drawInventoryItems(window);
            window.Draw(selected);
        }

        public void clear()
        {
            for (int i = 0; i < inventoryMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryMatrix.GetLength(1); j++)
                {
                    inventoryMatrix[i, j] = null;
                }
            }
        }
    }
}
