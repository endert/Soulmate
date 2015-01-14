using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Player : GameObjects
    {
        Texture playerTexture = new Texture("Pictures/Player/SpielerSeiteRechtsSchwert.png");
        Map map;
        Vector2f movement;

        float startLife = 4;
        Sprite lifeSprite;
        Texture lifeTexture = new Texture("Pictures/LifeFull.png");

        float att = 1;
        float def = 1;

        Texture[] playerTextures;

        public float getLife()
        {
            return startLife;
        }

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            sprite = new Sprite(playerTexture);
            sprite.Position = spawnPosition;
            position = spawnPosition;
            hitBox = new HitBox(sprite.Position, new Texture("Pictures/Player/SpielerSeiteRechts.png").Size.X, getHeight());
            map = levelMap;
        }

        override public void update(GameTime time)
        {
            movementSpeed = 0.2f * (float)time.EllapsedTime.TotalMilliseconds;

            //Console.Clear();
            //Console.WriteLine(movementSpeed);
            //Console.WriteLine((float)time.EllapsedTime.TotalMilliseconds);

            sprite.Position = position;
            hitBox.setPosition(sprite.Position);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            life(startLife);

            hitFromDirections.Clear();
        }

        public Vector2f getKeyPressed(float movementSpeed)
        {
            Vector2f result = new Vector2f(0, 0);

                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    result.X = -movementSpeed;

                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                    result.Y = -movementSpeed;

                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                    result.Y = movementSpeed;

                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    result.X = movementSpeed;

            return result;
        }

        public void life(float currentLife)
        {
            lifeSprite = new Sprite(lifeTexture);
            lifeSprite.Position = new Vector2f(10, (720 - lifeTexture.Size.Y));
            
            if(/*Collision with Enemy true*/false)
                currentLife -= 0.25f;

            if (currentLife == this.startLife)
                lifeSprite = new Sprite(lifeTexture);
        }
    }
}
