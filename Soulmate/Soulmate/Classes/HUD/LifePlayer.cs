using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class LifePlayer
    {
        static Texture[] lifeHeartTexture = { new Texture("Pictures/Life/LifeHeartZero.png"), new Texture("Pictures/Life/LifeHeartOne-Quarter.png"), new Texture("Pictures/Life/LifeHeartHalf.png"), 
                                       new Texture("Pictures/Life/LifeHeartThree-Quarters.png"), new Texture("Pictures/Life/LifeHeartFull.png") };
        static List<Sprite> lifeHeartSprite = new List<Sprite>();

        public LifePlayer()
        {
            for (int i = 0; i < ObjectHandler.player.getMaxHearts(); i++)
            {
                lifeHeartSprite.Add(new Sprite(lifeHeartTexture[0]));
            }
        }

        public static void addHeart()
        {
            lifeHeartSprite.Add(new Sprite(lifeHeartTexture[0]));
        }

        public List<Sprite> textureToSprite()
        {
            for (int i = 0; i < ObjectHandler.player.getMaxHearts(); i++)
            {
                if ((i + 1) * 4 <= ObjectHandler.player.getCurrentHP())
                {
                    lifeHeartSprite[i] = new Sprite(lifeHeartTexture[4]);
                }

                else if ((i * 4) <= ObjectHandler.player.getCurrentHP())
                {
                    lifeHeartSprite[i] = new Sprite(lifeHeartTexture[(ObjectHandler.player.getCurrentHP() % 4)]);
                }

                else
                {
                    lifeHeartSprite[i] = new Sprite(lifeHeartTexture[0]);
                }

                lifeHeartSprite[i].Position = new Vector2f((InGame.VIEW.Center.X - (Game.windowSizeX / 2) + 5 + ((i % 10) * lifeHeartSprite[i].Texture.Size.X)),
                                                           (InGame.VIEW.Center.Y - (Game.windowSizeY / 2) + (lifeHeartSprite[i].Texture.Size.Y * ((i / 10)))) + 5);
                                                                                                                  //Berrechnung damit Herzen in die nächste Zeile rutschen und wie viele Zeilen benötigt werden
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
