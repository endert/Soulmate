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

        float startLife = 4;
        Sprite lifeSprite;
        Texture lifeTexture = new Texture("Pictures/LifeFull.png");

        float att = 1;
        float def = 1;

        TestEnemy testEnemy;


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

        public float getLife()
        {
            return startLife;
        }

        public Player(Vector2f spawnPosition, Map levelMap)
        {
            playerSprite = new Sprite(playerTexture);
            playerSprite.Position = spawnPosition;
            map = levelMap;
        }

        public void update(GameTime time)
        {
            movementSpeed = 0.2f * (float)time.EllapsedTime.TotalMilliseconds;

            //Console.Clear();
            //Console.WriteLine(movementSpeed);
            //Console.WriteLine((float)time.EllapsedTime.TotalMilliseconds);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move(movement);

            life(startLife);
        }


        public Vector2f getKeyPressed(float movementSpeed)
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
            //Console.WriteLine(testEnemy.touchedPlayer());
            //defekt
            if (map.getWalkable(playerSprite, new Vector2f(move.X, move.Y)) /*&& !abstractEnemy.touchedPlayer()*/)
                playerSprite.Position = new Vector2f(playerSprite.Position.X + move.X, playerSprite.Position.Y + move.Y);
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

        public void draw(RenderWindow window)
        {
            lifeSprite.Position = new Vector2f(((getSprite().Position.X + (getWeidth() / 2)) - 620), (getSprite().Position.Y + (getHeight() / 2)) + 320);
            window.Draw(playerSprite);
            window.Draw(lifeSprite);
            Console.Clear();
            Console.WriteLine(lifeSprite.Position);
        }
    }
}
