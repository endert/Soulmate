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
        public int i { get; set; } //row in the inventoryMatrix
        public int j { get; set; } //colum in the inventoryMatrix
        protected Texture texture;
        protected Sprite sprite;
        protected int dropRate; //in percent

        protected int decayingIn = 5000; //5sec
        public bool onMap { get; set; }
        public bool visible { get; set; }
        protected bool wasOnMap { get; set; }

        public bool getIsAlive()
        {
            return isAlive;
        }

        public void pickUp()
        {
            onMap = false;
            for (int i = 0; i < ItemHandler.playerInventory.inventoryMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ItemHandler.playerInventory.inventoryMatrix.GetLength(1); j++)
                {
                    if (ItemHandler.playerInventory.inventoryMatrix[i,j] == null)
                    {
                        ItemHandler.playerInventory.inventoryMatrix[i, j] = this;
                        this.i = i;
                        this.j = j;
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

            distanceX += Math.Min(Math.Abs((position.X) - EnemyHandler.getHitBoxPlayer().getPosition().X),  //if the enemy is left of the player it checks the distance from the enemy's right side to the player's left 
                Math.Abs(position.X - (EnemyHandler.getHitBoxPlayer().getPosition().X + EnemyHandler.getHitBoxPlayer().getWidth())));

            distanceY += Math.Min(Math.Abs((position.Y) - EnemyHandler.getHitBoxPlayer().getPosition().Y),
                Math.Abs(position.Y - (EnemyHandler.getHitBoxPlayer().getPosition().Y + EnemyHandler.getHitBoxPlayer().getHeight())));

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
