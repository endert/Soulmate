using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class BarFusionPlayerPet
    {
        Texture barBackgroundTexture = new Texture("Pictures/FusionBarBackground.png");
        Texture barTexture = new Texture("Pictures/FusionBar.png");
        Sprite barBackgroundSprite;
        Sprite barSprite;

        public BarFusionPlayerPet()
        {
            barBackgroundSprite = new Sprite(barBackgroundTexture);
            barSprite = new Sprite(barTexture);
        }

        public Sprite scale()
        {
            barBackgroundSprite.Scale = new Vector2f(-1 + ((float)ObjectHandler.player.currentFusionValue / (float)ObjectHandler.player.maxFusionValue), 1);

            return barBackgroundSprite;
        }

        public void setPosition(float positionOfLifePlayer)
        {
            barSprite.Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5),
                                                        (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + positionOfLifePlayer + 5));
            barBackgroundSprite.Position = new Vector2f(barSprite.Position.X + barSprite.Texture.Size.X, barSprite.Position.Y);
        }

        public void update(float positionOfLifePlayer)
        {
            setPosition(positionOfLifePlayer);
            scale();
        }

        public void draw(RenderWindow window)
        {
            window.Draw(barSprite);
            window.Draw(barBackgroundSprite);
        }
    }
}
