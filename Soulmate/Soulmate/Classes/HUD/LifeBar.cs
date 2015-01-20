using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class LifeBar
    {
        Texture lifeTextureBackground = new Texture("Pictures/Life/LifePetBackground.png");
        Texture lifeTextureBar = new Texture("Pictures/Life/LifePetBar.png");
        Sprite lifeSpriteBackground;
        Sprite lifeSpriteBar;
        
        public LifeBar()
        {
            lifeSpriteBackground = new Sprite(lifeTextureBackground);
            lifeSpriteBar = new Sprite(lifeTextureBar);
        }

        public Sprite scale(GameObjects gameObject)
        {
            lifeSpriteBar.Scale = new Vector2f((float)gameObject.getCurrentHP() / (float)gameObject.getMaxHP(), 1);
            
            return lifeSpriteBar;
        }

        public void setPosition(GameObjects gameObject)
        {
            if(!gameObject.getType().Equals("player"))
            {
                if (gameObject != null)
                {
                    lifeSpriteBackground.Position = new Vector2f((gameObject.getPosition().X + gameObject.getSprite().Texture.Size.X / 2) - lifeTextureBackground.Size.X / 2, gameObject.getPosition().Y - 20);
                    lifeSpriteBar.Position = new Vector2f(lifeSpriteBackground.Position.X + 2, lifeSpriteBackground.Position.Y + 2);
                }
            }
        }
        
        public void update(GameObjects gameObject)
        {
            setPosition(gameObject);
            scale(gameObject);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifeSpriteBackground);
            window.Draw(lifeSpriteBar);
        }
    }
}
