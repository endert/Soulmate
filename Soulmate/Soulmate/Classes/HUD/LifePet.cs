using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class LifePet
    {
        Texture lifePetTextureBackground = new Texture("Pictures/Life/LifePetBackground.png");
        Texture lifePetTextureBar = new Texture("Pictures/Life/LifePetBar.png");
        Sprite lifePetSpriteBackground;
        Sprite lifePetSpriteBar;
        
        public LifePet()
        {
            lifePetSpriteBackground = new Sprite(lifePetTextureBackground);
            lifePetSpriteBar = new Sprite(lifePetTextureBar);
        }

        //public Sprite scale()
        //{
        //    lifePetTextureBar.Size.X *= CurrentLifePet/MaxLifePet
            
        //    return lifePetSpriteBar;
        //}

        public void setPosition()
        {
            if(ObjectHandler.pet != null)
            { 
                lifePetSpriteBackground.Position = new Vector2f(ObjectHandler.pet.getPosition().X, ObjectHandler.pet.getPosition().Y);
                lifePetSpriteBar.Position = new Vector2f(lifePetSpriteBackground.Position.X + 2, lifePetSpriteBackground.Position.Y + 2);
            }
        }
        
        public void update(GameTime gameTime)
        {
            setPosition();
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifePetSpriteBackground);
            window.Draw(lifePetSpriteBar);
        }
    }
}
