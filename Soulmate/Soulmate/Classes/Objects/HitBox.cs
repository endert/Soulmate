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
    public class HitBox
    {
        private Vector2f position { get; set; }
        private float width { get; set; }
        private float height { get;set; }

        Vector2f unionPos;
        float unionWidth;
        float unionHeight;

        public Vector2f getPosition()
        {
            return position;
        }
        public void setPosition(Vector2f _pos)
        {
            position = new Vector2f(_pos.X, _pos.Y);
        }
        public float getWidth()
        {
            return width;
        }
        public float getHeight()
        {
            return height;
        }

        public HitBox(Vector2f pos, float _width, float _height)
        {
            position = pos;
            width = _width;
            height = _height;
        }

        public Vector2f hitFrom(HitBox h)
        {
            Vector2f hitFrom = new Vector2f(0, 0);

            if (hitWithoutInsection(h))
            {
                if ((position.X+width) < h.position.X)
                {
                    hitFrom.X = 1;
                }
                else
                {
                    if ((h.position.X + h.width) < position.X)
                    {
                        hitFrom.X = -1;
                    }
                    else
                    {
                        hitFrom.X = 0;
                    }
                }

                if ((position.Y+height) <= h.position.Y)
                {
                    hitFrom.Y = 1;
                }
                else
                {
                    if ((h.position.Y + h.height) <= position.Y)
                    {
                        hitFrom.Y = -1;
                    }
                    else
                    {
                        hitFrom.Y = 0;
                    }
                }
            }

            if (hit(h) && !hitWithoutInsection(h))
            {
                if ((position.X) <= h.position.X)
                {
                    hitFrom.X = 1;
                }
                else
                {
                    hitFrom.X = -1;
                }

                if ((position.Y) <= h.position.Y)
                {
                    hitFrom.Y = 1;
                }
                else
                {
                    hitFrom.Y = -1;
                }
            }

            return hitFrom;
        }

        public bool hit(HitBox h)
        {
            union(h);
            if ((unionWidth <= (this.width + h.width)) && (unionHeight <= (this.height + h.height)))
                return true;
            else
                return false;
        }

        public bool hitWithoutInsection(HitBox h)
        {
            union(h);
            if (((unionWidth <= (this.width + h.width)) && (unionHeight == (this.height + h.height))) ||
                ((unionWidth == (this.width + h.width)) && (unionHeight <= (this.height + h.height))))
                return true;
            else
                return false;
        }

        private void union(HitBox h)
        {
            Vector2f thisBottomRight = new Vector2f(this.position.X + this.width, this.position.Y + this.height);
            Vector2f hBottomRight = new Vector2f(h.position.X + h.width, h.position.Y + h.height);

            unionPos = new Vector2f((this.position.X <= h.position.X) ? (this.position.X) : (h.position.X), 
                (this.position.Y <= h.position.Y) ? (this.position.Y) : (h.position.Y));

            unionWidth = ((thisBottomRight.X >= hBottomRight.X) ? (thisBottomRight.X) : (hBottomRight.X)) - unionPos.X;
            unionHeight = ((thisBottomRight.Y >= hBottomRight.Y) ? (thisBottomRight.Y) : (hBottomRight.Y)) - unionPos.Y;
        }
    }
}
