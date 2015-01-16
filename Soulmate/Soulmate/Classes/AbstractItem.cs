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
        protected Stopwatch decay = new Stopwatch();
        protected bool isAlive = true;
        protected Vector2f position;
        protected Texture texture;
        protected Sprite sprite;
        protected int dropRate; //in percent

        protected int decayingIn = 5000; //60sec
        public bool onMap { get; set; }
        protected bool wasOnMap { get; set; }

        public bool getIsAlive()
        {
            return isAlive;
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
        }

        public void draw(RenderWindow window)
        {
            if (onMap)
                window.Draw(sprite);
        }

        public void drop(Vector2f dropPosition)
        {
            position = dropPosition;
            sprite.Position = position;
            onMap = true;
        }
    }
}
