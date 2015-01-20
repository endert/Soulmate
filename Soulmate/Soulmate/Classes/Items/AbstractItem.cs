using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Soulmate.Classes
{
    abstract class AbstractItem
    {
        protected String name = "null";
        protected Stopwatch decay = new Stopwatch();
        protected bool isAlive = true;
        public Vector2f position { get; set; }
        protected Texture texture;
        public Sprite sprite { get; set; }
        protected int dropRate; //in percent

        protected float _pickUpRange = 150f;

        public float pickUpRange
        {
            get
            {
                return _pickUpRange;
            }
            set 
            { 
                _pickUpRange = value; 
            }
        }

        protected int decayingIn = 50000; //50sec
        public bool onMap { get; set; }
        public bool visible { get; set; }
        protected bool wasOnMap { get; set; }

        public bool getIsAlive()
        {
            return isAlive;
        }

        public void setPositionMatrix(int x, int y)
        {
            position = new Vector2f(x * ItemHandler.playerInventory.FIELDSIZE + ItemHandler.playerInventory.inventory.Position.X, 
                y * ItemHandler.playerInventory.FIELDSIZE + ItemHandler.playerInventory.inventory.Position.Y);
        }

        public void pickUp()
        {
            
            for (int i = 0; i < ItemHandler.playerInventory.inventoryMatrix.GetLength(0); i++) //row -> x-coordinate
            {
                for (int j = 0; j < ItemHandler.playerInventory.inventoryMatrix.GetLength(1); j++) //collum -> y-coordinate
                {
                    if (ItemHandler.playerInventory.inventoryMatrix[i,j] == null)
                    {
                        ItemHandler.playerInventory.inventoryMatrix[i, j] = this;
                        position = new Vector2f((j * 50 + ItemHandler.playerInventory.inventory.Position.X), (i * 50 + ItemHandler.playerInventory.inventory.Position.Y));
                        onMap = false;
                        return;
                    }
                }
            }
        }

        public int getDropRate()
        {
            return dropRate;
        }

        public void update(GameTime gameTime)
        {
            sprite.Position = position;
            if (onMap)
            {
                visible = true;
                wasOnMap = true;
                decay.Start();
                if (decay.ElapsedMilliseconds>=decayingIn)
                {
                    isAlive = false;
                }
            }
            if (wasOnMap && !onMap)
            {
                wasOnMap = false;
                onMap = false;
                decay.Reset();
            }
            if (!onMap)
            {
                visible = false;
            }
        }

        public void draw(RenderWindow window)
        {
            if (visible)
                window.Draw(sprite);
        }

        public float distancePlayer()
        {
            float distanceX = 0;
            float distanceY = 0;

            distanceX += Math.Min(Math.Abs((position.X) - ObjectHandler.player.getHitBox().getPosition().X),  //if the enemy is left of the player it checks the distance from the enemy's right side to the player's left 
                Math.Abs(position.X - (ObjectHandler.player.getHitBox().getPosition().X + ObjectHandler.player.getHitBox().getWidth())));

            distanceY += Math.Min(Math.Abs((position.Y) - ObjectHandler.player.getHitBox().getPosition().Y),
                Math.Abs(position.Y - (ObjectHandler.player.getHitBox().getPosition().Y + ObjectHandler.player.getHitBox().getHeight())));

            float distance = (float)Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));

            return distance;
        }

        public void drop(Vector2f dropPosition)
        {
            position = dropPosition;
            sprite.Position = position;
            onMap = true;
        }
    }
}
