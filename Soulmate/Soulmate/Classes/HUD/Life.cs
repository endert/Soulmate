using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Life
    {
        Texture[] lifeHeartTexture = { new Texture("Pictures/Life/LifeHeartZero.png"), new Texture("Pictures/Life/LifeHeartOne-Quarter.png"), new Texture("Pictures/Life/LifeHeartHalf.png"), 
                                       new Texture("Pictures/Life/LifeHeartThree-Quarters.png"), new Texture("Pictures/Life/LifeHeartFull.png") };
        List<Sprite> lifeHeartSprite = new List<Sprite>();

        public Life()
        {
            for (int i = 0; i < ObjectHandler.player.getMaxLife() / 4; i++)
            {
                lifeHeartSprite.Add(new Sprite(lifeHeartTexture[0]));
            }
        }

        public List<Sprite> textureToSprite()
        {
            for (int i = 0; i < ObjectHandler.player.getMaxLife()/4; i++)
            {
                if((i+1)*4<=ObjectHandler.player.getCurrentLife())
                {
                    lifeHeartSprite[i] = new Sprite(lifeHeartTexture[4]);
                }

                else if((i*4)<=ObjectHandler.player.getCurrentLife())
                {
                    lifeHeartSprite[i] = new Sprite(lifeHeartTexture[(ObjectHandler.player.getCurrentLife() % 4)]);
                }

                else
                {
                    lifeHeartSprite[i] = new Sprite(lifeHeartTexture[0]);
                }

                lifeHeartSprite[i].Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5 + (i*lifeHeartSprite[i].Texture.Size.X)),
                                                           (InGame.VIEW.Center.Y + (Game.windowSizeY / 2) - lifeHeartSprite[i].Texture.Size.Y - 5));
            }
            return lifeHeartSprite;
        }


        public void update(GameTime gameTime)
        {
            textureToSprite();
        }

        public void draw(RenderWindow window)
        {
            foreach (Sprite sprite in lifeHeartSprite)
            {
                window.Draw(sprite);
            }
        }
    }
}
