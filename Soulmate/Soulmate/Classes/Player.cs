using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate.Classes
{
    class Player
    {
        Sprite playerSprite;
        Texture playerTexture = new Texture("Pictures/SpielerSeiteRechts.png");
        Map map;
        Vector2f movement;
        float movementSpeed;

        public Sprite getSprite()
        {
            return playerSprite;
        }

        public float getWeidth()
        {
            return playerSprite.Texture.Size.X;
        }

        public float getHeight()
        {
            return playerSprite.Texture.Size.Y;
        }

        public void setSpritePosition(Vector2f pos)
        {
            playerSprite.Position = pos;
        }

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            playerSprite = new Sprite(playerTexture);
            playerSprite.Position = spawnPosition;
            map = levelMap;
        }

        public void update(GameTime time)
        {
            movementSpeed = 0.3f * time.EllapsedTime.Milliseconds;
            movement = new Vector2f(0, 0);
            movement = getKeyPressed();
            move(movement);
        }

        public Vector2f getKeyPressed()
        {
            Vector2f result = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                result.X -= movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                result.Y -= movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                result.Y += movementSpeed;

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                result.X += movementSpeed;

            return result;
        }

        public void move(Vector2f move)
        {
            if (map.getWalkable(playerSprite, new Vector2f(move.X, move.Y)))
                playerSprite.Position = new Vector2f(playerSprite.Position.X + move.X, playerSprite.Position.Y + move.Y);
        }

        public Vector2f getMovemnet()
        {
            return movement;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(playerSprite);
        }
    }
}
