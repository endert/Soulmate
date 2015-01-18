using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Interface
    {
        Texture[] lifeTexture = { new Texture("Pictures/Life/Life0hp.png"), new Texture("Pictures/Life/Life1hp.png"), new Texture("Pictures/Life/Life2hp.png"), new Texture("Pictures/Life/Life3hp.png"),
                                  new Texture("Pictures/Life/Life4hp.png"), new Texture("Pictures/Life/LifeFull.png") };
        Sprite lifeSprite;

        public Interface()
        {
            lifeSprite = new Sprite(lifeTexture[5]);
            lifeSprite.Position = getLifeSpritePosition();
        }

        public Vector2f getLifeSpritePosition()
        {
            return new Vector2f(((ObjectHandler.player.getPosition().X + (ObjectHandler.player.getWidth() / 2)) - 620),
                                 (ObjectHandler.player.getPosition().Y + (ObjectHandler.player.getHeight() / 2)) + 320);
        }

        public void update(GameTime gameTime)
        {
            lifeSprite.Position = getLifeSpritePosition();
        }

        public void draw(RenderWindow window)
        {
            switch ((int)ObjectHandler.player.getLife())
            {
                case 0:
                    {
                        lifeSprite = new Sprite(lifeTexture[0]);
                        lifeSprite.Position = getLifeSpritePosition();
                        break;
                    }

                case 2:
                    {
                        lifeSprite = new Sprite(lifeTexture[1]);
                        lifeSprite.Position = getLifeSpritePosition();
                        break;
                    }

                case 4:
                    {
                        lifeSprite = new Sprite(lifeTexture[2]);
                        lifeSprite.Position = getLifeSpritePosition();
                        break;
                    }

                case 6:
                    {
                        lifeSprite = new Sprite(lifeTexture[3]);
                        lifeSprite.Position = getLifeSpritePosition();
                        break;
                    }

                case 8:
                    {
                        lifeSprite = new Sprite(lifeTexture[4]);
                        lifeSprite.Position = getLifeSpritePosition();
                        break;
                    }

                case 10:
                    {
                        lifeSprite = new Sprite(lifeTexture[5]);
                        lifeSprite.Position = getLifeSpritePosition();
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            window.Draw(lifeSprite);
        }
    }
}
