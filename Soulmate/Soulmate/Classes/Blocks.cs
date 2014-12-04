using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Blocks
    {
        bool walkable;
        Sprite blockSprite;

        public Texture getTexture()
        {
            return this.blockSprite.Texture;
        }

        public bool getWalkable()
        {
            return this.walkable;
        }

        public Blocks(int blockType, Vector2f position, Texture bodenTex)
        {
            switch(blockType)
            {
                case 0://Boden
                    {
                        this.blockSprite = new Sprite(new Texture("Pictures/Boden"));
                        this.walkable = true;
                        break;
                    }
                case 1://Wald
                    {
                        this.blockSprite = new Sprite(new Texture("Pictures/Wald"));
                        this.walkable = false;
                        break;
                    }
            }
        }

        public void draw(RenderWindow window)
        {
            window.Draw(this.blockSprite);
        }
    }
}
