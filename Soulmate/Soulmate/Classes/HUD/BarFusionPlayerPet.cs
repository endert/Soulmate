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
        Texture barBackgroundTexture = new Texture("Pictures/BarBackground.png");
        Texture barTexture = new Texture("Pictures/FusionBar.png");
        Sprite barBackgroundSprite;
        Sprite barSprite;

        public BarFusionPlayerPet()
        {
            barBackgroundSprite = new Sprite(barBackgroundTexture);
            barSprite = new Sprite(barTexture);

            //Font font = new Font("Arial");
            //Text text = new Text("hgnkhzlh", font);
        }

        public Sprite scale(GameObjects gameObject)
        {
            barSprite.Scale = new Vector2f((float)gameObject.getCurrentHP() / (float)gameObject.getMaxHP(), 1);

            return barSprite;
        }

        public void setPosition(float Y)
        {
            //Console.WriteLine(LifePlayer.getLastLifeHeartSpritePositionBottomY());
            barBackgroundSprite.Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5),
                                                        (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + Y + 5));
            barSprite.Position = new Vector2f(barBackgroundSprite.Position.X + 2, barBackgroundSprite.Position.Y + 2);
        }

        public void update(float Y)
        {
            setPosition(Y);
            //scale(gameObject);
        }

        public void draw(RenderWindow window)
        {
            window.Draw(barBackgroundSprite);
            window.Draw(barSprite);
        }
    }
}
