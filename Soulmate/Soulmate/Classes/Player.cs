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
        Texture playerTexture = new Texture("Pictures/SpielerSeiteRechts.png");
        Map map;
        Vector2f movement;
        float movementSpeed;

        bool moveAwayFromEnemy = false;
        List<Vector2f> hitFromDirections = new List<Vector2f>();

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
            hitBox = new HitBox(sprite.Position, getWeidth(), getHeight());
            map = levelMap;
        }

        public void update(GameTime time)
        {
            movementSpeed = 0.2f * (float)time.EllapsedTime.TotalMilliseconds;

            //Console.Clear();
            //Console.WriteLine(movementSpeed);
            //Console.WriteLine((float)time.EllapsedTime.TotalMilliseconds);

            sprite.Position = position;
            hitBox.setPosition(sprite.Position);

            movement = new Vector2f(0, 0);
            movement = getKeyPressed(movementSpeed);
            move2(movement);

            life(startLife);

            hitFromDirections.Clear();
        }

        public void move2(Vector2f direction)    //get a direction, and move to it with the enemys' movementspeed only left,right,up,down and diagonal don't wanna implements sin/cos just now
        {
           //noch nicht "ganz" korrekt besser gesagt sehr weit entfernt, aber es klappt
            if (hitAnotherEnemy() && !moveAwayFromEnemy)
            {
                moveAwayFromEnemy = true;

                for (int i = 0; i < hitFromDirections.Count; i++)
                {
                    if (Math.Abs((direction.X >= hitFromDirections[i].X) ? (direction.X) : (hitFromDirections[i].X)) > Math.Abs(direction.X - hitFromDirections[i].X) ||
                        Math.Abs((direction.X >= hitFromDirections[i].X) ? (direction.X) : (hitFromDirections[i].X)) < Math.Abs(direction.X - hitFromDirections[i].X))//if they have the same sign otherwise it doesn't matter
                    {
                        direction.X = -hitFromDirections[i].X;
                    }

                    if (Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) > Math.Abs(direction.Y - hitFromDirections[i].Y) ||
                        Math.Abs((direction.Y >= hitFromDirections[i].Y) ? (direction.Y) : (hitFromDirections[i].Y)) < Math.Abs(direction.Y - hitFromDirections[i].Y))
                    {
                        direction.Y = -hitFromDirections[i].Y;
                    }
                }
                move2(direction);
            }

            else
            {
                moveAwayFromEnemy = false;
                Vector2f movement = new Vector2f(0, 0);

                if (direction.X > 0)
                    movement.X += movementSpeed;
                else
                {
                    if (direction.X < 0)
                        movement.X -= movementSpeed;
                    else
                        movement.X += 0;
                }
                if (direction.Y > 0)
                    movement.Y += movementSpeed;
                else
                {
                    if (direction.Y < 0)
                        movement.Y -= movementSpeed;
                    else
                        movement.Y += 0;
                }
                
                move(movement);
            }
        }

        private bool hitAnotherEnemy()
        {
            for (int i = 0; i < EnemyHandler.getEnemies().Count; i++)
            {
                if ((hitBox.hit(EnemyHandler.getEnemies()[i].getHitBox())))
                {
                    bool notFound = true;
                    for (int j = 0; j < hitFromDirections.Count; j++)
                    {
                        if (hitFromDirections[j].Equals(hitBox.hitFrom(EnemyHandler.getEnemies()[i].getHitBox())))
                        {
                            notFound = false;
                        }
                    }
                    if (notFound)
                        hitFromDirections.Add(hitBox.hitFrom(EnemyHandler.getEnemies()[i].getHitBox()));
                }
            }
            if (hitFromDirections.Count > 0)
            {
                return true;
            }
            else
                return false;
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

        public void move(Vector2f move)
        {
            if (map.getWalkable(sprite, new Vector2f(move.X, move.Y)))
                position = new Vector2f(position.X + move.X, position.Y + move.Y);
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

        override public void draw(RenderWindow window)
        {
            lifeSprite.Position = new Vector2f(((getSprite().Position.X + (getWeidth() / 2)) - 620), (getSprite().Position.Y + (getHeight() / 2)) + 320);
            window.Draw(sprite);
            window.Draw(lifeSprite);
        }
    }
}
