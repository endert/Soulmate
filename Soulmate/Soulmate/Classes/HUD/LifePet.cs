using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes.HUD
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
            lifePetSpriteBackground.Position = ;
        }

        public void update(GameTime gameTime)
        {
            
        }

        public void draw(RenderWindow window)
        {
            window.Draw(lifePetSpriteBackground);
            window.Draw(lifePetSpriteBar);
        }
    }
}
